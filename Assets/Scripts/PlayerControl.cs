using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    /* DELEGATES */
    /// <summary>
    /// A delegate that is invoked when the player dies.
    /// </summary>
    public delegate void OnDeathCallback();

    /// <summary>
    /// An event that is invoked when the player dies.
    /// </summary>
    public event OnDeathCallback OnDeath;

    /* PROPERTIES */
    /// <summary>
    /// The game over screen.
    /// </summary>
    public GameObject GameOverScreen;

    /// <summary>
    /// The death particle effect.
    /// </summary>
    public GameObject DeathSFX;

    /// <summary>
    /// The sound player that plays the death sound.
    /// </summary>
    public GameObject SoundPlayer;

    /// <summary>
    /// The end of the gun.
    /// </summary>
    public GameObject GunEnd;

    /// <summary>
    /// The bullet item to spawn.
    /// </summary>
    public GameObject Bullet;

    /// <summary>
    /// The sound that is played when Bean dies.
    /// </summary>
    public AudioClip DeathSound;

    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// The cooldown time of firing a bullet.
    /// </summary>
    public float FireCooldown = 0.5f;

    /// <summary>
    /// How much longer the player has to wait to fire.
    /// </summary>
    public float RemainFireTime;

    /// <summary>
    /// Did Bean die or are we just exiting?
    /// </summary>
    public bool DidDie;

    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        SoundPlayer = Resources.Load<GameObject>("PlaySoundOnce");
        GameOverScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        RemainFireTime -= Time.deltaTime;
        MovePlayer();
        FireGun();
	}

    // Called when the player dies.
    void OnDestroy() {
        if (DidDie) {
            GameObject soundPlayerObj = Instantiate<GameObject>(SoundPlayer);
            PlaySound player = soundPlayerObj.GetComponent<PlaySound>();
            player.Sound = DeathSound;
            player.transform.position = transform.position;

            GameObject obj = Instantiate<GameObject>(DeathSFX);
            obj.transform.position = transform.position;
        }

        // notify listeners that the player died
        if (OnDeath != null) {
            OnDeath();
        }
    }

    /* HELPER FUNCTIONS */
    /// <summary>
    /// Move the player according to the current input.
    /// </summary>
    private void MovePlayer() {
        // get the movement direction and convert it to a 3D vector
        Vector2 moveDir = GetMoveDirection();
        Vector3 moveDir3D = new Vector3(moveDir.x, moveDir.y, 0.0f);

        // determine the new position
        Vector3 newPosition = transform.localPosition + (moveDir3D * MoveSpeed * Time.deltaTime);

        // update the position
        transform.localPosition = ClampInsideScreen(newPosition);
    }

    /// <summary>
    /// Fire the gun if clicking.
    /// </summary>
    private void FireGun()
    {
        // do nothing if not currently firing
        if (!Input.GetMouseButtonDown(0) || RemainFireTime > 0.0f) {
            return;
        }

        // reset the cooldown timer
        RemainFireTime = FireCooldown;
        
        // determine direction towards mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        if (direction != Vector3.zero) {
            direction.Normalize();
        }

        // fire bullet towards direction
        GameObject obj = Instantiate<GameObject>(Bullet);
        BulletControl bullet = obj.GetComponent<BulletControl>();
        bullet.transform.localPosition = GunEnd.transform.position;
        bullet.MoveDirection = direction;
    } 

    /// <summary>
    /// Determine the move direction based on the keys that are currently held down.
    /// </summary>
    /// <returns>The movement direction</returns>
    private Vector2 GetMoveDirection() {
        int vertDireciton = 0;
        int horizDirection = 0;

        // determine inputs
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            ++vertDireciton;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            --vertDireciton;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ++horizDirection;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            --horizDirection;
        }

        Vector2 dir = new Vector2(horizDirection, vertDireciton);
        if (dir != Vector2.zero) {
            dir.Normalize();
        }

        return dir;
    }

    /// <summary>
    /// Clamp a player position to remain inside of the screen bounds.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>The clamped position</returns>
    private Vector2 ClampInsideScreen(Vector2 position) {
        Vector3 worldMax = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0.0f));
        Vector3 worldMin = Camera.main.ScreenToWorldPoint(
            new Vector3(0.0f, 0.0f, 0.0f));

        // determine the size of the sprite
        Renderer renderer = GetComponent<Renderer>();
        float halfWidth = renderer.bounds.extents.x;
        float halfHeight = renderer.bounds.extents.y;

        // adjust max and min to keep object completely in bounds
        worldMax.x -= halfWidth;
        worldMax.y -= halfHeight;

        worldMin.x += halfWidth;
        worldMin.y += halfHeight;

        return new Vector2(Mathf.Clamp(position.x, worldMin.x, worldMax.x),
                           Mathf.Clamp(position.y, worldMin.y, worldMax.y));
    }

    //Destroy player if it touches enemy
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy") {
			Destroy(gameObject);
            GameOverScreen.SetActive(true);
            DidDie = true;
		}
	}
}
