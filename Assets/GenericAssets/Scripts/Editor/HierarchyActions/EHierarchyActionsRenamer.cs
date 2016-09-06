using UnityEditor;
using UnityEngine;
using System.Collections;

// =================================================================================================================
public class EHierarchyActionsRenamer : EditorWindow
{
    private static EHierarchyActionsRenamer Window;
    private const string StateFilename = "EHierarchyActionsRenamer.xml";
    
    // -------------------------------------------------------------------------------------------------------------
    private string mRenameTo;
    private bool mEnumerate;
    
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Hierarchy Actions/Renamer")]
    static void Init()
    {
        Window = (EHierarchyActionsRenamer) EditorWindow.GetWindow(typeof(EHierarchyActionsRenamer), false, "Renamer");
        
//        Window.mRenameTo = string.Empty;
//        Window.mEnumerate = false;
        
//        Window.OnDisable();
    }
    
    #region Initialization and State load/save
    // -------------------------------------------------------------------------------------------------------------
    void Awake()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        mRenameTo = state.GetState("mRenameTo", string.Empty);
        mEnumerate = state.GetState("mEnumerate", false);
        
        //mLogScrollViewPosition = Vector2.zero;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void Update () 
    {
        if ( mRenameTo == null )
        {
            Awake();
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnDisable()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        state.SetState("mRenameTo", mRenameTo);
        state.SetState("mEnumerate", mEnumerate);
        
        state.Save();
    }
    #endregion    
    
    // -------------------------------------------------------------------------------------------------------------
    void OnGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);
        {
            mRenameTo = EditorGUIControls.Field<string>("Name:", mRenameTo);
            mEnumerate = EditorGUIControls.FieldBool("Enumerate:", mEnumerate);
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button("Rename", EditorGUIControls.BUTTON_WIDTH) )
                {
                    ActionRename();
                }
            }
            GUILayout.EndHorizontal();
        }    
        GUILayout.EndVertical();
    }    
    
    #region Action routines
    // -------------------------------------------------------------------------------------------------------------
    private void ActionRename()
    {
        GameObject [] gos = Selection.gameObjects;
        
        int number = 1;
        mRenameTo = ( mRenameTo == null || mRenameTo == string.Empty ? "<empty>" : mRenameTo );
        
        foreach ( GameObject go in gos )
        {
            go.name = mRenameTo + ( mEnumerate ? number.ToString() : string.Empty );
            
            number ++;
        }
    }
    #endregion
}
