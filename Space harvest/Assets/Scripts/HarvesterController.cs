using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterController : MonoBehaviour
{
	public float interval;
	public float powerUpDelay;
	public float power;
	public GameObject rayPrefab;
	public GameObject beepPrefab;
	public EnergyConsumerController energyConsumer;

	private GameController gameController;
	private List<AsteroidController> asteroids = new List<AsteroidController>();
	private GameObject ray;
	private AsteroidController target;
	private float creationTime;
	private float minLifeTime = 5.0f;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Can not find GameController object");
		}

		creationTime = Time.timeSinceLevelLoad;
		StartCoroutine(ChangeTarget());
		energyConsumer.FullEnergize();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			asteroids.Add(other.gameObject.GetComponent<AsteroidController>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			asteroids.Remove(other.gameObject.GetComponent<AsteroidController>());
		}
	}

	IEnumerator ChangeTarget() {
		while (true) {
			List<AsteroidController> notEmpty = new List<AsteroidController>();
			foreach (AsteroidController asteroid in asteroids) {
				if (!asteroid.Empty()) notEmpty.Add(asteroid);
			}
			asteroids = notEmpty;

			if (asteroids.Count > 0) {
				if (energyConsumer.energy > 0) {
					AsteroidController oldTarget = target;
					for (int attempt = 0; attempt < 99; attempt++) {
						target = asteroids[Random.Range(0, asteroids.Count)];
						if (target != oldTarget) break;
					}
					float distance = (target.gameObject.transform.position - gameObject.transform.position).magnitude;
					Vector2 direction = (target.gameObject.transform.position - gameObject.transform.position).normalized;
					float angle = -Mathf.Atan2(direction.x, direction.y) * 180 / Mathf.PI;

					yield return new WaitForSeconds(powerUpDelay);
					if (ray == null) ray = Instantiate(rayPrefab, gameObject.transform.position, Quaternion.identity);
					ray.transform.parent = gameObject.transform;
					ray.transform.position = (target.gameObject.transform.position + gameObject.transform.position) / 2;
					ray.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					ray.transform.localScale = new Vector3(1, distance * 0.4f, 1);
					yield return new WaitForSeconds(interval - powerUpDelay);

					Destroy(ray);
					target.Harvest(power);
					gameController.AddMinerals(power);
					energyConsumer.Spend();
				} else {
					yield return new WaitForSeconds(powerUpDelay);
				}
			} else {
				target = null;
				Destroy(ray);
				if (Time.timeSinceLevelLoad > creationTime + minLifeTime) {
					Instantiate(beepPrefab, gameObject.transform.position, Quaternion.identity);
					Destroy(gameObject);
				}
				yield return new WaitForSeconds(powerUpDelay);
			}
		}
	}
}
