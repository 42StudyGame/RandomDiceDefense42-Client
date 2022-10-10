using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private EnemyPool _enemyPool;
	[SerializeField] private EnemyData[] _enemyDatas;
	private List<Enemy> _enemies = new List<Enemy>();
	[SerializeField] private Transform _spawnPoint;
	public Enemy targetFirst { get; private set; }
	public Enemy targetRandom { get; private set; }
	public Enemy targetStrongest { get; private set; }
	public Transform[] wayPoints;

	
	private void Start()
	{
		CreateEnemy();
	}

	private void Update()
	{
		SetGeneralTarget();
	}

	public void Init() {
		
	}

	public void CreateEnemy()
	{
		Enemy enemy = _enemyPool.GetObject();
		EnemyData enemyData = _enemyDatas[0];
		enemy.transform.position = _spawnPoint.position;
		enemy.Init(enemyData, 0, this);
		_enemies.Add(enemy);
	}

	public void DestroyEnemy(Enemy enemy)
	{
		_enemyPool.ReturnObject(enemy);
		_enemies.Remove(enemy);
	}

	private void SetGeneralTarget()
	{
		int maxHealth = 0;
		float minDist = float.MaxValue;
		for (int i = 0; i < _enemies.Count; i++)
		{
			if (_enemies[i].progressToGoal < minDist)
			{
				minDist = _enemies[i].progressToGoal;
				targetFirst = _enemies[i];
			}
			if (_enemies[i].currHealth > maxHealth)
			{
				maxHealth = _enemies[i].currHealth;
				targetStrongest = _enemies[i];
			}
		}

		targetRandom = _enemies[Random.Range(0, _enemies.Count)];
	}

	public void EnemyGoal() {
		
	}
}
