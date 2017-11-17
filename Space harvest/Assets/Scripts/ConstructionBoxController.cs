using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBoxController : MonoBehaviour
{
	public int targetEnergy;
	public GameObject construction;
	public GameObject progress;

	private int energy = 0;

	public void Energize() {
		energy++;

		UpdateBar();

		if (energy >= targetEnergy) {
			Construct();
		}
	}

	void Start() {
		UpdateBar();
	}

	void Construct() {
		Instantiate(construction, transform.position, Quaternion.identity);
		transform.position = new Vector2(100000, 100000);
		Invoke("Remove", 1);
	}

	void Remove() {
		Destroy(this);
	}

	void UpdateBar() {
		progress.transform.localScale = new Vector3((3.0f * energy) / targetEnergy, 3, 3);
	}
}
