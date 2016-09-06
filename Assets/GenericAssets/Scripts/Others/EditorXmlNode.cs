using System;
using System.Collections.Generic;

// =================================================================================================================
[Flags] public enum EditorXmlNodeType 
{
	NONE = 0
	,UNDEFINED = 1
	,DOCUMENT = 2
	,DECLARATION = 4
	,COMMENT = 8
	,ELEMENT = 16
	,ATTRIBUTE = 32
	,TEXT = 64
	,WHITESPACE = 128
	,ALL = 0xFF
    ,EXTRAS = 0 | 1 | 2 | 4 | 8 | 128
    ,DATA =  ~EditorXmlNodeType.EXTRAS
}

// =================================================================================================================
public class EditorXmlNode 
{
	public EditorXmlNode parent {
		set {
			if ( type == EditorXmlNodeType.DOCUMENT )
				return;
			
			if ( value != null )
			{
				if ( mParent != null )
					mParent.DetachChild (this);
			}
			mParent = value;
			
			if ( mParent != null && !mChildren.Contains (this) )
				mParent.AttachChild (this);
		}
		get {
			return mParent;
		}
	}
	
	public EditorXmlNode document {
		get {
			EditorXmlNode current = this;
			
			while ( current != null && current.type != EditorXmlNodeType.DOCUMENT )
				current = current.parent;
			
			return current;
		}
	}
	
	public List<EditorXmlNode> children {
		get { return mChildren; }
	}
    
    public string text {
        get 
        { 
            EditorXmlNode node = children.Find(x => x.type == EditorXmlNodeType.TEXT);
            return ( node != null ? node.content : string.Empty );
        }
    }

	public List<EditorXmlNode> attributes {
		get { return mAttributes; }
	}
	
	public bool inline {
		get {
			if ( mChildren.Count == 0 )
				return true;

			return ( mChildren [0].type == EditorXmlNodeType.TEXT );
		}
	}
	
	private EditorXmlNodeType type;
	public string name;
	public string content;
	
	private EditorXmlNode mParent;
	private List<EditorXmlNode> mChildren;
	private List<EditorXmlNode> mAttributes;
	
	private static char [] WHITESPACES = new char [] {' ', '\t', '\n', '\r'};

	private class XmlRawData 
	{
		public int pointer {
			get { return mPointer; }
			//set { mPointer = ( value < 0 ? 0 : ( value > mLength - 1 ? mLength - 1 : value ) ); }
		}
	
		public bool valid {
			get { return mValid; }
		}

		public char actual {
			get { if ( !valid ) return ' '; return mText [mPointer]; }
		}
		
		public char next {
			get { 
				if ( mPointer + 1 == mLength )
					return ' ';
				
				return mText [mPointer + 1]; 
			}
		}
	
		public char previous {
			get {
				if ( mPointer == 0 )
					return ' ';
			
				return mText [mPointer - 1];
			}
		}
	
		public string text {
			get { return mText; }		
		}
	
		public int length {
			get { return mLength; }
		}

		private bool mValid;
		private int mPointer;
		private string mText;
		private int mLength;
		
		public XmlRawData (string text)
		{
			mValid = true;

			if ( text == string.Empty )
				mValid = false;

			mPointer = 0;
		
			mText = text;
			mLength = text.Length;
		}
	
		public bool Advance ()
		{
			mPointer ++;
		
			if ( mPointer > mLength - 1 )
			{
				mPointer --;
				mValid = false;
			
				return false;
			}
		
			return true;
		}
	
		public bool Advance (int steps)
		{
			bool ret = true;
		
			while ( steps > 0 )
			{
				ret = Advance ();
				steps --;
			}
		
			return ret;		
		}
	
		public string GetData (int length)
		{
			if ( mPointer + length >= mLength )
				return "";
			
			return GetData (mPointer, length);
		}

		public string GetData (int from, int length)
		{
			if ( from >= mLength || from + length >= mLength )
				return "";
			
			return mText.Substring (from, length);
		}
		
		public bool IsOn (char [] chars)
		{
			foreach ( char c in chars )
			{
				if ( actual == c )
					return true;
			}
			
			return false;
		}
		
//		public void say (string str)
//		{
//			int line = 1;
//			int column = 0;
//			
//			for ( int i = 0; i < pointer; i ++ )
//			{
//				if ( mText [i] == '\n' )
//				{
//					line ++;
//					column = 0;
//				}
//				else
//				{
//					column ++;
//				}
//			}
//			
//			System.Console.WriteLine ("[" + line + "," + column + "]: " + str);
//		}
	}


	public EditorXmlNode (EditorXmlNodeType type, string name)
	{
		_Create (type, name, string.Empty);
	}
	
	public EditorXmlNode (EditorXmlNodeType type, string name, string content)
	{
		_Create (type, name, content);
	}
	
	private void _Create (EditorXmlNodeType type, string name, string content)
	{
		//LOG.say ("New node: " + LOG.fill (type.ToString (), 15) + " | " + LOG.fill (name.ToString (), 15) + " | [" + content.Replace ("\n", "/") + "]");
		
		//int index;
		
		this.type = type;
		this.name = name;
		this.content = content;
		
		mParent = null;
		mChildren = new List<EditorXmlNode> (1);
		mAttributes = new List<EditorXmlNode> (0); //_ParseAttributes (content.Trim ());
	}

	public string ToXmlStringProfiling (bool indent)
	{
		string str;
		
		//DateTime start = DateTime.Now;
		
		str = ToXmlString (indent);
		
		//DateTime end = DateTime.Now;
		//TimeSpan span = end - start;

		return str;
	}

	public string ToXmlString (bool indent)
	{
		if ( indent )
		{
			return _ToXmlStringIndent (0);
		}

		return _ToXmlString ();
	}

	public List<string> ToXmlListProfiling (bool indent)
	{
		List<string> list;
		
		//DateTime start = DateTime.Now;
		
		list = ToXmlList (indent);
		
		//DateTime end = DateTime.Now;
		//TimeSpan span = end - start;

		return list;
	}
	
	public List<string> ToXmlList (bool indent)
	{
		List<string> list = new List<string> (100);
		
		if ( indent )
		{
			return _ToXmlListIndent (list, 0);
		}

		return _ToXmlList (list);
	}
	









	private string _ToXmlStringIndent (int indent)
	{
		string str = string.Empty;
		
		switch ( type )
		{
			case EditorXmlNodeType.DOCUMENT: 
			{
				if ( indent > 0 )
					str += new String (' ', indent * 3);
					
				foreach (EditorXmlNode node in mChildren)
				{
					str += node._ToXmlStringIndent (indent);
				}
				
				break;
			}
			
			case EditorXmlNodeType.DECLARATION:
			{
				if ( indent > 0 )
					str += new String (' ', indent * 3);

				str += "<?" + name;
				if ( mAttributes.Count != 0 )
				{
					foreach ( EditorXmlNode node in mAttributes )
					{
						str += " " + node.name + "=\"" + node.content + "\"";
					}
				}
				str += "?>\n";
				
				break;
			}
			
			case EditorXmlNodeType.COMMENT: 
			{
				if ( indent > 0 )
					str += new String (' ', indent * 3);

				str += "<!--" + content + "-->\n";
				
				break;
			}
			
			case EditorXmlNodeType.TEXT:
			{
				str += content;
				
				break;
			}
			
			case EditorXmlNodeType.ELEMENT: 
			{
				if ( mChildren.Count == 0 )
				{
					if ( indent > 0 )
						str += new String (' ', indent * 3);

					str += "<" + name;
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							str += " " + node.name + "=\"" + node.content + "\"";
						}
					}
					str += "/>\n";
				}
				else
				{
					bool textflag = false;
					
					EditorXmlNode previous = parent.PreviousChild (this);
					
					if ( indent > 0 && (previous == null || previous.type != EditorXmlNodeType.TEXT) )
						str += new String (' ', indent * 3);
						
					str += "<" + name;
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							str += " " + node.name + "=\"" + node.content + "\"";
						}
					}
					str += ">";
					
					if ( mChildren.Count != 0 )
					{
						if ( mChildren [0].type == EditorXmlNodeType.TEXT )
							textflag = true;
					}
						
					if ( !textflag )
						str += "\n";

					foreach (EditorXmlNode node in mChildren)
					{
						str += node._ToXmlStringIndent (indent + 1);
					}
					
					if ( indent > 0 && !textflag )
						str += new String (' ', indent * 3);

					str += "</" + name + ">";
					
					if ( !parent.inline )
						str += "\n";
				}
				
				break;
			}
			
			case EditorXmlNodeType.WHITESPACE: 
			{
				break;
			}
			
			default:
			{
				if ( indent > 0 )
					str += new String (' ', indent * 3);

				str += "<!-- Undefined node: " + name + " -->\n";
				
				break;
			}
		}
		
		return str;
	}


	private string _ToXmlString ()
	{
		string str = string.Empty;
		
		switch ( type )
		{
			case EditorXmlNodeType.DOCUMENT: 
			{
				foreach (EditorXmlNode node in mChildren)
					str += node._ToXmlString ();
				
				break;
			}
			
			case EditorXmlNodeType.DECLARATION:
			{
				str += "<?" + name;
				if ( mAttributes.Count != 0 )
				{
					foreach ( EditorXmlNode node in mAttributes )
					{
						str += " " + node.name + "=\"" + node.content + "\"";
					}
				}
				str += "?>";
				
				break;
			}
			
			case EditorXmlNodeType.COMMENT: 
			{
				str += "<!--" + content + "-->";
				
				break;
			}
			
			case EditorXmlNodeType.TEXT:
			{
				str += content;
				
				break;
			}
			
			case EditorXmlNodeType.ELEMENT: 
			{
				if ( mChildren.Count == 0 )
				{
					str += "<" + name;
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							str += " " + node.name + "=\"" + node.content + "\"";
						}
					}
					str += "/>";
				}
				else
				{
					//XmlNode previous = parent.PreviousChild (this);
					
					str += "<" + name;
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							str += " " + node.name + "=\"" + node.content + "\"";
						}
					}
					str += ">";
					
					foreach (EditorXmlNode node in mChildren)
					{
						str += node._ToXmlString ();
					}
					
					str += "</" + name + ">";
				}
				
				break;
			}
			
			case EditorXmlNodeType.WHITESPACE: 
			{
				break;
			}
			
			default:
			{
				str += "<!-- Undefined node: " + name + " -->";
				
				break;
			}
		}
		
		return str;
	}


	private List<string> _ToXmlListIndent (List<string> list, int indent)
	{
		switch ( type )
		{
			case EditorXmlNodeType.DOCUMENT: 
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));
					
				foreach (EditorXmlNode node in mChildren)
				{
					list = node._ToXmlListIndent (list, indent);
				}
				
				break;
			}
			
			case EditorXmlNodeType.DECLARATION:
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<?" + name);
				if ( mAttributes.Count != 0 )
				{
					foreach ( EditorXmlNode node in mAttributes )
					{
						list.Add (" " + node.name + "=\"" + node.content + "\"");
					}
				}
				list.Add ("?>\n");
				
				break;
			}
			
			case EditorXmlNodeType.COMMENT: 
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<!--" + content + "-->\n");
				
				break;
			}
			
			case EditorXmlNodeType.TEXT:
			{
				list.Add (content);
				
				break;
			}
			
			case EditorXmlNodeType.ELEMENT: 
			{
				if ( mChildren.Count == 0 )
				{
					if ( indent > 0 )
						list.Add (new String (' ', indent * 3));

					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add ("/>\n");
				}
				else
				{
					bool textflag = false;
					
					EditorXmlNode previous = parent.PreviousChild (this);
					
					if ( indent > 0 && (previous == null || previous.type != EditorXmlNodeType.TEXT) )
						list.Add (new String (' ', indent * 3));
						
					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add (">");
					
					if ( mChildren.Count != 0 )
					{
						if ( mChildren [0].type == EditorXmlNodeType.TEXT )
							textflag = true;
					}
						
					if ( !textflag )
						list.Add ("\n");

					foreach (EditorXmlNode node in mChildren)
					{
						list = node._ToXmlListIndent (list, indent + 1);
					}
					
					if ( indent > 0 && !textflag )
						list.Add (new String (' ', indent * 3));

					list.Add ("</" + name + ">");
					
					if ( !parent.inline )
						list.Add ("\n");
				}
				
				break;
			}
			
			case EditorXmlNodeType.WHITESPACE: 
			{
				break;
			}
			
			default:
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<!-- Undefined node: " + name + " -->\n");
				
				break;
			}
		}
		
		return list;
	}


	private List<string> _ToXmlList (List<string> list)
	{
		switch ( type )
		{
			case EditorXmlNodeType.DOCUMENT: 
			{
				foreach (EditorXmlNode node in mChildren)
					list = node._ToXmlList (list);
				
				break;
			}
			
			case EditorXmlNodeType.DECLARATION:
			{
				list.Add ("<?" + name);
				if ( mAttributes.Count != 0 )
				{
					foreach ( EditorXmlNode node in mAttributes )
					{
						list.Add (" " + node.name + "=\"" + node.content + "\"");
					}
				}
				list.Add ("?>");
				
				break;
			}
			
			case EditorXmlNodeType.COMMENT: 
			{
				list.Add ("<!--" + content + "-->");
				
				break;
			}
			
			case EditorXmlNodeType.TEXT:
			{
				list.Add (content);
				
				break;
			}
			
			case EditorXmlNodeType.ELEMENT: 
			{
				if ( mChildren.Count == 0 )
				{
					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add ("/>");
				}
				else
				{
					//XmlNode previous = parent.PreviousChild (this);
					
					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( EditorXmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add (">");
					
					foreach (EditorXmlNode node in mChildren)
					{
						list = node._ToXmlList (list);
					}
					
					list.Add ("</" + name + ">");
				}
				
				break;
			}
			
			case EditorXmlNodeType.WHITESPACE: 
			{
				break;
			}
			
			default:
			{
				list.Add ("<!-- Undefined node: " + name + " -->");
				
				break;
			}
		}
		
		return list;
	}


	/*
	private void _ToXmlListIndent (List<string> list, int indent)
	{
		DateTime start = DateTime.Now;
		DateTime end;
		TimeSpan span;
		
		switch ( type )
		{
			case XmlNodeType.DOCUMENT: 
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));
					
				foreach (XmlNode node in mChildren)
				{
					node._ToXmlListIndent (list, indent, false);
				}
				
				break;
			}
			
			case XmlNodeType.DECLARATION:
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<?" + name);
				if ( mAttributes.Count != 0 )
				{
					foreach ( XmlNode node in mAttributes )
					{
						list.Add (" " + node.name + "=\"" + node.content + "\"");
					}
				}
				list.Add ("?>\n");
				
				break;
			}
			
			case XmlNodeType.COMMENT: 
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<!--" + content + "-->\n");
				
				break;
			}
			
			case XmlNodeType.TEXT:
			{
				list.Add (content);
				
				break;
			}
			
			case XmlNodeType.ELEMENT: 
			{
				if ( mChildren.Count == 0 )
				{
					if ( indent > 0 )
						list.Add (new String (' ', indent * 3));

					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( XmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add ("/>\n");
				}
				else
				{
					bool textflag = false;
					
					XmlNode previous = parent.PreviousChild (this);
					
					if ( indent > 0 && (previous == null || previous.type != XmlNodeType.TEXT) )
						list.Add (new String (' ', indent * 3));
						
					list.Add ("<" + name);
					if ( mAttributes.Count != 0 )
					{
						foreach ( XmlNode node in mAttributes )
						{
							list.Add (" " + node.name + "=\"" + node.content + "\"");
						}
					}
					list.Add (">");
					
					if ( mChildren.Count != 0 )
					{
						if ( mChildren [0].type == XmlNodeType.TEXT )
							textflag = true;
					}
						
					if ( !textflag )
						list.Add ("\n");

					foreach (XmlNode node in mChildren)
					{
						node._ToXmlListIndent (list, indent + 1, false);
					}
					
					if ( indent > 0 && !textflag )
						list.Add (new String (' ', indent * 3));

					list.Add ("</" + name + ">");
					
					if ( !parent.inline )
						list.Add ("\n");
				}
				
				break;
			}
			
			case XmlNodeType.UNDEFINED:
			{
				if ( indent > 0 )
					list.Add (new String (' ', indent * 3));

				list.Add ("<!-- Undefined node: " + name + " -->");
				
				break;
			}
		}
		
		if ( profiling )
		{
			end = DateTime.Now;
			span = end - start;
		
			say ("building string representation using list<string> sequence: " + span.TotalSeconds + " sec");
		}
	}
	*/
	
	public static EditorXmlNode CreateDocument () { return EditorXmlNode.ParseDocument (string.Empty); }
	
	public static EditorXmlNode ParseDocumentProfiling (string xml) { return EditorXmlNode.ParseDocumentProfiling (xml, EditorXmlNodeType.NONE); }
	public static EditorXmlNode ParseDocumentProfiling (string xml, EditorXmlNodeType ignore)
	{
		EditorXmlNode document = null;
		
		//DateTime start = DateTime.Now;
		
		document = EditorXmlNode.ParseDocument (xml, ignore);
		
		//DateTime end = DateTime.Now;
		//TimeSpan span = end - start;

		//say ("parsing xml string to xml document: " + span.TotalSeconds + " sec");
		
		return document;
	}
	
	public static EditorXmlNode ParseDocument (string xml) { return EditorXmlNode.ParseDocument (xml, EditorXmlNodeType.NONE); }
	public static EditorXmlNode ParseDocument (string xml, EditorXmlNodeType ignore)
	{
		EditorXmlNode emptydocument = new EditorXmlNode (EditorXmlNodeType.DOCUMENT, "XmlDocument", "");
		EditorXmlNode document = new EditorXmlNode (EditorXmlNodeType.DOCUMENT, "XmlDocument", "");
		XmlRawData data = new XmlRawData (xml);

		EditorXmlNode current = document;
		EditorXmlNode node;
		
		string tagname = string.Empty;
		string content = string.Empty;
		int mark;
		int textmark = -1;
		
		
		while ( data.valid )
		{
			if ( data.actual == '<' )
			{
				// Check if text was found before entering the tag
				if ( textmark != -1 )
				{
					content = data.GetData (textmark, data.pointer - textmark);
					
					if ( content.Trim () == string.Empty )
					{
						if ( !((ignore & EditorXmlNodeType.WHITESPACE) == EditorXmlNodeType.WHITESPACE) )
						{
							node = new EditorXmlNode (EditorXmlNodeType.WHITESPACE, "XmlWhitespace", content);

							current.AttachChild (node);
						}
					}
					else
					{
						if ( !((ignore & EditorXmlNodeType.TEXT) == EditorXmlNodeType.TEXT) )
						{
							node = new EditorXmlNode (EditorXmlNodeType.TEXT, "XmlText", content);
						
							current.AttachChild (node);
						}
					}
					
					textmark = -1;
				}
				
				// Xml declaration ?
				if ( data.next == '?' ) 
				{
					data.Advance (2);
					mark = data.pointer;

					// Get the node's name
					while ( data.valid && data.actual != '?' && !data.IsOn (WHITESPACES) )
					{
						data.Advance ();
					}
					
					if ( !data.valid )
					{
						//data.say ("The declaration was not closed properly");
						return emptydocument;
					}
					
					tagname = data.GetData (mark, data.pointer - mark).Trim ();
					
					if ( tagname == string.Empty )
					{
						//data.say ("An Xml declaration can not have an empty name");
						return emptydocument;
					}
						
					node = new EditorXmlNode (EditorXmlNodeType.DECLARATION, tagname);
						
					while ( data.valid && data.IsOn (WHITESPACES) )
						data.Advance ();

					while ( data.valid && data.actual != '?' )
					{
						if ( !data.IsOn (WHITESPACES) )
						{
							mark = data.pointer;

							while ( data.valid && data.actual != '=' )
								data.Advance ();
					
							if ( !data.valid )
							{
								//data.say ("All attributes must be matched with a value");
								return emptydocument;
							}
					
							tagname = data.GetData (mark, data.pointer - mark).Trim ();
					
							data.Advance ();
					
							while ( data.valid && data.IsOn (WHITESPACES) )
							{
								data.Advance ();
							}
					
							if ( !data.valid )
							{
								//data.say ("All attributes must have a value specified");
								return emptydocument;
							}
					
							while ( data.valid && data.actual != '"' )
							{
								data.Advance ();
							}
					
							data.Advance ();
					
							mark = data.pointer;
					
							while ( data.valid && data.actual != '"' )
							{
								data.Advance ();
							}
					
							if ( !data.valid )
							{
								//data.say ("All attribute's values must be enclosed with double quotes");
								return emptydocument;
							}
					
							content = data.GetData (mark, data.pointer - mark);
					
							node.SetAttribute (tagname, content);
						}
						
						data.Advance ();
					}	
					
					if ( !data.valid )
					{
						//data.say ("End of declaration was not closed properly");
						return emptydocument;
					}
					
					current.AttachChild (node);
					data.Advance (2);
				}
				// Comments and CDATA
				else  if ( data.next == '!' )
				{
					// Comments ?
					if ( data.GetData (4) == "<!--" )
					{
						data.Advance (4);
						mark = data.pointer;
						
						while ( data.valid && (data.actual != '-' || data.GetData (3) != "-->") )
							data.Advance ();
							
						if ( !data.valid )
						{
							//data.say ("Comment was not closed properly");
							return emptydocument;
						}
						
						if ( !((ignore & EditorXmlNodeType.COMMENT) == EditorXmlNodeType.COMMENT) )
						{
							node = new EditorXmlNode (EditorXmlNodeType.COMMENT, "XmlComment", data.GetData (mark, data.pointer - mark));
						
							current.AttachChild (node);
						}
						
						data.Advance (2);
					}
					else
					{
						//data.say ("Tag type could not be determined");
						return emptydocument;
					}
				}
				// Closing element
				else  if ( data.next == '/' )
				{
					data.Advance (2);
					mark = data.pointer;
					
					while ( data.valid && data.actual != '>' )
						data.Advance ();
						
					if ( !data.valid )
					{
						//data.say ("End of element was not closed properly");
						return emptydocument;
					}
					
					tagname = data.GetData (mark, data.pointer - mark);
					
					if ( string.Compare (tagname, current.name) != 0 )
					{
						//data.say ("Closing a not previously opened element (" + tagname + "," + current.name + ")");
						return emptydocument;
					}
					
					current = current.parent;
				}
				// Everything else (expecting a new element)
				else
				{
					data.Advance ();
					mark = data.pointer;

					// Get the node's name
					while ( data.valid && data.actual != '>' && data.actual != '/' && !data.IsOn (WHITESPACES) )
					{
						data.Advance ();
					}
					
					if ( !data.valid )
					{
						//data.say ("The element was not closed properly");
						return emptydocument;
					}

					tagname = data.GetData (mark, data.pointer - mark).Trim ();
					
					if ( tagname == string.Empty )
					{
						//data.say ("A tag can not have an empty name");
						return emptydocument;
					}
						
					node = new EditorXmlNode (EditorXmlNodeType.ELEMENT, tagname);
						
					while ( data.valid && data.IsOn (WHITESPACES) )
						data.Advance ();

					while ( data.valid && data.actual != '>' )
					{
						if ( data.actual == '/' && data.GetData (2) == "/>" )
						{
							break;
						}
						else  if ( data.actual == '>' )
						{
							break;
						}
						else  if ( !data.IsOn (WHITESPACES) )
						{
							mark = data.pointer;

							while ( data.valid && data.actual != '=' )
								data.Advance ();
					
							if ( !data.valid )
							{
								//data.say ("All attributes must be matched with a value");
								return emptydocument;
							}
					
							tagname = data.GetData (mark, data.pointer - mark).Trim ();
					
							data.Advance ();
					
							while ( data.valid && data.IsOn (WHITESPACES) )
							{
								data.Advance ();
							}
					
							if ( !data.valid )
							{
								//data.say ("All attributes must have a value specified");
								return emptydocument;
							}
					
							while ( data.valid && data.actual != '"' )
							{
								data.Advance ();
							}
					
							data.Advance ();
					
							mark = data.pointer;
					
							while ( data.valid && data.actual != '"' )
							{
								data.Advance ();
							}
					
							if ( !data.valid )
							{
								//data.say ("All attribute's values must be enclosed with double quotes");
								return emptydocument;
							}
					
							content = data.GetData (mark, data.pointer - mark);
					
							node.SetAttribute (tagname, content);
						}
						
						data.Advance ();
					}	
					
					if ( !data.valid )
					{
						//data.say ("End of element was not closed properly");
						return emptydocument;
					}
					
					if ( data.actual == '>' )
					{
						current.AttachChild (node);
						current = node;
					}
					else  if ( data.actual == '/' )
					{
						current.AttachChild (node);
						data.Advance ();
					}
				}
			}
			else
			{
				if ( textmark == -1 )
					textmark = data.pointer;
			}
			
			
			data.Advance ();
		}
		
		return document;	
	}
	
    public EditorXmlNode CreateChildElement(string name)
    {
        EditorXmlNode child = new EditorXmlNode(EditorXmlNodeType.ELEMENT, name);
        
        AttachChild(child);
        
        return child;
    }
    
	public void AttachChild (EditorXmlNode child)
	{
		if ( child != null && (type == EditorXmlNodeType.ELEMENT || type == EditorXmlNodeType.DOCUMENT) )
		{
			if ( !mChildren.Contains (child) )
			{
				if ( child.parent != null )
					child.parent = null;
			
				mChildren.Add (child);
			
				child.parent = this;
			}
		}
	}
	
	public void DetachChild (EditorXmlNode child)
	{
		if ( child != null && child.parent == this && (type == EditorXmlNodeType.ELEMENT || type == EditorXmlNodeType.DOCUMENT) )
		{
			if ( mChildren.Contains (child) )
			{
				mChildren.Remove (child);
				
				if ( child.parent != null )
					child.parent = null;
			}
		}
	}	
	
	public EditorXmlNode PreviousChild (EditorXmlNode child)
	{
		int index = mChildren.IndexOf (child);
		
		if ( index >= 1 )
		{
			return mChildren [index - 1];
		}
		
		return null;
	}
 
    // -------------------------------------------------------------------------------------------------------------
	public EditorXmlNode NextChild (EditorXmlNode child)
	{
		int index = mChildren.IndexOf (child);
		
		if ( index != -1 && index < mChildren.Count - 1 )
		{
			return mChildren [index + 1];
		}
		
		return null;
	}
 
    // -------------------------------------------------------------------------------------------------------------
	public void SetAttribute (string name, string content)
	{
		EditorXmlNode node;
		
		if ( (node = GetAttribute (name)) != null )
		{
			node.content = content;
		}
		else
		{
			mAttributes.Add (new EditorXmlNode (EditorXmlNodeType.ATTRIBUTE, name, content));
		}	
	}
	
    // -------------------------------------------------------------------------------------------------------------
    public void SetAttribute (string name, object content)
    {
        if ( content as IFormattable != null )
        {
            SetAttribute(name, content.ToString());
        }
    }

    // -------------------------------------------------------------------------------------------------------------
	public EditorXmlNode GetAttribute (string name)
	{
		foreach ( EditorXmlNode node in mAttributes )
		{
			if ( node.name == name )
			{
				return node;
			}
		}
		
		return null;
	}
	
    // -------------------------------------------------------------------------------------------------------------
    public EditorXmlNode GetChildByName (string name)
    {
        foreach ( EditorXmlNode node in mChildren )
        {
            if ( node.name.CompareTo(name) == 0 )
            {
                return node;
            }
        }
        
        return null;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public List<EditorXmlNode> GetChildrenByName (string name)
    {
        List<EditorXmlNode> result = new List<EditorXmlNode>();
        
        foreach ( EditorXmlNode node in mChildren )
        {
            if ( node.name.CompareTo(name) == 0 )
            {
                result.Add(node);
            }
        }
        
        return result;
    }

//	private static void say (string str)
//	{
//		System.Console.WriteLine (str);
//	}
}







