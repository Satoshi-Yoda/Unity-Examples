using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float x_min, x_max, z_min, z_max;

	public Vector3 Apply(Vector3 position) {
		return new Vector3(
			Mathf.Clamp(position.x, x_min, x_max),
			0.0f, 
			Mathf.Clamp(position.z, z_min, z_max)
		);
	}
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Start() { }
	
	void FixedUpdate() {
		var body = GetComponent<Rigidbody>();
		body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);
		body.position = boundary.Apply(body.position);
		body.rotation = Quaternion.Euler(0.0f, 0.0f, body.velocity.x * -tilt);
	}

	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}
}
