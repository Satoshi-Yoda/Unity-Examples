using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeController : MonoBehaviour
{
	public int mineralsConsumption;
	public int energyConsumption;
	public GameObject objectPrefab;
	public GameObject constructionBoxPrefab;

	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Can not find GameController object");
		}
	}

	void Update() {
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = 10.0f;
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

		if (screenPoint.y > 100) {
			if (Input.GetMouseButtonDown(0)) {
				if (gameController.SpendMinerals(mineralsConsumption)) {
		            ConstructionBoxController constructionBox = Instantiate(constructionBoxPrefab, transform.position, Quaternion.identity).GetComponent<ConstructionBoxController>();
		            if (constructionBox != null) {
			            constructionBox.construction = objectPrefab;
			            constructionBox.targetEnergy = energyConsumption;
			        }
		        }
			}
		}
	}
}
