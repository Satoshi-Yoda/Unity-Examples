using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	public float radius;
	public float delay;
	public float interval;
	public float difficultyPerSecond;
	public float fighterDifficulty;
	public float interseptorDifficulty;
	public float mothershipDifficulty;
	public GameObject fighterPrefab;
	public GameObject interseptorPrefab;
	public GameObject mothershipPrefab;

	private float startTime;

	void Start() {
		startTime = Time.time;
		StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn() {
		yield return new WaitForSeconds(delay);
		while (true) {
			float targetDifficulty = (Time.time - startTime) * difficultyPerSecond;
			float difficulty = 0.0f;
			while (difficulty <= targetDifficulty) {
				Vector3 position = Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(radius, 0, 0);
				Instantiate(interseptorPrefab, position, Quaternion.identity);
				difficulty += interseptorDifficulty;
			}
			yield return new WaitForSeconds(interval);
		}
	}
}
