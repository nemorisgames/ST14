using UnityEditor;
using UnityEngine;
using System.Collections;

// =================================================================================================================
public partial class MenuBarCommands
{
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Tools/Create/Scene Data", false, 1)]
    static void CreateSceneData()
    {
        GameObject scenedatago = GameObject.Find("/TrainingRoom/SceneData");
        
        if ( scenedatago != null )
        {
            EditorUtility.DisplayDialog("Scene Data", "Ya existe un objeto SceneData en:\n" + EditorUtilities.PathTo(scenedatago), "Aceptar");
            return;
        }
        
        GameObject trainingroomgo = GameObject.Find("/TrainingRoom");
        
        if ( trainingroomgo == null )
        {
            trainingroomgo = new GameObject("TrainingRoom");
        }
        
        scenedatago = GameObject.Find("/TrainingRoom/SceneData");
        
        if ( scenedatago == null )
        {
            scenedatago = new GameObject("SceneData");
            scenedatago.transform.parent = trainingroomgo.transform;
        }
        
        if ( scenedatago.GetComponent<TrainingRoomSceneData>() == null )
        {
            scenedatago.AddComponent<TrainingRoomSceneData>();
        }
    }
}