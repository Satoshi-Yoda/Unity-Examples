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
		if (target == null) return;

		Vector3 selfPos = transform.position;
		Vector3 targetPos = target.transform.position;
		Vector2 delta = new Vector2(targetPos.x - selfPos.x, targetPos.y - selfPos.y);
		float magnitude = delta.magnitude;
		nextRetarget = magnitude / velocity;
		Invoke("Retarget", nextRetarget);
		GetComponent<Rigidbody2D>().velocity = delta.normalized * velocity;
	}

	void Retarget() {
		if (target == null) {
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
}
