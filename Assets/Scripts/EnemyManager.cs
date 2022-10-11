using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

	public float maxDistToGoal { get; private set; }


	private void Start()
	{
		CreateEnemy();
	}

	private void Update()
	{
		SetGeneralTarget();
	}

	public void Init() {
		maxDistToGoal = GetMaxDistToGoal();
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
		if (!_enemies.Any())
		{
			targetFirst = null;
			targetRandom = null;
			targetStrongest = null;
			return;
		}
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

	private float GetMaxDistToGoal()
	{
		float dist = 0f;
		for (int i = 0; i < wayPoints.Length - 1; i++)
		{
			dist += Vector2.Distance(wayPoints[i].position, wayPoints[i + 1].position);
		}

		return dist;
	}
}
