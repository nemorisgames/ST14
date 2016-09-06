using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

// =================================================================================================================
public static class EditorGUIControls
{
    // -------------------------------------------------------------------------------------------------------------
    private static GUILayoutOption _LARGE_BUTTON_WIDTH;
    public static float LARGE_BUTTON_WIDTH_NUMBER { get { return 85.0f; } }
    public static GUILayoutOption LARGE_BUTTON_WIDTH { get { _LARGE_BUTTON_WIDTH = ( _LARGE_BUTTON_WIDTH == null ? GUILayout.Width(LARGE_BUTTON_WIDTH_NUMBER) : _LARGE_BUTTON_WIDTH ); return _LARGE_BUTTON_WIDTH; } }
    
    private static GUILayoutOption _BUTTON_WIDTH;
    public static GUILayoutOption BUTTON_WIDTH { get { _BUTTON_WIDTH = ( _BUTTON_WIDTH == null ? GUILayout.Width(70) : _BUTTON_WIDTH ); return _BUTTON_WIDTH; } }
    
    private static GUILayoutOption _MEDIUM_BUTTON_WIDTH;
    public static float MEDIUM_BUTTON_WIDTH_NUMBER { get { return 40.0f; } }
    public static GUILayoutOption MEDIUM_BUTTON_WIDTH { get { _MEDIUM_BUTTON_WIDTH = ( _MEDIUM_BUTTON_WIDTH == null ? GUILayout.Width(MEDIUM_BUTTON_WIDTH_NUMBER) : _MEDIUM_BUTTON_WIDTH ); return _MEDIUM_BUTTON_WIDTH; } }
    
    private static GUILayoutOption _SMALL_BUTTON_WIDTH;
    public static float SMALL_BUTTON_WIDTH_NUMBER { get { return 25.0f; } }
    public static GUILayoutOption SMALL_BUTTON_WIDTH { get { _SMALL_BUTTON_WIDTH = ( _SMALL_BUTTON_WIDTH == null ? GUILayout.Width(SMALL_BUTTON_WIDTH_NUMBER) : _SMALL_BUTTON_WIDTH ); return _SMALL_BUTTON_WIDTH; } }

    private static GUILayoutOption _VERYSMALL_BUTTON_WIDTH;
    public static float VERYSMALL_BUTTON_WIDTH_NUMBER { get { return 20.0f; } }
    public static GUILayoutOption VERYSMALL_BUTTON_WIDTH { get { _VERYSMALL_BUTTON_WIDTH = ( _VERYSMALL_BUTTON_WIDTH == null ? GUILayout.Width(VERYSMALL_BUTTON_WIDTH_NUMBER) : _VERYSMALL_BUTTON_WIDTH ); return _VERYSMALL_BUTTON_WIDTH; } }

    private static GUILayoutOption _LABEL_WIDTH;
    public static GUILayoutOption LABEL_WIDTH { get { _LABEL_WIDTH = ( _LABEL_WIDTH == null ? GUILayout.Width(100) : _LABEL_WIDTH ); return _LABEL_WIDTH; } }
        
    private static GUILayoutOption _LARGE_LABEL_WIDTH;
    public static float LARGE_LABEL_WIDTH_NUMBER { get { return 120.0f; } }
    public static GUILayoutOption LARGE_LABEL_WIDTH { get { _LARGE_LABEL_WIDTH = ( _LARGE_LABEL_WIDTH == null ? GUILayout.Width(LARGE_LABEL_WIDTH_NUMBER) : _LARGE_LABEL_WIDTH ); return _LARGE_LABEL_WIDTH; } }
    
    private static GUILayoutOption _LABEL_SEPARATOR_WIDTH;
    public static float LABEL_SEPARATOR_WIDTH_NUMBER { get { return 10.0f; } }
    public static GUILayoutOption LABEL_SEPARATOR_WIDTH { get { _LABEL_SEPARATOR_WIDTH = ( _LABEL_SEPARATOR_WIDTH == null ? GUILayout.Width(LABEL_SEPARATOR_WIDTH_NUMBER) : _LABEL_SEPARATOR_WIDTH ); return _LABEL_SEPARATOR_WIDTH; } }
    
    private static GUILayoutOption _DROP_WIDTH;
    public static GUILayoutOption DROP_WIDTH { get { _DROP_WIDTH = ( _DROP_WIDTH == null ? GUILayout.Width(53) : _DROP_WIDTH ); return _DROP_WIDTH; } }
    
    private static GUILayoutOption _SMALL_INPUT_WIDTH;
    public static GUILayoutOption SMALL_INPUT_WIDTH { get { _SMALL_INPUT_WIDTH = ( _SMALL_INPUT_WIDTH == null ? GUILayout.Width(100) : _SMALL_INPUT_WIDTH ); return _SMALL_INPUT_WIDTH; } }
    
    private static GUILayoutOption _INPUT_WIDTH;
    public static GUILayoutOption INPUT_WIDTH { get { _INPUT_WIDTH = ( _INPUT_WIDTH == null ? GUILayout.Width(140) : _INPUT_WIDTH ); return _INPUT_WIDTH; } }
                            
    private static GUIStyle _STYLE_BUTTON_LEFT;
    public static GUIStyle STYLE_BUTTON_LEFT { get { if ( _STYLE_BUTTON_LEFT == null ) { _STYLE_BUTTON_LEFT = new GUIStyle(GUI.skin.button); _STYLE_BUTTON_LEFT.alignment = TextAnchor.MiddleLeft; } return _STYLE_BUTTON_LEFT; } }
    
    private static GUIStyle _STYLE_LABEL_LEFT;
    public static GUIStyle STYLE_LABEL_LEFT { get { if ( _STYLE_LABEL_LEFT == null ) { _STYLE_LABEL_LEFT = new GUIStyle(GUI.skin.label); } return _STYLE_LABEL_LEFT; } }
    
    private static GUIStyle _STYLE_LABEL_CENTER;
    public static GUIStyle STYLE_LABEL_CENTER { get { if ( _STYLE_LABEL_CENTER == null ) { _STYLE_LABEL_CENTER = new GUIStyle(GUI.skin.label); _STYLE_LABEL_CENTER.alignment = TextAnchor.MiddleCenter; } return _STYLE_LABEL_CENTER; } }
    
    private static GUIStyle _STYLE_LABEL_RIGHT;
    public static GUIStyle STYLE_LABEL_RIGHT { get { if ( _STYLE_LABEL_RIGHT == null ) { _STYLE_LABEL_RIGHT = new GUIStyle(GUI.skin.label); _STYLE_LABEL_RIGHT.alignment = TextAnchor.MiddleRight; } return _STYLE_LABEL_RIGHT; } }
    
    public const string EDITOR_RESOURCES_PATH = "Assets/3_Engine/Scripts/UnityEditorScripts/Editor/EditorResources/";
    
    // -------------------------------------------------------------------------------------------------------------
    private static Dictionary<string,bool> TITLEX;
    
    private static Color CLIPBOARD_COLOR;

    #region Generics
    // -------------------------------------------------------------------------------------------------------------
    public static bool TitleX(string title)
    {
        if ( TITLEX == null )
        {
            TITLEX = new Dictionary<string,bool>();
        }
        
        string titleshow = string.Empty;
        string titleid = string.Empty;
        
        if ( title.Contains("|") )
        {
            titleshow = title.Split('|')[1];
            titleid = title;
        }
        else
        {
            titleshow = title;
            titleid = "Generic|" + title;
        }
        
        if ( !TITLEX.ContainsKey(titleid) )
        {
            TITLEX.Add(titleid, true);
        }
        
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("", EditorGUIControls.SMALL_BUTTON_WIDTH);
            GUILayout.FlexibleSpace();
            
            GUI.color = new Color(0.8f, 1.0f, 0.8f);
            if ( GUILayout.Button(titleshow, GUI.skin.label) )
            {
                TITLEX[titleid] = !TITLEX[titleid];
            }
            GUI.color = Color.white;
            
            GUILayout.FlexibleSpace();
            
            if ( GUILayout.Button("", EditorGUIControls.SMALL_BUTTON_WIDTH) )
            {
                TITLEX[titleid] = !TITLEX[titleid];
            }
        }
        GUILayout.EndHorizontal();
        
        return TITLEX[titleid];
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static void Title(string title)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUI.color = new Color(0.8f, 1.0f, 0.8f);
            GUI.SetNextControlName("Dummy");
            GUILayout.Label(title);
            GUI.color = Color.white;
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static T DropField<T>(string label) where T:class
    {
        T dropped = null;
        
        GUILayout.BeginHorizontal();
        {
            if ( !string.IsNullOrEmpty(label) )
            {
                GUILayout.Label(label);
            }
            
            dropped = EditorGUILayout.ObjectField(null, typeof(T), true, EditorGUIControls.DROP_WIDTH) as T;
        }
        GUILayout.EndHorizontal();
        
        return dropped;
    }
    
    
    // -------------------------------------------------------------------------------------------------------------
    public static T Field<T>(string label, T target) where T:class { return Field<T>(label, target, false); }
    public static T Field<T>(string label, T target, bool large) where T:class
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            if ( typeof(T) == typeof(string) )
            {
                target = EditorGUILayout.TextField(target as string) as T;
            }
            else
            {
                target = EditorGUILayout.ObjectField(target as UnityEngine.Object, typeof(T), true) as T;
            }
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static bool FieldBool(string label, bool target) { return FieldBool(label, target, false); }
    public static bool FieldBool(string label, bool target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.Toggle(target);
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static int FieldInt(string label, int target) { return FieldInt(label, target, false); }
    public static int FieldInt(string label, int target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.IntField(target);
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static float FieldFloat(string label, float target) { return FieldFloat(label, target, false); }
    public static float FieldFloat(string label, float target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.FloatField(target);
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
//        quad.iWidth = EditorGUIControls.FloatSlider("Width:", quad.iWidth, 0.1f, 1920.0f, 0.1f);
    public static float FieldSlider(string label, float target, float min, float max)                                 { return FieldSlider(label, target, min, max, false, 0.0f, false); }
    public static float FieldSlider(string label, float target, float min, float max, float precisescale)             { return FieldSlider(label, target, min, max, true, precisescale, false); }
    public static float FieldSlider(string label, float target, float min, float max, bool large)                     { return FieldSlider(label, target, min, max, false, 0.0f, large); }
    public static float FieldSlider(string label, float target, float min, float max, float precisescale, bool large) { return FieldSlider(label, target, min, max, true, precisescale, large); }
    public static float FieldSlider(string label, float target, float min, float max, bool useprecise, float precisescale, bool large)
    {
        string more = ">";
        string less = "<";
        
        if ( Event.current.shift && !Event.current.alt )
        {
            precisescale *= 10.0f;
        }
        
        if ( Event.current.alt && !Event.current.shift )
        {
            precisescale *= 0.1f;
        }
        
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            if ( precisescale > 0.0f && GUILayout.Button(less, SMALL_BUTTON_WIDTH) )
            {
                target -= precisescale;
                GUI.changed = true;
            }
            
            target = GUILayout.HorizontalSlider(target, min, max);
            
            string targetstr = target.ToString();
            targetstr = GUILayout.TextField(targetstr, MEDIUM_BUTTON_WIDTH);
            
            if ( float.TryParse(targetstr, out target) )
            {
                target = Mathf.Clamp(target, min, max);
            }
            
            if ( precisescale > 0.0f && GUILayout.Button(more, SMALL_BUTTON_WIDTH) )
            {
                target += precisescale;
                GUI.changed = true;
            }
        }
        GUILayout.EndHorizontal();
        
        return target;
        
    }

    // -------------------------------------------------------------------------------------------------------------
    public static Color FieldColor(string label, Color target) { return FieldColor(label, target, false); }
    public static Color FieldColor(string label, Color target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.ColorField(target);
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static Color FieldColorPlus(string label, Color target) { return FieldColorPlus(label, target, false); }
    public static Color FieldColorPlus(string label, Color target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.ColorField(target);
            
            if ( GUILayout.Button("<", SMALL_BUTTON_WIDTH) )
            {
                target = CLIPBOARD_COLOR;
                GUI.changed = true;
            }
            
            if ( GUILayout.Button("C", SMALL_BUTTON_WIDTH) )
            {
                CLIPBOARD_COLOR = target;
            }
        }
        GUILayout.EndHorizontal();
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static int FieldPopup(string label, int selected, string [] popupdata) { return FieldPopup(label, selected, popupdata, false); }
    public static int FieldPopup(string label, int selected, string [] popupdata, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            selected = EditorGUILayout.Popup(selected, popupdata);
        }
        GUILayout.EndHorizontal();
        
        return selected;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    //public static T FieldEnum<T>(string label, T target, bool large) where T : enum { }
    public static System.Enum FieldEnum(string label, System.Enum target) { return FieldEnum(label, target, false); }
    public static System.Enum FieldEnum(string label, System.Enum target, bool large)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            
            target = EditorGUILayout.EnumPopup(target);
        }
        GUILayout.EndHorizontal();            
        
        return target;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static void LabelDescription(string label, string description) { LabelDescription(label, description, false); } 
    public static void LabelDescription(string label, string description, bool large) 
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label, ( large ? LARGE_LABEL_WIDTH : LABEL_WIDTH ));
            GUILayout.Label(":", LABEL_SEPARATOR_WIDTH);
            GUILayout.Label(description);
        }
        GUILayout.EndHorizontal();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static void LabelCentered(string label)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label(label);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static void BoxMessage(string message)
    {
        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.FlexibleSpace();
        GUILayout.Label(message);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static void BoxWarning(string message)
    {
        Color color = GUI.color;
        
        GUI.color = Color.yellow;
        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.FlexibleSpace();
        GUI.color = Color.yellow;
        GUILayout.Label("\n" + message + "\n");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUI.color = color;
    }
    #endregion    
    
    #region Specifics
    // -------------------------------------------------------------------------------------------------------------
    public static void UndoSnapshotButton(Object target, string name)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            if ( GUILayout.Button("Save Snapshot") )
            {
                Undo.RegisterUndo(target, name);
            }
        }
        GUILayout.EndHorizontal();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static string ActionButtons(params string [] buttons)
    {
        int button = -1;
        
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            
            for ( int i = 0; i < buttons.Length; i++ )
            {
                if ( GUILayout.Button(buttons[i], BUTTON_WIDTH) )
                {
                    button = i;
                    break;
                }
            }
        }
        GUILayout.EndHorizontal();    
        
        if ( button < 0 )
        {
            return string.Empty;
        }
        
        return buttons[button];
    }
    #endregion
}
