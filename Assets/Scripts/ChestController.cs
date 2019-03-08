using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The audio player that is spawned on destruction.
    /// </summary>
    public GameObject SoundPlayer;

    /// <summary>
    /// The sound that is played when the chest is collected.
    /// </summary>
    public AudioClip PopSound;

    /// <summary>
    /// How many points this is worth.
    /// </summary>
    public int ScoreValue = 100;

    /// <summary>
    /// The movement speed.
    /// </summary>
	public float MoveSpeed = 10f;

    /// <summary>
    /// Was the chest collected?
    /// </summary>
    public bool WasCollected;

    /* MEMBERS */
    /// <summary>
    /// The direction the chest moves in.
    /// </summary>
	private float mMoveDirection = -1f;
	
    /* UNITY FUNCTIONS */
    // Initialize the object here.
    void Start() {
        SoundPlayer = Resources.Load<GameObject>("PlaySoundOnce");
    }

    // Called once each frame.
    void Update() {
		MoveChest();
	}

    // Called when this collides with a trigger.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            ScoreScript.scoreValue += ScoreValue;
            WasCollected = true;
            Destroy(gameObject);
        }

		// adds to high score if highScoreValue > scoreValue
		if(ScoreScript.scoreValue > HighScoreScript.highScoreValue){
			HighScoreScript.highScoreValue = ScoreScript.scoreValue;
		}
    }

    // Called when chest is collected.
    void OnDestroy() {
        if (WasCollected) {
            GameObject soundPlayerObj = Instantiate<GameObject>(SoundPlayer);
            PlaySound player = soundPlayerObj.GetComponent<PlaySound>();
            player.Sound = PopSound;
            player.transform.position = transform.position;
        }
    }

    /* HELPER FUNCTIONS */
    /// <summary>
    /// Update the chest location.
    /// </summary>
	private void MoveChest() {
		Vector3 newPosition = transform.localPosition;
		newPosition.x += MoveSpeed * mMoveDirection * Time.deltaTime; //update position every frame

		transform.localPosition = newPosition;
	}
}
