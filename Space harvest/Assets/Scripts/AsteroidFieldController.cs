using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldController : MonoBehaviour
{
	public int count;
	public float radius;
	public GameObject prefab;

	void Start() {
		for (int i = 0; i < count; i++) {
			//Vector3 position = new Vector3(Random.Range(-radius, +radius), Random.Range(-radius, +radius), 0.0f);
			Vector3 position = Random.insideUnitCircle * radius;
			AsteroidController asteroid = (Instantiate(prefab, position, Quaternion.identity) as GameObject).GetComponent<AsteroidController>();
			asteroid.minerals = Random.Range(50, 1500);
		}
	}
}
