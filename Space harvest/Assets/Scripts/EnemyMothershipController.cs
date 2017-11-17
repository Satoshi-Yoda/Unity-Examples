using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothershipController : MonoBehaviour
{
	public float spawnPeriod;
	public float spawnStart;
	public float spawnRadius;
	public GameObject interseprotPrefab;

	void Start() {
		StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn() {
		yield return new WaitForSeconds(spawnStart);
		while (true) {
			if (gameObject.transform.position.magnitude < spawnRadius) {
				GameObject interseptor1 = Instantiate(interseprotPrefab, gameObject.transform.position, gameObject.transform.rotation);
				interseptor1.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 45) * gameObject.GetComponent<Rigidbody2D>().velocity * 10;
				GameObject interseptor2 = Instantiate(interseprotPrefab, gameObject.transform.position, gameObject.transform.rotation);
				interseptor2.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, -45) * gameObject.GetComponent<Rigidbody2D>().velocity * 10;
			}
			yield return new WaitForSeconds(spawnPeriod);
		}
	}
}
