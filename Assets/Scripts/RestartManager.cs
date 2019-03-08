using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour {
    /// <summary>
    /// The scene to load when restarting.
    /// </summary>
    public string SceneName = "MainScene";

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneName);

			ScoreScript.scoreValue = 0;
        }
	}
}
