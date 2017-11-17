using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public Text mineralsText;
	public Text energyText;
	public Text requisitionText;
	public Text spentMineralsText;
	public Text spentEnergyText;
	public Text spentRequisitionText;
	public Text gameOverText;
	public Text enemiesText;
	public Text buildingText;
	public GameObject enemiesArrow;
	public GameObject gameOverButton;
	public GameObject gameOverImage;
	public GameObject youVinImage;
	public GameObject HarvesterPrototype;
	public GameObject SolarPanelPrototype;
	public GameObject EnergyLinkPrototype;
	public GameObject LaserTurretPrototype;
	public GameObject mainCamera;
	public GameObject victorySoundPrefab;
	public EnemyGenerator enemyGenerator;
	public float minerals;

	private PrototypeController activePrototype;
	private int energy = 0;
	private int requisition = 0;
	private int totalEnergy = 0;
	private float totalMinerals = 0.0f;
	private bool gameEnd = false;

	public void GameOver() {
		gameOverText.gameObject.SetActive(true);
		gameOverImage.SetActive(true);
		gameOverButton.SetActive(true);
		enemiesText.text = "";
		gameEnd = true;
	}

	public void Win() {
		Instantiate(victorySoundPrefab, Vector3.zero, Quaternion.identity);
		gameOverText.gameObject.SetActive(true);
		youVinImage.SetActive(true);
		gameOverButton.SetActive(true);
		enemiesText.text = "";
		gameEnd = true;
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void AddMinerals(float amount) {
		minerals += amount;
		totalMinerals += amount;
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
		totalEnergy++;
	}

	public void DecEnergy() {
		energy--;
	}

	public void AddRequisition(int amount) {
		requisition += amount;
	}

	public void SelectHarvesterPrototype() {
		if (activePrototype != null) Destroy(activePrototype.gameObject);
		activePrototype = Instantiate(HarvesterPrototype, new Vector3(999, 999, 0), Quaternion.identity).GetComponent<PrototypeController>();
		buildingText.text = "Harvester";
	}

	public void SelectSolarPanelPrototype() {
		if (activePrototype != null) Destroy(activePrototype.gameObject);
		activePrototype = Instantiate(SolarPanelPrototype, new Vector3(999, 999, 0), Quaternion.identity).GetComponent<PrototypeController>();
		buildingText.text = "Solar Panel";
	}

	public void SelectEnergyLinkPrototype() {
		if (activePrototype != null) Destroy(activePrototype.gameObject);
		activePrototype = Instantiate(EnergyLinkPrototype, new Vector3(999, 999, 0), Quaternion.identity).GetComponent<PrototypeController>();
		buildingText.text = "Energy Link";
	}

	public void SelectLaserTurretPrototype() {
		if (activePrototype != null) Destroy(activePrototype.gameObject);
		activePrototype = Instantiate(LaserTurretPrototype, new Vector3(999, 999, 0), Quaternion.identity).GetComponent<PrototypeController>();
		buildingText.text = "Laser Turret";
	}

	void Start() {
		gameOverText.gameObject.SetActive(false);
		gameOverImage.SetActive(false);
		youVinImage.SetActive(false);
		gameOverButton.SetActive(false);
		buildingText.text = "";
	}

	void Update() {
		if (gameEnd) return;

		mineralsText.text = "" + Mathf.Round(minerals);
		energyText.text = "" + energy;
		requisitionText.text = "" + requisition;
		gameOverText.text = "Score: " + (requisition * 10 + totalEnergy + Mathf.Round(totalMinerals));

		if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
            Destroy(activePrototype.gameObject);
            buildingText.text = "";
		}

		if (activePrototype != null) {
			spentMineralsText.text = "-" + activePrototype.mineralsConsumption;
			spentEnergyText.text = "-" + activePrototype.energyConsumption;
		} else {
			spentMineralsText.text = "";
			spentEnergyText.text = "";
		}

		List<GameObject> targets = new List<GameObject>();
		targets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		if (targets.Count == 0) {
			enemiesText.text = "Enemies approaching in " + Mathf.Round(enemyGenerator.nextWave - Time.timeSinceLevelLoad) + " seconds\nGet prepared!";
			enemiesArrow.SetActive(false);
		} else {
			Vector3 avgPos = Vector3.zero;
			foreach (GameObject enemy in targets) {
				avgPos += enemy.transform.position;
			}
			avgPos /= targets.Count;
			Vector3 cameraPos = mainCamera.transform.position + new Vector3(0, 5.5f, 0);
			Vector3 direction = avgPos - cameraPos;
			float angle = -Mathf.Atan2(direction.x, direction.y) * 180 / Mathf.PI;
			enemiesArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			enemiesText.text = "Watch out!";
			enemiesArrow.SetActive(true);
		}

		if (targets.Count == 0 && Time.timeSinceLevelLoad > enemyGenerator.lastWaveTime) {
			Win();
		}
	}
}
