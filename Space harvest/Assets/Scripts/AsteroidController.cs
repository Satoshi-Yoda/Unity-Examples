using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
	public GameObject[] asteroids_s4;
	public GameObject[] asteroids_s3;
	public GameObject[] asteroids_s2;
	public GameObject[] asteroids_s1;
	public int minerals;

	void Start() {
		GameObject[] asteroidPrefabs = asteroids_s1;
		if (minerals >= 1000) asteroidPrefabs = asteroids_s4;
		else if (minerals >= 500) asteroidPrefabs = asteroids_s3;
		else if (minerals >= 200) asteroidPrefabs = asteroids_s2;

		GameObject asteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
		Instantiate(asteroid, transform.position, Quaternion.identity);
	}
	
	void Update() { }
}
