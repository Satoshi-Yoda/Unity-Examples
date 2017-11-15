using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
	public GameObject[] asteroids_s4;
	public GameObject[] asteroids_s3;
	public GameObject[] asteroids_s2;
	public GameObject[] asteroids_s1;
	public float minerals;
	public int size2;
	public int size3;
	public int size4;

	public int size;
	private GameObject visual;

	public void Harvest(float amount) {
		minerals -= amount;
	}

	void Start() {
		CreateVisual();
	}

	void Update() {
		if (minerals <= 0) {
			Destroy(gameObject);
		} else {
			int newSize = CalcSize();
			if (newSize != size) {
				CreateVisual();
			}
		}
	}

	void CreateVisual() {
		size = CalcSize();
		GameObject[]        asteroidPrefabs = asteroids_s1;
		if      (size == 2) asteroidPrefabs = asteroids_s2;
		else if (size == 3) asteroidPrefabs = asteroids_s3;
		else if (size == 4) asteroidPrefabs = asteroids_s4;

		if (visual != null) Destroy(visual);

		GameObject asteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
		visual = Instantiate(asteroid, transform.position, Quaternion.identity) as GameObject;
		visual.transform.parent = gameObject.transform;
	}

	int CalcSize() {
		int                           result = 1;
		if      (minerals >= size4) { result = 4; }
		else if (minerals >= size3) { result = 3; }
		else if (minerals >= size2) { result = 2; }
		return result;
	}
}
