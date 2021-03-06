﻿using System.Collections;
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
	public float mothershipTime;
	public float startDifficulty;
	public float lastWaveTime;
	public GameObject fighterPrefab;
	public GameObject interseptorPrefab;
	public GameObject mothershipPrefab;

	private float startTime;
	public float nextWave { get; private set; }

	void Start() {
		startTime = Time.timeSinceLevelLoad;
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		nextWave = Time.timeSinceLevelLoad + delay;
		yield return new WaitForSeconds(delay);
		while (Time.timeSinceLevelLoad < lastWaveTime) {
			float targetDifficulty = (Time.timeSinceLevelLoad - startTime - delay) * difficultyPerSecond + startDifficulty;
			float difficulty = 0.0f;
			float angle = Random.Range(0, 360);
			if (Random.value < 0.25 && Time.timeSinceLevelLoad <= mothershipTime) {
				while (difficulty <= targetDifficulty) {
					Vector3 position = Quaternion.Euler(0, 0, angle) * new Vector3(radius + Random.Range(-2.9f, +2.9f), 0, 0);
					angle += 7;
					Instantiate(interseptorPrefab, position, Quaternion.identity);
					difficulty += interseptorDifficulty;
				}
			} else {
				while (difficulty <= targetDifficulty) {
					if (Time.timeSinceLevelLoad > mothershipTime) {
						Vector3 position = Quaternion.Euler(0, 0, angle) * new Vector3(radius + Random.Range(-2.9f, +2.9f), 0, 0);
						angle += 10;
						Instantiate(mothershipPrefab, position, Quaternion.identity);
						difficulty += mothershipDifficulty;
					} else {
						Vector3 position = Quaternion.Euler(0, 0, angle) * new Vector3(radius + Random.Range(-2.9f, +2.9f), 0, 0);
						angle += 5;
						Instantiate(fighterPrefab, position, Quaternion.identity);
						difficulty += fighterDifficulty;
					}
				}
			}
			nextWave = Time.timeSinceLevelLoad + interval;
			yield return new WaitForSeconds(interval);
		}
	}
}
