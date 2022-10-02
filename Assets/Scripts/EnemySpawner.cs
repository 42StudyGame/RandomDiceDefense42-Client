using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public float spawnTime;
	private float lastSpawnTime;
	public GameObject enemyPrefab;
	public GameObject startPoint;
	private void Start() {
		lastSpawnTime = Time.time;
	}

	void Update()
    {
		if (Time.time >= lastSpawnTime + spawnTime)
		{
			CreateEnemy();
			lastSpawnTime = Time.time;
		}
    }

	public void CreateEnemy() {
		Instantiate(enemyPrefab, startPoint.transform.position, Quaternion.identity);
	}
}
