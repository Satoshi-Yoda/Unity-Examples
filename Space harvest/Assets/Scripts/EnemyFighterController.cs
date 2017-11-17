using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterController : MonoBehaviour
{
	public float velocity;
	public float thrust;
	public float hull;
	public GameObject explosionPrefab;

	void Start() { }

	void Update() {
		if (hull <= 0) {
			Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		List<GameObject> targets = new List<GameObject>();
		targets.AddRange(GameObject.FindGameObjectsWithTag("Harvester"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("SolarPanel"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("EnergyLink"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("LaserTurret"));
		targets.AddRange(GameObject.FindGameObjectsWithTag("ConstructionBox"));

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
			if (nearestDistance < 0.3) {
				Instantiate(explosionPrefab, nearest.transform.position, Quaternion.identity);
				Destroy(nearest.gameObject);
			}
		}
	}
}
