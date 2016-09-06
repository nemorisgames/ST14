using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;

// =================================================================================================================
class EWToolsRenderCubemap : ScriptableWizard
{
    private string StateFilename = "EWToolRenderCubemap.xml";
    
    // -------------------------------------------------------------------------------------------------------------
    public string Name;
    public string From;
    public eSize Size;
        
    // -------------------------------------------------------------------------------------------------------------
    [MenuItem ("Tools/Render Cubemap")]
    static void CreateWizard ()
    {
        EWToolsRenderCubemap wizard = ScriptableWizard.DisplayWizard ("Render Cubemap", typeof (EWToolsRenderCubemap), "Render") as EWToolsRenderCubemap;
  
        wizard.isValid = false;
        //wizard.helpString = "Select an object to use as the center of a cubemap";
        
        wizard.OnWizardUpdate();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        Name = state.GetState("Name", string.Empty);
        From = state.GetState("From", string.Empty);
        Size = (eSize)state.GetState("Size", 0);
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnDisable()
    {
        EditorSaveState state = new EditorSaveState(StateFilename);
        
        state.SetState("Name", Name);
        state.SetState("From", From);
        state.SetState("Size", (int)Size);
        
        state.Save();
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnWizardUpdate ()
    {
        isValid = false;
        
        if ( Name == String.Empty )
        {
            errorString = "The Name should have at least one character";
        }
        else if ( From == String.Empty )
        {
            errorString = "The From should specify a path to an object";
        }
        else if ( GameObject.Find(( From[0] == '/' ? From : "/" + From )) == null )
        {
            errorString = "The object '" + From + "' can't be found";
        }
        else if ( Size == eSize.None )
        {
            errorString = "The Size should be 32 or more";
        }
        else
        {
            errorString = string.Empty;
            isValid = true;
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    void OnWizardCreate ()
    {
        Cubemap cm = new Cubemap ((int)Size, TextureFormat.RGB24, false);
        GameObject go = new GameObject ("CubemapCamera") as GameObject;
    
        GameObject fromgo = GameObject.Find(( From[0] == '/' ? From : "/" + From ));    
        
        go.transform.position = fromgo.transform.position;
        go.transform.rotation = fromgo.transform.rotation;
        // pv
        go.transform.localScale = fromgo.transform.localScale;
        // pv
        
        go.AddComponent (typeof (Camera));
        
        go.GetComponent<Camera>().RenderToCubemap (cm);
        
        Color [] face = new Color[(int)Size * (int)Size];
        Color [] newface = new Color[(int)Size * (int)Size];
        
        string facename;
        Texture2D text2D = new Texture2D ((int)Size, (int)Size, TextureFormat.RGB24, false);
        
        foreach ( CubemapFace f in Enum.GetValues (typeof (CubemapFace)))
        {
            face = cm.GetPixels (f);
            
//            if ( f == CubemapFace.NegativeY || f == CubemapFace.PositiveY )
            {
                RevertFaceXY(face, newface);
                text2D.SetPixels(newface);
            }
//            else
//            {
//                text2D.SetPixels(face);
//            }
//
//            if ( f == CubemapFace.NegativeZ )
//            {
//                facename = CubemapFace.PositiveZ.ToString();
//            }
//            else if ( f == CubemapFace.PositiveZ )
//            {
//                facename = CubemapFace.NegativeZ.ToString();
//            }
//            else if ( f == CubemapFace.NegativeY )
//            {
//                facename = CubemapFace.PositiveY.ToString();
//            }
//            else if ( f == CubemapFace.PositiveY )
//            {
//                facename = CubemapFace.NegativeY.ToString();
//            }
//            else 
            {
                facename = f.ToString();
            }
            
            SaveFile (Name + facename, text2D.EncodeToPNG ());
        }        
        
        DestroyImmediate (text2D);
        DestroyImmediate (go);
        DestroyImmediate (cm);
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private void SaveFile (string filename, byte [] content)
    {
        File.WriteAllBytes (filename + ".png", content);
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private void RevertFaceX(Color [] face, Color [] newface)
    {
        for ( int y = 0; y < (int)Size; y++ )
        {
            for ( int x = 0; x < (int)Size; x++ )
            {
                newface[y * (int)Size + ((int)Size - 1 - x)] = face[y * (int)Size + x];
            }
        }
    }

    // -------------------------------------------------------------------------------------------------------------
    private void RevertFaceY(Color [] face, Color [] newface)
    {
        for ( int y = 0; y < (int)Size; y++ )
        {
            for ( int x = 0; x < (int)Size; x++ )
            {
                newface[((int)Size - 1 - y) * (int)Size + x] = face[y * (int)Size + x];
            }
        }
    }
    
    // -------------------------------------------------------------------------------------------------------------
    private void RevertFaceXY(Color [] face, Color [] newface)
    {
        for ( int y = 0; y < (int)Size; y++ )
        {
            for ( int x = 0; x < (int)Size; x++ )
            {
                newface[((int)Size - 1 - y) * (int)Size + ((int)Size - 1 - x)] = face[y * (int)Size + x];
            }
        }
    }

    // =================================================================================================================
    public enum eSize : int
    {
        None = 0
        ,x32 = 32
        ,x64 = 64
        ,x128 = 128
        ,x256 = 256
        ,x512 = 512
        ,x1024 = 1024
        ,x2048 = 2048
    }
}
