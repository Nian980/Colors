using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The sound to play.
    /// </summary>
    public AudioClip Sound;

    /// <summary>
    /// Should the object destroy itself after playing the sound?
    /// </summary>
    public bool DestroyOnFinish = false;

    /* MEMBERS */
    /// <summary>
    /// The audio source.
    /// </summary>
    private AudioSource mSource;
    
    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        mSource = GetComponent<AudioSource>();
        mSource.PlayOneShot(Sound);
        if (DestroyOnFinish) {
            Destroy(gameObject, Sound.length + 1.0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
