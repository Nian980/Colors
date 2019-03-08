using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour {
    /* MEMBERS */
    /// <summary>
    /// The lifespan of the object.
    /// </summary>
    public float Lifespan = 2.0f;

    /* UNITY FUNCTIONS */
    // Use this for initialization
    void Start () {
        Destroy(gameObject, Lifespan);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
