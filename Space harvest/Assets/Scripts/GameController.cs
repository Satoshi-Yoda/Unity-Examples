using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Text mineralsText;
	public Text energyText;
	public GameObject HarvesterPrototype;
	public GameObject SolarPanelPrototype;
	public GameObject EnergyLinkPrototype;
	public GameObject LaserTurretPrototype;

	private GameObject activePrototype;

	private float minerals = 0.0f;
	private int energy = 0;

	public void AddMinerals(float amount) {
		minerals += amount;
	}

	public void IncEnergy() {
		energy++;
	}

	public void DecEnergy() {
		energy--;
	}

	public void SelectHarvesterPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(HarvesterPrototype, Vector3.zero, Quaternion.identity);
	}

	public void SelectSolarPanelPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(SolarPanelPrototype, Vector3.zero, Quaternion.identity);
	}

	public void SelectEnergyLinkPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(EnergyLinkPrototype, Vector3.zero, Quaternion.identity);
	}

	public void SelectLaserTurretPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(LaserTurretPrototype, Vector3.zero, Quaternion.identity);
	}

	void Start() { }
	
	void Update() {
		mineralsText.text = "" + Mathf.Round(minerals);
		energyText.text = "" + energy;

		if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
            Destroy(activePrototype);
		}
	}
}
