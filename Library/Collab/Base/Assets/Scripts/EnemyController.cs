using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour {

    public float moveSpeed = 10f;

    float moveDirection = -1f;


    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.x += moveSpeed * moveDirection * Time.deltaTime; //update position every frame

        transform.localPosition = newPosition;
    }

	//void OnCollisionEnter2D(Collision2D coll)
	//{
 //       if (coll.gameObject.tag == "Bullet")
 //       {
 //           Destroy(coll.gameObject);
 //       }
	//}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
