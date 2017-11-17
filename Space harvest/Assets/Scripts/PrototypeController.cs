using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeController : MonoBehaviour
{
	public GameObject objectPrefab;
	public GameObject constructionBoxPrefab;

	void Start() { }
	
	void Update() {
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = 10.0f;
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

		if (screenPoint.y > 100) {
			if (Input.GetMouseButtonDown(0)) {
	            ConstructionBoxController constructionBox = Instantiate(constructionBoxPrefab, transform.position, Quaternion.identity).GetComponent<ConstructionBoxController>();
	            constructionBox.construction = objectPrefab;
			}
		}
	}
}
