using UnityEngine;
using System.Collections;

// =================================================================================================================
[ExecuteInEditMode]
public class TrainingRoomSceneData : MonoBehaviour 
{
    // -------------------------------------------------------------------------------------------------------------
    public Color BackgroundColor = Color.black;
    public Color AmbientLight = Color.white;
    public float ShadowDistance = 50.0f;
    
    // -------------------------------------------------------------------------------------------------------------
    public float MaxShadowDistance = 100.0f;
    
    // -------------------------------------------------------------------------------------------------------------
    public void Apply()
    {
        if ( Camera.main != null ) { Camera.main.backgroundColor = this.BackgroundColor; }
        RenderSettings.ambientLight = this.AmbientLight;
        QualitySettings.shadowDistance = this.ShadowDistance;
    }
    
    // -------------------------------------------------------------------------------------------------------------
    public void Obtain()
    {
        this.BackgroundColor = ( Camera.main != null ? Camera.main.backgroundColor : Color.black );
        this.AmbientLight = RenderSettings.ambientLight;
        this.ShadowDistance = QualitySettings.shadowDistance;    
        this.MaxShadowDistance = Mathf.Floor(Mathf.Clamp(this.ShadowDistance * 2.0f, 2.0f, 1000.0f));    
    }
}
