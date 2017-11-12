using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody2D rb2d;
	private int count = 0;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		countText.text = "0";
		winText.text = "";
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb2d.AddForce(speed * movement);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("PickUp")) {
			other.gameObject.SetActive(false);
			count++;
			countText.text = count.ToString();
			if (count >= 8) winText.text = "You win!";
		}
	}
}
