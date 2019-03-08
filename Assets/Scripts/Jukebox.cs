using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

    /* GLOBALS */
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static Jukebox gInstance;

    /* PROPERTIES */
    /// <summary>
    /// The song that is playing.
    /// </summary>
    public AudioClip CurrentSong;

    /// <summary>
    /// Song name that uniquely identifies that song.
    /// </summary>
    //public string CurrentSongName;

    /* GLOBAL FUNCTIONS */
    /// <summary>
    /// Get an instance of the Jukebox.
    /// </summary>
    /// <returns></returns>
    public static Jukebox GetInstance() {
        return gInstance;
    }

    /* UNITY FUNCTIONS */
    // Called when the object is initialized.
    void Awake () {
        if (gInstance == null) {
            gInstance = this;
        } else {
            // change song if different
            if (gInstance.CurrentSong != CurrentSong) {
                gInstance.PlaySong(CurrentSong);
            }

            Destroy(gameObject);
        }

        PlaySong(CurrentSong);
        DontDestroyOnLoad(gameObject);
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

    /* METHODS */
    public void PlaySong(AudioClip song) {
        CurrentSong = song;

        AudioSource source = GetComponent<AudioSource>();
        source.clip = CurrentSong;
        source.Play();
    }
}
