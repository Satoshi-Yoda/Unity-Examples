using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
	public float velocity;

	private EnergyLinkController previous = null;
	private EnergyLinkController target = null;
	private float nextRetarget = 0;
	private GameController gameController;

	public void SetTarget(EnergyLinkController newTarget) {
		target = newTarget;
		CalcVelocity();
	}

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Can not find GameController object");
		}

		gameController.IncEnergy();
		StartCoroutine(RetargetCoroutine());

		GetComponent<Rigidbody2D>().angularVelocity = 180.0f;
	}

	IEnumerator RetargetCoroutine() {
		while (true) {
			Retarget();
			yield return new WaitForSeconds(nextRetarget);
		}
	}

	void CalcVelocity() {
		if (target == null) return;

		Vector3 selfPos = transform.position;
		Vector3 targetPos = target.transform.position;
		Vector2 delta = new Vector2(targetPos.x - selfPos.x, targetPos.y - selfPos.y);
		float magnitude = delta.magnitude;
		nextRetarget = magnitude / velocity;
		GetComponent<Rigidbody2D>().velocity = delta.normalized * velocity;
	}

	void Retarget() {
		if (target == null) {
			Destroy(gameObject);
			gameController.DecEnergy();
			return;
		}

		List<EnergyLinkController> nearby = target.GetNearby();
		List<EnergyLinkController> candidates = new List<EnergyLinkController>();

		foreach (EnergyLinkController link in nearby) {
			if (link != target && link != previous) {
				candidates.Add(link);
			}
		}

		if (candidates.Count == 0) {
			foreach (EnergyLinkController link in nearby) {
				if (link != target) {
					candidates.Add(link);
				}
			}
		}

		if (candidates.Count > 0) {
			previous = target;
			target = candidates[Random.Range(0, candidates.Count)];
		} else {
			Destroy(gameObject);
			gameController.DecEnergy();
			return;
		}

		CalcVelocity();
	}
}
