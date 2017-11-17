using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
	public float velocity;

	private float nextRetarget = 0.0f;
	private GameController gameController;
	public EnergyLinkController previous { get; set; }
	public EnergyLinkController target { get; set; }
	public ConstructionBoxController construction { get; set; }

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Can not find GameController object");
		}

		gameController.IncEnergy();

		GetComponent<Rigidbody2D>().angularVelocity = 180.0f;
		CalcVelocity();
	}

	void CalcVelocity() {
		if (target == null && construction == null) return;

		Vector3 selfPos = transform.position;
		Vector3 targetPos = (construction != null) ? construction.transform.position : target.transform.position;
		Vector2 delta = new Vector2(targetPos.x - selfPos.x, targetPos.y - selfPos.y);
		float magnitude = delta.magnitude;
		nextRetarget = magnitude / velocity;

		if (construction != null) {
			Invoke("Energize", nextRetarget);
		} else {
			Invoke("Retarget", nextRetarget);
		}

		GetComponent<Rigidbody2D>().velocity = delta.normalized * velocity;
	}

	void Retarget() {
		if (target == null && construction == null) {
			Destroy(gameObject);
			gameController.DecEnergy();
			return;
		}

		bool success = target.Retarget(this);

		if (!success) {
			Destroy(gameObject);
			gameController.DecEnergy();
			return;
		}

		CalcVelocity();
	}

	void Energize() {
		if (construction != null) {
			construction.Energize();
		}

		Destroy(gameObject);
		gameController.DecEnergy();
		return;
	}
}
