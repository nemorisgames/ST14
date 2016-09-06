using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// =================================================================================================================
public class ELogWindow : EditorWindow 
{
    private static ELogWindow Window;
    private const string StateFilename = "ELogWindow.xml";
    
    // -------------------------------------------------------------------------------------------------------------
    private string mTitle;
    private string mLog;
    private Vector2 mLogScrollViewPosition;
    
	// -------------------------------------------------------------------------------------------------------------
    public static void ShowLog (string title, string log) 
    {
        // Get existing open window or if none, make a new one:
        ELogWindow win = EditorWindow.GetWindow<ELogWindow>(true, "Log Window");
        
        Window = win;
        
        Window.mTitle = title;
        Window.mLog = log;
        Window.mLogScrollViewPosition = Vector2.zero;
        
        Window.OnDisable();
    }
    
    #region Initialization and State load/save
    // -------------------------------------------------------------------------------------------------------------
    void Awake()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        mTitle = state.GetState("mTitle", "<title>");
        mLog = state.GetState("mLog", string.Empty);
        
        mLogScrollViewPosition = Vector2.zero;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void Update () 
    {
        if ( mTitle == null )
        {
            Awake();
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnDisable()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        state.SetState("mTitle", mTitle);
        state.SetState("mLog", mLog);
        
        state.Save();
    }
    #endregion
    
    // -------------------------------------------------------------------------------------------------------------
    void OnGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUIControls.Title(mTitle);
            
            mLogScrollViewPosition = GUILayout.BeginScrollView(mLogScrollViewPosition, false, true);
            GUILayout.TextArea(mLog);
            GUILayout.EndScrollView();
        }    
    }
    
    #region Specific areas
    // -------------------------------------------------------------------------------------------------------------
//    private void GUISceneBox()
//    {
//        GUILayout.BeginVertical(GUI.skin.box);
//        {
//            EditorGUIControls.GUITitle("Scene");
//        
//            mTargetGameObject = EditorGUIControls.GUIField<GameObject>("Target", mTargetGameObject);
//            
//            GUILayout.BeginHorizontal();
//            {
//                GUILayout.FlexibleSpace();
//                if ( GUILayout.Button("Load", GUILayout.Width(BUTTON_WIDTH)) )
//                {
//                    if ( mTargetGameObject != null )
//                    {
//                        EditorHierarchyDescription description = new EditorHierarchyDescription("Object <" + mTargetGameObject.name + ">");
//                        mDescriptions.Add(description);
//                        
//                        AddLog(description.LoadFromGameObject(mTargetGameObject));
//                    }
//                    else
//                    {
//                        AddLog("Select a GameObject from the Scene", true);
//                    }
//                }
//            }
//            GUILayout.EndHorizontal();
//        }
//        GUILayout.EndVertical();
//    }
    #endregion
}
