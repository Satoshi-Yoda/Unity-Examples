using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
	public int damage;
	public float lifeTime;
	public GameObject explosionPrefab;

	private List<GameObject> targets = new List<GameObject>();

	public void WillDestroy() {
		Boom();
	}

	void Start() {
		Invoke("Boom", lifeTime);
	}

	void Boom() {
		foreach (GameObject target in targets) {
			EnemyFighterController fighterTarget = target.GetComponent<EnemyFighterController>();
			if (fighterTarget != null) fighterTarget.hull -= damage;
		}
		Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			targets.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Enemy") {
			targets.Remove(other.gameObject);
		}
	}
}
