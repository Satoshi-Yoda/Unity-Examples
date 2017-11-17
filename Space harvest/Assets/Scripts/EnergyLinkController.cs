using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLinkController : MonoBehaviour
{
	private float maxEPS;
	private List<EnergyLinkController> nearby = new List<EnergyLinkController>();

	public List<EnergyLinkController> GetNearby() {
		return nearby;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearby.Add(other.gameObject.GetComponent<EnergyLinkController>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearby.Remove(other.gameObject.GetComponent<EnergyLinkController>());
		}
	}
}
