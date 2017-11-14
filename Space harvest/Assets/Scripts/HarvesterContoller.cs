using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterContoller : MonoBehaviour
{
	private List<GameObject> asteroids = new List<GameObject>();

	void Start () { }
	
	void Update() {
		//Debug.Log(asteroids.Count);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			asteroids.Add(other.gameObject);
		}
	}
}
