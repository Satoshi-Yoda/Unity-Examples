using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConsumerController : MonoBehaviour
{
	public int targetEnergy;
	public GameObject progress;

	private int willEnergy = 0;
	public int energy { get; private set; }

	public void Energize() {
		energy++;
		UpdateBar();
	}

	public void WillEnergize() {
		willEnergy++;
	}

	public bool NeedMore() {
		return willEnergy < targetEnergy;
	}

	public bool Full() {
		return energy >= targetEnergy;
	}

	void Start() {
		UpdateBar();
	}

	void UpdateBar() {
		progress.transform.localScale = new Vector3((1.0f * energy) / targetEnergy, 1, 1);
	}
}
