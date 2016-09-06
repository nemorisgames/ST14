using UnityEditor;
using UnityEngine;

using System.Collections;

[CustomEditor(typeof(TrainingRoomSceneData))]
public class ITrainingRoomSceneData : Editor 
{
    // -------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        
        TrainingRoomSceneData data = target as TrainingRoomSceneData;
        
        string action = EditorGUIControls.ActionButtons("Set", "Get");
        
        switch ( action )
        {
            case "Set": ActionSet(); break;
            case "Get": ActionGet(); break;
        }
        
        GUI.changed = false;
        
        data.BackgroundColor = EditorGUIControls.FieldColor("Background Color", data.BackgroundColor);
        data.AmbientLight = EditorGUIControls.FieldColor("Ambient Light", data.AmbientLight);
        
        GUILayout.BeginHorizontal();
        data.ShadowDistance = EditorGUIControls.FieldSlider("Shadow Distance", data.ShadowDistance, 1.0f, data.MaxShadowDistance);
        
        if ( GUILayout.Button("-", EditorGUIControls.SMALL_BUTTON_WIDTH) )
        {
            data.MaxShadowDistance = Mathf.Floor(Mathf.Clamp(data.MaxShadowDistance * 0.5f, 2.0f, 1000.0f));
        }
        
        if ( GUILayout.Button("+", EditorGUIControls.SMALL_BUTTON_WIDTH) )
        {
            data.MaxShadowDistance = Mathf.Floor(Mathf.Clamp(data.MaxShadowDistance * 2.0f, 2.0f, 1000.0f));
        }
        
        GUILayout.EndHorizontal();
        
        if ( GUI.changed )
        {
            ActionSet();
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private void ActionSet()
    {
        TrainingRoomSceneData data = target as TrainingRoomSceneData;
        
        data.Apply();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private void ActionGet()
    {
        TrainingRoomSceneData data = target as TrainingRoomSceneData;
        
        data.Obtain();
    }
}
