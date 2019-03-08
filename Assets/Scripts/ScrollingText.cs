using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The movement speed.
    /// </summary>
    public float MoveSpeed = 5.0f;

    /* MEMBERS */
    /// <summary>
    /// The text component.
    /// </summary>
    private Text mText;

    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = mText.rectTransform.position;
        pos.y += MoveSpeed * Time.deltaTime;
        mText.rectTransform.position = pos;
	}
}
