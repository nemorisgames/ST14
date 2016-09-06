// Marmoset Skyshop
// Copyright 2013 Marmoset LLC
// http://marmoset.co


using UnityEngine;
using System.Collections;

public class SkySwab : MonoBehaviour {	
	public mset.Sky targetSky = null;
	Vector3 scale = new Vector3(1.0f,1.01f,1.0f);
	Quaternion baseRot = Quaternion.identity;
	
	public Vector3 bigScale = new Vector3(1.2f,1.21f,1.2f);
	public Vector3 hoverScale = new Vector3(1f,1f,1f);
	public Vector3 littleScale = new Vector3(0.75f,0.76f,0.75f);
	
	// Use this for initialization
	void Start () {
		baseRot = transform.localRotation;
		scale = littleScale;
	}
	
	void OnMouseDown() {
		if(targetSky) targetSky.Apply();
	}
	
	void OnMouseOver() {
		scale = hoverScale;
	}
	
	void OnMouseExit() {
		scale = littleScale;
	}
	
	// Update is called once per frame
	void Update () {
		if( mset.Sky.activeSky == targetSky ) {
			transform.Rotate(0,200f*Time.deltaTime,0);
			transform.localScale = bigScale;
		} else {
			transform.localRotation = baseRot;
			transform.localScale = scale;
		}
        if (targetSky != null) {
			//apply the swab's target sky as the custom sky
			targetSky.Apply(this.GetComponent<Renderer>());
			//make the sky swabs unaffected by global exposure, we always need to see them
			targetSky.SetCustomExposure(this.GetComponent<Renderer>(), 1f, 1f, 1f, 1f);
        }
	}

    void OnDrawGizmos() {
	    if (targetSky != null) {
            targetSky.Apply(this.GetComponent<Renderer>());
        }
    }

}
