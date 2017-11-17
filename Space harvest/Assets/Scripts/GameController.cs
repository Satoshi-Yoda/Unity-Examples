using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Text mineralsText;
	public Text energyText;
	public Text requisitionText;
	public Text spentMineralsText;
	public Text spentEnergyText;
	public Text spentRequisitionText;
	public GameObject HarvesterPrototype;
	public GameObject SolarPanelPrototype;
	public GameObject EnergyLinkPrototype;
	public GameObject LaserTurretPrototype;
	public float minerals;

	private PrototypeController activePrototype;
	private int energy = 0;
	private int requisition = 0;

	public void AddMinerals(float amount) {
		minerals += amount;
	}

	public bool SpendMinerals(float amount) {
		if (minerals >= amount) {
			minerals -= amount;
			return true;
		} else {
			return false;
		}
	}

	public void IncEnergy() {
		energy++;
	}

	public void DecEnergy() {
		energy--;
	}

	public void AddRequisition(int amount) {
		requisition += amount;
	}

	public void SelectHarvesterPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(HarvesterPrototype, Vector3.zero, Quaternion.identity).GetComponent<PrototypeController>();
	}

	public void SelectSolarPanelPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(SolarPanelPrototype, Vector3.zero, Quaternion.identity).GetComponent<PrototypeController>();
	}

	public void SelectEnergyLinkPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(EnergyLinkPrototype, Vector3.zero, Quaternion.identity).GetComponent<PrototypeController>();
	}

	public void SelectLaserTurretPrototype() {
		Destroy(activePrototype);
		activePrototype = Instantiate(LaserTurretPrototype, Vector3.zero, Quaternion.identity).GetComponent<PrototypeController>();
	}

	void Start() { }
	
	void Update() {
		mineralsText.text = "" + Mathf.Round(minerals);
		energyText.text = "" + energy;
		requisitionText.text = "" + requisition;

		if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
            Destroy(activePrototype.gameObject);
		}

		if (activePrototype != null) {
			spentMineralsText.text = "-" + activePrototype.mineralsConsumption;
			spentEnergyText.text = "-" + activePrototype.energyConsumption;
		} else {
			spentMineralsText.text = "";
			spentEnergyText.text = "";
		}
	}
}
