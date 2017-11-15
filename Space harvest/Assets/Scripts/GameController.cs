using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Text mineralsText;

	private float minerals = 0.0f;

	public void AddMinerals(float amount) {
		minerals += amount;
	}

	void Start() { }
	
	void Update() {
		mineralsText.text = "" + Mathf.Round(minerals);
	}
}
