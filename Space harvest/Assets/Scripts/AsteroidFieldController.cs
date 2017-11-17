using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldController : MonoBehaviour
{
	public int groupCount;
	public int groupSize;
	public float areaRadius;
	public float groupRadius;
	public float minRadius;
	public int totalMinerals;
	public GameObject prefab;

	void Start() {
		List<AsteroidController> array = new List<AsteroidController>();
		for (int k = 0; k < groupCount; k++) {
			Vector3 groupPosition = Random.insideUnitCircle * areaRadius;
			for (int i = 0; i < Random.Range(groupSize / 4, groupSize); i++) {
				Vector3 shift = Random.insideUnitCircle * groupRadius;
				Vector3 position = groupPosition + shift;
				if (position.magnitude > minRadius) {
					AsteroidController asteroid = (Instantiate(prefab, position, Quaternion.identity) as GameObject).GetComponent<AsteroidController>();
					asteroid.minerals = Random.Range(10, 200);
					array.Add(asteroid);
				}
			}
		}

		float totalObtrained = 0.0f;
		foreach (AsteroidController asteroid in array) {
			totalObtrained += asteroid.minerals;
		}
		//Debug.Log("Minerals: " + totalObtrained);
		float koef = totalMinerals / totalObtrained;
		foreach (AsteroidController asteroid in array) {
			asteroid.minerals *= koef;
		}
	}
}
