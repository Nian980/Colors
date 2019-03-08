using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour {
    /* PROPERTIES */
    /// <summary>
    /// The active color.
    /// </summary>
    public Color CurrentColor;

    /// <summary>
    /// The amount of time to change colors.
    /// </summary>
    public float TimeToChange = 0.5f;

    /// <summary>
    /// The current time in lerp.
    /// </summary>
    public float CurrentTime = 0.0f;

    /* MEMBERS */
    /// <summary>
    /// The sprite renderer.
    /// </summary>
    private SpriteRenderer mRenderer;

    /// <summary>
    /// The colors to change to.
    /// </summary>
    private Queue<Color> mPendingColors = new Queue<Color>();
    
    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        // get components
        mRenderer = GetComponent<SpriteRenderer>();

        // register for color changes
        ColorManager.GetInstance().ColorChanged += OnColorChanged;

        // colorize if necessary
        CurrentColor = ColorManager.GetInstance().CurrentColor;
        SetColor(CurrentColor);
    }
	
	// Update is called once per frame
	void Update () {
        if (mPendingColors.Count <= 0) {
            return;
        }

        // if done lerping, remove pending item and prepare for next lerp
        if (CurrentTime >= TimeToChange) {
            CurrentColor = mPendingColors.Dequeue();
            SetColor(CurrentColor);
            CurrentTime = 0.0f;
            return;
        }

        // add to current time and compute time coefficient
        CurrentTime += Time.deltaTime;
        float time = CurrentTime / TimeToChange;

        Color newColor = mPendingColors.Peek();
        SetColor(Color.Lerp(CurrentColor, newColor, time));        
	}

    // Called when the object is destroyed.
    void OnDestroy() {
        ColorManager.GetInstance().ColorChanged -= OnColorChanged;
    }

    /* PUBLIC FUNCTIONS */
    /// <summary>
    /// Called when the color changes.
    /// </summary>
    /// <param name="color"></param>
    public void OnColorChanged(Color color) {
        mPendingColors.Enqueue(color);
    }

    /* HELPER FUNCTIONS */
    /// <summary>
    /// Set the color.
    /// </summary>
    /// <param name="color">The new color.</param>
    private void SetColor(Color color) {
        if (mRenderer != null) {
            mRenderer.color = color;
        }
    }
}
