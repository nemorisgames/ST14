using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class cInputDemoRestart : MonoBehaviour {

	public GUIText resetText;

	void Start() {
		resetText.enabled = false;
	}

	void OnMouseDown() {
		SceneManager.LoadScene("Main (C#)");
	}
}
