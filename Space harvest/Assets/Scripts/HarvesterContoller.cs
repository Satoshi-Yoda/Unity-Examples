using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterContoller : MonoBehaviour
{
	public float interval;

	private GameController gameController;
	private List<AsteroidController> asteroids = new List<AsteroidController>();

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Can not find GameController object");
		}
	}
	
	void Update() {
		foreach (AsteroidController asteroid in asteroids) {
			float amount = Time.deltaTime / (interval * asteroids.Count);
			asteroid.Harvest(amount);
			gameController.AddMinerals(amount);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			asteroids.Add(other.gameObject.GetComponent<AsteroidController>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			asteroids.Remove(other.gameObject.GetComponent<AsteroidController>());
		}
	}
}
