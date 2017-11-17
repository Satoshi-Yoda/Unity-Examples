using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldController : MonoBehaviour
{
	public int groupCount;
	public int groupSize;
	public float areaRadius;
	public float groupRadius;
	public GameObject prefab;

	void Start() {
		for (int k = 0; k < groupCount; k++) {
			Vector3 groupPosition = Random.insideUnitCircle * areaRadius;
			for (int i = 0; i < Random.Range(groupSize / 4, groupSize); i++) {
				Vector3 shift = Random.insideUnitCircle * groupRadius;
				Vector3 position = groupPosition + shift;
				if (position.magnitude > 2) {
					AsteroidController asteroid = (Instantiate(prefab, position, Quaternion.identity) as GameObject).GetComponent<AsteroidController>();
					asteroid.minerals = Random.Range(100, 2000);
				}
			}
		}
	}
}
