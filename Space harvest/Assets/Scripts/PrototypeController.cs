using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeController : MonoBehaviour
{
	public GameObject objectPrefab;

	void Start() { }
	
	void Update() {
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = 10.0f;
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

		if (Input.GetMouseButtonDown(0)) {
            Instantiate(objectPrefab, transform.position, Quaternion.identity);
		}
	}
}
