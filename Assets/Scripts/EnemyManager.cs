using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private GameManager _gameManager;
	private EnemyPool _enemyPool;
	private Enemy[] _enemies;

	public void Init() {
		_enemyPool = GetComponent<EnemyPool>();
		_gameManager = FindObjectOfType<GameManager>();
	}

	public void CreateEnemy() {
		
	}

	public void EnemyGoal() {
		
	}
}
