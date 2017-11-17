using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBoxController : MonoBehaviour
{
	public int targetEnergy;
	public GameObject construction;
	public GameObject progress;

	private int energy = 0;
	private int willEnergy = 0;

	public void Energize() {
		energy++;

		UpdateBar();

		if (energy >= targetEnergy) {
			Construct();
		}
	}

	public void WillEnergize() {
		willEnergy++;
	}

	public bool NeedMore() {
		return willEnergy < targetEnergy;
	}

	void Start() {
		UpdateBar();
	}

	void Construct() {
		Instantiate(construction, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void UpdateBar() {
		progress.transform.localScale = new Vector3((1.0f * energy) / targetEnergy, 1, 1);
	}
}
