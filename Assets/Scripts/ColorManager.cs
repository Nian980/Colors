using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    /* DELEAGATES */
    /// <summary>
    /// Delegate definition of a callback that is invoked when the color changes.
    /// </summary>
    /// <param name="color">The new color.</param>
    public delegate void ColorChangedCallback(Color color);

    /* EVENTS */
    /// <summary>
    /// An event that is invoked when the color changes.
    /// </summary>
    public event ColorChangedCallback ColorChanged;

    /* GLOBALS */
    /// <summary>
    /// The color manager instance.
    /// </summary>
    private static ColorManager gInstance;

    /// <summary>
    /// The set of possible colors.
    /// </summary>
    private static Color[] Colors = new Color[] {
		Color.cyan,
		new Color32(0,250,154,255),
		new Color32(230,230,250,255),
		new Color32(238,130,238,255),
		//new Color32(100,149,237,255)
		new Color32(255,182,193,255)
    };

    /* GLOBAL METHODS */
    /// <summary>
    /// Get the instance of the ColorManager.
    /// </summary>
    /// <returns>The manager.</returns>
    public static ColorManager GetInstance() {
        return gInstance;
    }

    /// <summary>
    /// Get a random color.
    /// </summary>
    /// <returns>A random color.</returns>
    public static Color GetRandomColor() {
        return Colors[Random.Range(0, Colors.Length)];
    }

    /* PROPERTIES */
    /// <summary>
    /// The current color.
    /// </summary>
    public Color CurrentColor;

    /* UNITY METHODS */
    void Awake () {
        gInstance = this;
        CurrentColor = GetRandomColor();
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gInstance == null) {
            gInstance = this;
        }
	}

    /* PUBLIC METHODS */
    public void ChangeColor(Color color) {
        if (ColorChanged != null) {
            CurrentColor = color;
            ColorChanged(color);
        }
    }
}
