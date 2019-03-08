using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastCreditItem : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The scene to load after the credits finish rolling.
    /// </summary>
    public string ReturnScene = "Menu";

    /* MEMBERS */
    private Text mText;

    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        // quick exit if pressing space
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadSceneAsync(ReturnScene);
        }

        // check if off screen, if it is reload the main scene
        float height = mText.rectTransform.sizeDelta.y;
        float position = mText.rectTransform.position.y;
        float bottom = position - height / 2.0f;

        if (bottom > Screen.height) {
            SceneManager.LoadScene(ReturnScene);
        }
	}
}
