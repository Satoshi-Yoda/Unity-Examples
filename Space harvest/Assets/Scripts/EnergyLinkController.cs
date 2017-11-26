using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLinkController : MonoBehaviour
{
	public float cooling;
	public float maxTemperature;
	public GameObject energyLinkVisual;
	public GameObject energyLinkOverloadedVisual;
	public GameObject energyBoom;

	private GameObject visual;
	private float maxEPS;
	private List<EnergyLinkController> nearby = new List<EnergyLinkController>();
	private List<EnergyConsumerController> constructions = new List<EnergyConsumerController>();
	private float temperature = 0.0f;
	private bool lastOverloaded = false;

	public bool Retarget(EnergyController energy) {
		List<EnergyLinkController> candidates = new List<EnergyLinkController>();
		EnergyLinkController target = energy.target;
		EnergyLinkController previous = energy.previous;

		List<EnergyConsumerController> needMoreConstructions = new List<EnergyConsumerController>();
		foreach (EnergyConsumerController construnction in constructions) {
			if (construnction.NeedMore()) {
				needMoreConstructions.Add(construnction);
			}
		}

		if (needMoreConstructions.Count > 0) {
			EnergyConsumerController construnction = needMoreConstructions[Random.Range(0, needMoreConstructions.Count)];
			energy.construction = construnction;
			construnction.WillEnergize();
			return true;
		}

		foreach (EnergyLinkController link in nearby) {
			if (link != target && link != previous) {
				candidates.Add(link);
			}
		}

		if (candidates.Count == 0) {
			foreach (EnergyLinkController link in nearby) {
				if (link != target) {
					candidates.Add(link);
				}
			}
		}

		if (candidates.Count > 0) {
			energy.previous = target;
			energy.target = candidates[Random.Range(0, candidates.Count)];
			if (temperature > maxTemperature) {
				Instantiate(energyBoom, transform.position, Quaternion.identity);
				return false;
			} else {
				temperature += 1;
				return true;
			}
		} else {
			Instantiate(energyBoom, transform.position, Quaternion.identity);
			return false;
		}
	}

	void Start() {
		visual = Instantiate(energyLinkVisual, transform.position, Quaternion.identity) as GameObject;
		visual.transform.parent = gameObject.transform;
	}

	void Update() {
		bool overloaded = (temperature > (maxTemperature - cooling));

		if (overloaded && !lastOverloaded) {
			Destroy(visual);
			visual = Instantiate(energyLinkOverloadedVisual, transform.position, Quaternion.identity) as GameObject;
			visual.transform.parent = gameObject.transform;
		}

		if (!overloaded && lastOverloaded) {
			Destroy(visual);
			visual = Instantiate(energyLinkVisual, transform.position, Quaternion.identity) as GameObject;
			visual.transform.parent = gameObject.transform;
		}

		lastOverloaded = overloaded;

		temperature -= Time.deltaTime * cooling;
		if (temperature < 0) temperature = 0;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearby.Add(other.gameObject.GetComponent<EnergyLinkController>());
			// Debug.Log("Energy Link added");
		} else if (other.tag == "EnergyConsumer") {
			constructions.Add(other.gameObject.GetComponent<EnergyConsumerController>());
			// Debug.Log("ConstructionBox added");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "EnergyLink") {
			nearby.Remove(other.gameObject.GetComponent<EnergyLinkController>());
			// Debug.Log("Energy Link removed");
		} else if (other.tag == "EnergyConsumer") {
			constructions.Remove(other.gameObject.GetComponent<EnergyConsumerController>());
			// Debug.Log("Construction Box removed");
		}
	}
}
