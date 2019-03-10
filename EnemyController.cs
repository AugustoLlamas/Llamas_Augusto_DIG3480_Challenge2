using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;

    public float speed = 1.0f;
    private float startTime;
    private float journeylength;

	
	void Start () {
        startTime = Time.time;
        journeylength = Vector2.Distance(startMarker.position, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeylength;

        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
	}
}
