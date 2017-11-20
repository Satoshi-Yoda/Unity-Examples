using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConsumerController : MonoBehaviour
{
	public int targetEnergy;
	public int visualOverheadAllowance;
	public GameObject frame;
	public GameObject progress;

	private int willEnergy = 0;
	public int energy { get; private set; }

	public void Energize() {
		energy++;
		UpdateBar();
	}

	public void FullEnergize() {
		energy = targetEnergy;
		willEnergy = targetEnergy;
		UpdateBar();
	}

	public void Spend() {
		if (energy > 0) {
			energy--;
			willEnergy--;
			UpdateBar();
		}
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
		if (targetEnergy == 0 || (energy + visualOverheadAllowance >= targetEnergy)) {
			frame.transform.localScale = new Vector3(0, 0, 0);
			progress.transform.localScale = new Vector3(0, 0, 0);
		} else {
			frame.transform.localScale = new Vector3(1, 1, 1);
			progress.transform.localScale = new Vector3((1.0f * energy) / targetEnergy, 1, 1);
		}
	}
}
