using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speed;

	void Update () {
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		transform.position = new Vector3(transform.position.x + speed * moveHorizontal, transform.position.y + speed * moveVertical, transform.position.z);
	}
}
