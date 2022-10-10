using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private EnemyPool _enemyPool;
	public EnemyData[] _enemyDatas;
	private List<Enemy> _enemies = new List<Enemy>();
	[SerializeField] private Transform _spawnPoint;
	public Transform[] wayPoints;

	private void Start()
	{
		CreateEnemy();
	}

	public void Init() {
		
	}

	public void CreateEnemy()
	{
		Enemy enemy = _enemyPool.GetObject();
		enemy.transform.position = _spawnPoint.position;
		enemy.Init(_enemyDatas[0], 0, this);
		_enemies.Add(enemy);
	}

	public void KillEnemy(Enemy enemy)
	{
		_enemyPool.ReturnObject(enemy);
		_enemies.Remove(enemy);
	}

	public void EnemyGoal() {
		
	}
}
