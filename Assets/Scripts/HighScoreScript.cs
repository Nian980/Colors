using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

	public static int highScoreValue = 0;
	Text highScore;

	// Use this for initialization
	void Start () {
		highScore = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		highScore.text = "HIGH SCORE: " + highScoreValue;
	}
}