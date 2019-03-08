using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The movement speed of the bullet.
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// The movement direction of the bullet.
    /// </summary>
    public Vector2 MoveDirection;

    /* MEMBBERS */
    private Collider2D mCollider;

    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        // listen for the player's death
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        PlayerControl player = obj.GetComponent<PlayerControl>();
        player.OnDeath += OnPlayerDeath;

        // get renderer
        mCollider = GetComponent<Collider2D>();

        // prevent static bullets from breaking the game
        if (MoveDirection == Vector2.zero) {
            Destroy(gameObject);
        } else {
            // ensure direction is normalized otherwise
            MoveDirection.Normalize();
        }
	}
	
	// Update is called once per frame
	void Update () {
        MoveBullet();
        CullBullet();
	}

    void OnDestroy() {
        // stop listening for the player's death
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null) {
            PlayerControl player = obj.GetComponent<PlayerControl>();
            player.OnDeath -= OnPlayerDeath;
        }
    }

    /* CALLBACK FUNCTIONS */
    /// <summary>
    /// Destroy self on player death.
    /// </summary>
    public void OnPlayerDeath() {
        Destroy(gameObject);
    }

    /* HELPER FUNCTIONS */
    /// <summary>
    /// Update the position of the bullet.
    /// </summary>
    private void MoveBullet() {
        // convert to 3D vector and add to current position
        Vector3 direction3D = new Vector3(MoveDirection.x, MoveDirection.y, 0.0f);
        transform.localPosition += direction3D * MoveSpeed * Time.deltaTime;

        // rotate in direction of movement
        float cosAngle = Vector3.Dot(direction3D, new Vector3(1.0f, 0.0f, 0.0f));
        if (direction3D.y < 0) {
            cosAngle = -cosAngle;
        }

        // BUBBLES DON'T ROTATE!
        //Vector3 rotation = transform.localEulerAngles;
        //rotation.z = Mathf.Rad2Deg * Mathf.Acos(cosAngle) + 90.0f;
        //transform.localEulerAngles = rotation;
    }

    /// <summary>
    /// Remove bullet if it left the screen.
    /// </summary>
    private void CullBullet() {
        // determine bounding points points
        Vector3 minPt = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxPt = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        // adjust min and max to ensure object is complete off screen
        float halfWidth = mCollider.bounds.extents.x;
        float halfHeight = mCollider.bounds.extents.y;

        minPt.x -= halfWidth;
        minPt.y -= halfHeight;

        maxPt.x += halfWidth;
        maxPt.y += halfHeight;

        // destroy object if out of bounds
        Vector3 position = mCollider.bounds.center;
        if (position.x < minPt.x || position.y < minPt.y ||
            position.x > maxPt.x || position.y > maxPt.y) {
            Destroy(gameObject);
        }
    }
}
