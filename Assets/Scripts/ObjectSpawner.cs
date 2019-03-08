using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The objects that can be spawned.
    /// </summary>
    public GameObject[] objects;
    
    /// <summary>
    /// The set of spawn points.
    /// </summary>
    public Transform[] spawnPoints;

    /// <summary>
    /// The maximum wait time.
    /// </summary>
    public float MaxDelay = 2.0f;

    /// <summary>
    /// The minimum wait time.
    /// </summary>
    public float MinDelay = 0.1f;

    /// <summary>
    /// How much the spawn time can vary by.
    /// </summary>
    public float Variance = 0.25f;

    /// <summary>
    /// How long to wait until destroying the spawned item.
    /// </summary>
    public float DestroyTime = 5f;

    /* UNITY FUNCTIONS */
	void Start () {
		StartCoroutine(SpawnObjects());
	}

    /* HELPER FUNCTIONS */
    /// <summary>
    /// Spawns the objects.
    /// </summary>
    /// <returns>The coroutine.</returns>
    private IEnumerator SpawnObjects() {
        yield return new WaitForSeconds(2f); //wait for 2 seconds before spawning starts

        while (true) {
            // select random object
            GameObject anObject = objects[Random.Range(0, objects.Length)];

            // apply wait before spawning next object
            float avgWait = GetMaxWaitTime();
            float variance = GetVariance(avgWait);
            float delay = Random.Range(avgWait, avgWait);
            yield return new WaitForSeconds(delay);

            // spawn at a random spawn point and set to destroy after timeout
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            GameObject spawnedObject = Instantiate(anObject, spawnPoint.position, spawnPoint.rotation);
            Destroy(spawnedObject, DestroyTime);
        }
    }

    /// <summary>
    /// Get the maximum wait time based on the current score.
    /// </summary>
    /// <returns>The maximum wait time.</returns>
    private float GetMaxWaitTime() {
        // get the score
        int score = ScoreScript.scoreValue;

        // determine how long to wait
        float spread = MaxDelay - MinDelay;
        float delay = MaxDelay - (spread / 2.0f + spread * (Mathf.Atan(score / 25 - 10)) / Mathf.PI);

        return delay;
    }

    /// <summary>
    /// Get the variance based on the average wait time.
    /// </summary>
    /// <returns>The variance.</returns>
    private float GetVariance(float avgWait) {
        return Variance;
    }
}
