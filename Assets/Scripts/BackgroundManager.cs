using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    /* PROPERTIES */
    /// <summary>
    /// The background to loop.
    /// </summary>
    public GameObject Background;

    /// <summary>
    /// The maximum number of spawned backgrounds at once.
    /// </summary>
    public int MaxSpawn = 5;

    /// <summary>
    /// The speed that the background scrolls at.
    /// </summary>
    public float BackgroundScrollSpeed = 5.0f;

    /* MEMBERS */
    /// <summary>
    /// The set of spawned background.
    /// </summary>
    private List<BackgroundControl> mBackgrounds = new List<BackgroundControl>();

    /* UNITY FUNCTIONS */
	// Use this for initialization
	void Start () {
        // spawn first
        mBackgrounds.Clear();
        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        GameObject obj = Instantiate<GameObject>(Background);
        obj.transform.position = new Vector3(origin.x, 0, 0);
        mBackgrounds.Add(obj.GetComponent<BackgroundControl>());

        // spawn rest
        while (mBackgrounds.Count < MaxSpawn) {
            AddBackground();
        }
	}
	
	// Update is called once per frame
	void Update () {
        ScrollBackgrounds();
        CullBackgrounds();
        while (mBackgrounds.Count < MaxSpawn) {
            AddBackground();
        }
	}
    
    /* HELPER METHODS */
    /// <summary>
    /// Scroll all of the backgrounds.
    /// </summary>
    private void ScrollBackgrounds() {
        float distance = Time.deltaTime * BackgroundScrollSpeed;

        foreach (BackgroundControl background in mBackgrounds) {
            Vector3 position = background.transform.localPosition;
            position.x -= distance;
            background.transform.localPosition = position;
        }
    }

    /// <summary>
    /// Remove backgrounds that have gone off-screen.
    /// </summary>
    private void CullBackgrounds() {
        Vector3 offscreen = Camera.main.ScreenToWorldPoint(new Vector3(-1000.0f, 0, 0));

        // cull backgrounds (but never cull them all!)
        bool isDone = false;
        while (!isDone && mBackgrounds.Count > 1) {
            BackgroundControl first = mBackgrounds[0];

            // destroy if offscreen
            if (first.AttachPoint.transform.position.x < offscreen.x) {
                mBackgrounds.RemoveAt(0);
                Destroy(first.gameObject);
            } else {
                isDone = true;
            }
        }
    }

    /// <summary>
    /// Add a new background.
    /// </summary>
    private void AddBackground() {
        GameObject obj = Instantiate<GameObject>(Background);
        obj.transform.position = mBackgrounds[mBackgrounds.Count - 1].AttachPoint.transform.position;
        mBackgrounds.Add(obj.GetComponent<BackgroundControl>());
    }
}
