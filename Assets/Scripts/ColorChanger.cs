using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
    /* PROPERTIES */
    /// <summary>
    /// The amount of time to change colors.
    /// </summary>
    public float TimeToChange = 0.5f;

    /// <summary>
    /// The current time in lerp.
    /// </summary>
    public float CurrentTime = 0.0f;

    /// <summary>
    /// The color to change to.
    /// </summary>
    public Color CurrentColor;

    /// <summary>
    /// Was this murdered?
    /// </summary>
    public bool WasMurdered;

    /* MEMBERS */
    /// <summary>
    /// The sprite renderer.
    /// </summary>
    private SpriteRenderer mRenderer;

    /// <summary>
    /// The colors to change to.
    /// </summary>
    private List<Color> mPendingColors = new List<Color>();

    /* UNITY FUNCTIONS */
    // Use this for initialization
    void Start () {
        // get components
        mRenderer = GetComponent<SpriteRenderer>();

        // register for color changes
        ColorManager manager = ColorManager.GetInstance();
        manager.ColorChanged += OnColorChanged;

        // select random color and apply it, but make sure it isn't the current color
        do {
            CurrentColor = ColorManager.GetRandomColor();
        }
        while (CurrentColor == manager.CurrentColor);

        SetColor(CurrentColor);
	}
	
	// Update is called once per frame
	void Update () {
        // stop if not changing colors
		if (mPendingColors.Count <= 0) {
            return;
        }

        // check if done changing
        if (CurrentTime >= TimeToChange) {
            CurrentColor = mPendingColors[0];
            mPendingColors.RemoveAt(0);
            SetColor(CurrentColor);
            CurrentTime = 0.0f;
            return;
        }

        // add to current time and compute time coefficient
        CurrentTime += Time.deltaTime;
        float time = CurrentTime / TimeToChange;

        Color newColor = mPendingColors[0];
        SetColor(Color.Lerp(CurrentColor, newColor, time));
    }

    // Called when the object is destroyed.
    void OnDestroy() {
        ColorManager.GetInstance().ColorChanged -= OnColorChanged;

        if (WasMurdered) {
            Color newColor;
            if (mPendingColors.Count > 0 && CurrentTime / TimeToChange > 0.5f) {
                newColor = mPendingColors[0];
            } else {
                newColor = CurrentColor;
            }

            ColorManager.GetInstance().ChangeColor(newColor);
        }
    }

    /* PUBLIC METHODS */
    /// <summary>
    /// Called when the color is changed.
    /// </summary>
    /// <param name="color">The new color.</param>
    public void OnColorChanged(Color color) {
        if (GetNextColor() != color) {
            return;
        }

        // choose a new color that isn't the background color
        Color next = GetNextColor();
        while (next == color) {
            next = ColorManager.GetRandomColor();
        }

        mPendingColors.Add(next);
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

    /// <summary>
    /// Get the next color.
    /// </summary>
    /// <returns>The next color.</returns>
    private Color GetNextColor() {
        if (mPendingColors.Count > 0) {
            return mPendingColors[mPendingColors.Count - 1];
        }

        return CurrentColor;
    }
}
