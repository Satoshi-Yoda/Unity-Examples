using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretController : MonoBehaviour
{
	public float dps;
	public float distance;
	public float energyConsumptionPerSecond;
	public GameObject flarePrefab;
	public GameObject rayPrefab;
	public GameObject boomPrefab;
	public EnergyConsumerController energyConsumer;

	public bool firing { get { return lastTarget != null; } }
	private GameObject flare;
	private GameObject ray;
	private GameObject boom;
	private GameObject lastTarget;
	private float fireTime = 0;

	void Start() {
		energyConsumer.FullEnergize();
	}

	void Update() {
		List<GameObject> targets = new List<GameObject>();
		targets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

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

			if (nearestDistance <= distance) {
				if (energyConsumer.energy > 0) {
					fireTime += Time.deltaTime;
					if (fireTime >= 1.0f / energyConsumptionPerSecond) {
						fireTime -= 1.0f / energyConsumptionPerSecond;
						energyConsumer.Spend();
					}

					Vector2 direction = (nearest.transform.position - gameObject.transform.position).normalized;
					float angle = -Mathf.Atan2(direction.x, direction.y) * 180 / Mathf.PI;

					nearest.GetComponent<EnemyFighterController>().hull -= dps * Time.deltaTime;

					if (flare == null) {
						flare = Instantiate(flarePrefab, gameObject.transform.position, Quaternion.identity);
						flare.transform.parent = gameObject.transform;
					}
					if (ray == null) {
						ray = Instantiate(rayPrefab, gameObject.transform.position, Quaternion.identity);
						ray.transform.parent = gameObject.transform;
					}

					if (lastTarget != nearest || boom == null) {
						Destroy(boom);
						boom = Instantiate(boomPrefab, nearest.transform.position, Quaternion.identity);
						boom.transform.parent = nearest.transform;
					}

					ray.transform.position = (nearest.transform.position + gameObject.transform.position) / 2;
					ray.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					ray.transform.localScale = new Vector3(1, nearestDistance * 0.4f, 1);

					lastTarget = nearest;
				} else {
					Destroy(flare);
					Destroy(ray);
					Destroy(boom);
					lastTarget = null;
				}
			} else {
				Destroy(flare);
				Destroy(ray);
				Destroy(boom);
				lastTarget = null;
			}
		} else {
			Destroy(flare);
			Destroy(ray);
			Destroy(boom);
			lastTarget = null;
		}
	}
}
