using UnityEditor;
using UnityEngine;
using System.Collections;

// =================================================================================================================
public partial class EHierarchyActions 
{
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Hierarchy Actions/Show objects' paths", false, 50)]
    static void ShowObjectsPaths()
    {
        GameObject [] gos = Selection.gameObjects;
        string paths = string.Empty;
        
        foreach ( GameObject go in gos )
        {
            if ( paths != string.Empty )
            {
                paths += "\n";
            }
            
            paths += EditorUtilities.PathTo(go);
        }
        
        ELogWindow.ShowLog("Objects' paths", paths);
        //Debug.Log(paths);
    }
    
    #region New Group
    // -------------------------------------------------------------------------------------------------------------
//    [MenuItem ("Hierarchy Actions/New Empty Group", true)]
//    static bool NewEmptyGroupValidate()
//    {
//        return Selection.gameObjects.Length == 1;
//    }
    
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Hierarchy Actions/New Empty Group", false, 101)]
    static void NewEmptyGroup()
    {
        GameObject go = Selection.activeGameObject;
        
        GameObject newgroup = new GameObject("NewGroup");
        newgroup.transform.parent = ( go != null ? go.transform : null );

        Undo.RegisterCreatedObjectUndo(newgroup, "Undo Create New Group");
        
        Selection.objects = new Object[] { newgroup };
    }
    #endregion
 
    #region Group Selected
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem("Hierarchy Actions/Group Selected", true)]
    static bool GroupValidate()
    {
        GameObject [] gos = Selection.gameObjects;
        
        if ( gos.Length == 0 )
        {
            return false;
        }
        
        bool sameparent = true;
        Transform lastparent = gos[0].transform.parent;
        
        foreach ( GameObject go in gos )
        {
            if ( go.transform.parent != lastparent )
            {
                sameparent = false;
                break;
            }
        }
  
        return sameparent;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Hierarchy Actions/Group Selected", false, 102)]
    static void Group()
    {
        Undo.RegisterSceneUndo("Undo Group Selected");
        
        GameObject [] gos = Selection.gameObjects;
        GameObject newgroup = new GameObject("NewGroup");
        
        newgroup.transform.parent = Selection.gameObjects[0].transform.parent;
        newgroup.transform.localPosition = Vector3.zero;
        newgroup.transform.localRotation = Quaternion.identity;
        newgroup.transform.localScale = Vector3.one;
        
        foreach ( GameObject go in gos )
        {
            go.transform.parent = newgroup.transform;
        }
        
        Selection.objects = new Object[] { newgroup };
    }
    #endregion
    
    #region Destroy Group
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem("Hierarchy Actions/Destroy Group", true)]
    static bool DestroyGroupValidate()
    {
        return Selection.gameObjects.Length == 1;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem("Hierarchy Actions/Destroy Group", false, 103)]
    static void DestroyGroup()
    {
        Undo.RegisterSceneUndo("Undo Destroy Group");
        
        GameObject go = Selection.activeGameObject;
        
        Transform [] children = new Transform[go.transform.childCount];
        
        int i = 0;
        foreach ( Transform tr in go.transform )
        {
            children[i++] = tr;
        }
        
        foreach ( Transform tr in children )
        {
            tr.parent = go.transform.parent;
        }
        
        GameObject.DestroyImmediate(Selection.activeGameObject);
    }
    #endregion
}
