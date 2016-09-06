using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// =================================================================================================================
public class EditorSaveState
{
    // -------------------------------------------------------------------------------------------------------------
    private string mXmlFilename;
    private string mXmlFileContent;
    
    private Dictionary<string, string> mRecords;
    
    // -------------------------------------------------------------------------------------------------------------
    private const string EDITOR_STATE_FOLDER = "EditorStates";
    
    // -------------------------------------------------------------------------------------------------------------
    public EditorSaveState(string xmlfilename) { Initialize(xmlfilename, false); }
    public EditorSaveState(string xmlfilename, bool forcenew) { Initialize(xmlfilename, forcenew); }
        
    // -------------------------------------------------------------------------------------------------------------
    private void Initialize(string xmlfilename, bool forcenew)    
    {
        #if !UNITY_WEBPLAYER
        mXmlFilename = EDITOR_STATE_FOLDER + "/" + xmlfilename;
        
        if ( !Directory.Exists(EDITOR_STATE_FOLDER) )
        {
            Directory.CreateDirectory(EDITOR_STATE_FOLDER);
        }
        
        if ( forcenew )
        {
            File.CreateText(mXmlFilename).Close();
        }
        else if ( !File.Exists(mXmlFilename) )
        {
            Debug.LogWarning("File '" + xmlfilename + "' not found under " + EDITOR_STATE_FOLDER + " folder, creating a new one.");
            
            File.CreateText(mXmlFilename).Close();
        }
        
        mXmlFileContent = File.ReadAllText(mXmlFilename);
        
        EditorXmlNode file = EditorXmlNode.ParseDocument(mXmlFileContent, EditorXmlNodeType.DATA);
        EditorXmlNode records = file.GetChildByName("records");
        
        mRecords = new Dictionary<string, string>();
        
        if ( records != null )
        {
            foreach ( EditorXmlNode record in records.GetChildrenByName("record") )
            {
                mRecords.Add(record.GetAttribute("name").content, record.GetAttribute("value").content);
            }
        }
        #endif
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void SetState(string name, string value)
    {
        mRecords[name] = value;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void SetState(string name, int value)
    {
        mRecords[name] = value.ToString();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void SetState(string name, float value)
    {
        mRecords[name] = value.ToString();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void SetState(string name, bool value)
    {
        mRecords[name] = value.ToString();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public string GetState(string name, string def)
    {
        if ( mRecords.ContainsKey(name) )
        {
            return mRecords[name];
        }
        
        return def;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public int GetState(string name, int def)
    {
        if ( mRecords.ContainsKey(name) )
        {
            try
            {
                return System.Convert.ToInt32(mRecords[name]);
            }
            catch ( System.Exception )
            {
                return def;
            }
        }
        
        return def;
    }

    // -------------------------------------------------------------------------------------------------------------
    public float GetState(string name, float def)
    {
        if ( mRecords.ContainsKey(name) )
        {
            try
            {
                return System.Convert.ToSingle(mRecords[name]);
            }
            catch ( System.Exception )
            {
                return def;
            }
        }
        
        return def;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public bool GetState(string name, bool def)
    {
        if ( mRecords.ContainsKey(name) )
        {
            try
            {
                return System.Convert.ToBoolean(mRecords[name]);
            }
            catch ( System.Exception )
            {
                return def;
            }
        }
        
        return def;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void Save()
    {
        #if !UNITY_WEBPLAYER
        File.CreateText(mXmlFilename).Close();
        
        EditorXmlNode document = EditorXmlNode.CreateDocument();
        EditorXmlNode records = new EditorXmlNode(EditorXmlNodeType.ELEMENT, "records");
        EditorXmlNode record = null;
        
        document.AttachChild(records);
        
        foreach ( string key in mRecords.Keys )
        {
            record = new EditorXmlNode(EditorXmlNodeType.ELEMENT, "record");
            
            record.SetAttribute("name", key);
            record.SetAttribute("value", mRecords[key]);
            
            records.AttachChild(record);
        }
        
        File.WriteAllText(mXmlFilename, document.ToXmlString(true));
        #endif
    }
}
