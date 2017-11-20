using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBoxController : MonoBehaviour
{
	public GameObject construction;
	public EnergyConsumerController energyConsumer;

	public int targetEnergy { set { energyConsumer.targetEnergy = value; } }

	void Update() {
		if (energyConsumer.Full()) {
			Instantiate(construction, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
