using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
	public float velocity;

	private EnergyLinkController previous = null;
	private EnergyLinkController target = null;
	private float nextRetarget;

	public void SetTarget(EnergyLinkController newTarget) {
		target = newTarget;
		CalcVelocity();
	}

	void Update() {
		if (Time.time > nextRetarget) Retarget();
	}

	void CalcVelocity() {
		if (target == null) return;

		Vector3 selfPos = transform.position;
		Vector3 targetPos = target.transform.position;
		Vector2 delta = new Vector2(targetPos.x - selfPos.x, targetPos.y - selfPos.y);
		float magnitude = delta.magnitude;
		nextRetarget = Time.time + magnitude / velocity;
		GetComponent<Rigidbody2D>().velocity = delta.normalized * velocity;
	}

	void Retarget() {
		if (target == null) {
			Destroy(gameObject);
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
			return;
		}

		CalcVelocity();
	}
}
