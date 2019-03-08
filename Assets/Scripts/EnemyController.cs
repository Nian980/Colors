using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The death particle effect.
    /// </summary>
    public GameObject DeathVFX;

    /// <summary>
    /// The sound player object.
    /// </summary>
    public GameObject SoundPlayer;

    /// <summary>
    /// The death sound.
    /// </summary>
    public AudioClip DeathSound;

    /// <summary>
    /// How many points this is worth.
    /// </summary>
    public int ScoreValue = 10;

    /// <summary>
    /// The speed the object moves at.
    /// </summary>
    public float moveSpeed = 10f;

    /* MEMBERS */
    /// <summary>
    /// The movement direction.
    /// </summary>
    private float mMoveDirection = -1f;

    /// <summary>
    /// Was the enemy murdered?
    /// </summary>
    private bool mWasMurdered;

    /* UNITY FUNCTIONS */
    void Start() {
        SoundPlayer = Resources.Load<GameObject>("PlaySoundOnce");
    }

    // Called once per frame.
    void Update()
    {
        MoveEnemy();
    }

    // Called when the enemy enters a trigger.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // tell the colors change
            ColorChanger colorChanger = GetComponent<ColorChanger>();
            if (colorChanger != null)
            {
                colorChanger.WasMurdered = true;
            }

            // set our own was murdered flag
            mWasMurdered = true;

            // destroy the object
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // adds 10 to score
            ScoreScript.scoreValue += ScoreValue;

			// adds to high score if highScoreValue > scoreValue
			if(ScoreScript.scoreValue > HighScoreScript.highScoreValue){
				HighScoreScript.highScoreValue = ScoreScript.scoreValue;
			}

        }
    }

    // Called when the enemy is killed.
    void OnDestroy() {
        if (mWasMurdered) {
            GameObject soundPlayerObj = Instantiate<GameObject>(SoundPlayer);
            PlaySound player = soundPlayerObj.GetComponent<PlaySound>();
            player.Sound = DeathSound;
            player.transform.position = transform.position;

            GameObject obj = Instantiate<GameObject>(DeathVFX);
            obj.transform.position = transform.position;
        }
    }

    /* HELPER FUNCTIONS */
    private void MoveEnemy()
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x += moveSpeed * mMoveDirection * Time.deltaTime; //update position every frame

        transform.localPosition = newPosition;
    }

}
