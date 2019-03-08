using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGame : MonoBehaviour {

    /// <summary>
    /// The scene to return to.
    /// </summary>
    public string ReturnScene = "Menu";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(ReturnScene);
        }
	}
}
