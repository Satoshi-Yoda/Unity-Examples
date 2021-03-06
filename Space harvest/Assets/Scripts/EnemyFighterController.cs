﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterController : MonoBehaviour
{
	public float velocity;
	public float thrust;
	public float hull;
	public int requisition;
	public float destroyDistance;
	public GameObject explosionPrefab;

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
		if (hull <= 0) {
			Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
			gameController.AddRequisition(requisition);
			Destroy(gameObject);
		}

		List<GameObject> targets = new List<GameObject>();
		targets.AddRange(GameObject.FindGameObjectsWithTag("Harvester"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("SolarPanel"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("EnergyLink"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("LaserTurret"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("Bomb"));
		if (targets.Count == 0) {
			targets.AddRange(GameObject.FindGameObjectsWithTag("ConstructionBox"));
		}

		if (targets.Count > 0) {
			GameObject nearest = targets[0];
			float nearestDistance = float.MaxValue;
			foreach (GameObject target in targets) {
				float distance = (target.transform.position - gameObject.transform.position).magnitude;
				if (distance < nearestDistance) {
					nearestDistance = distance;
					nearest = target;
				}
			}

			Vector2 direction = (nearest.transform.position - gameObject.transform.position).normalized;
			gameObject.GetComponent<Rigidbody2D>().AddForce(direction * thrust);
			gameObject.GetComponent<Rigidbody2D>().rotation = -Mathf.Atan2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y) * 180 / Mathf.PI;

			if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > velocity)
			gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized * velocity;
			if (nearestDistance < destroyDistance) {
				Instantiate(explosionPrefab, nearest.transform.position, Quaternion.identity);
				gameController.AddRequisition(requisition);
				BombController bomb = nearest.gameObject.GetComponent<BombController>();
				if (bomb != null) {
					bomb.WillDestroy();
				}
				Destroy(nearest.gameObject);
			}
		} else {
			gameController.GameOver();
		}
	}
}
