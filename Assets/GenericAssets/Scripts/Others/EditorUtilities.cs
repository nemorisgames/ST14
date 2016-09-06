using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// =================================================================================================================
public class EditorUtilities
{
    // -------------------------------------------------------------------------------------------------------------
    public static string PathTo(GameObject go) { return PathTo(go.transform); }
    public static string PathTo(Transform tr)
    {
        List<string> names = new List<string>();
        
        while ( tr != null )
        {
            names.Insert(0, tr.gameObject.name);
            names.Insert(0, "/");
            
            tr = tr.parent;
        }
        
        return string.Join(string.Empty, names.ToArray());
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static string Number(int number)
    {
        if ( number == 0 )
        {
            return "0";
        }
        
        return string.Format("{0:###,###,###}", number);
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static List<GameObject> FindAll(Transform root, params string[] names)
    {
        List<GameObject> gameobjects = new List<GameObject>();
        
        FindAllRecursive(gameobjects, root, names);
            
        return gameobjects;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private static void FindAllRecursive(List<GameObject> gameobjects, Transform root, string[] names)
    {
        for ( int i = 0; i < names.Length; i++ )
        {
            if ( root.name == names[i] )
            {
                gameobjects.Add(root.gameObject);
            }
        }
        
        foreach ( Transform child in root )
        {
            FindAllRecursive(gameobjects, child, names);
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public static List<T> FindAll<T>(Transform root) where T : Component
    {
        List<T> components  = new List<T>();
        
        FindAllRecursive<T>(components, root);
        
        return components;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private static void FindAllRecursive<T>(List<T> components, Transform root) where T : Component
    {
        T component = root.gameObject.GetComponent<T>();
        
        if ( component != null )
        {
            components.Add(component);
        }
        
        foreach ( Transform child in root )
        {
            FindAllRecursive<T>(components, child);
        }
    }
    
}














