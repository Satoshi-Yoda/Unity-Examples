using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelController : MonoBehaviour
{
	public float interval;
	public GameObject energyPrefab;

	private List<EnergyLinkController> nearbyLinks = new List<EnergyLinkController>();

	void Start() {
		StartCoroutine(SpawnEnergy());
	}

	IEnumerator SpawnEnergy() {
		while (true) {
			if (nearbyLinks.Count > 0) {
				EnergyController energy = Instantiate(energyPrefab, transform.position, Quaternion.identity).GetComponent<EnergyController>();
				energy.target = nearbyLinks[Random.Range(0, nearbyLinks.Count)];
			}
			yield return new WaitForSeconds(interval);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearbyLinks.Add(other.gameObject.GetComponent<EnergyLinkController>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearbyLinks.Remove(other.gameObject.GetComponent<EnergyLinkController>());
		}
	}
}
