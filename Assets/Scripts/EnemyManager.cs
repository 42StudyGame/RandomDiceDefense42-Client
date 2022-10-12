using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class EnemyManager // IO
{
	[HideInInspector] public Transform[] wayPoints;

	public Enemy targetFirst { get; private set; }
	public Enemy targetRandom { get; private set; }
	public Enemy targetStrongest { get; private set; }
	public float maxDistToGoal { get; private set; }

	public void Init() => _Init();
	public void CreateEnemy() => _CreateEnemy();
	public void DestroyEnemy(Enemy enemy) => _DestroyEnemy(enemy);
	public void EnemyGoal() => _EnemyGoal();

}

public partial class EnemyManager // SerializeField
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private EnemyPool _enemyPool;
	[SerializeField] private EnemyData[] _enemyDatas;
	[SerializeField] private Transform _spawnPoint;
}

public partial class EnemyManager : MonoBehaviour
{
	private void Start()
	{
		// TODO : remove
		CreateEnemy();
	}

	private void Update()
	{
		SetGeneralTarget();
	}
}

public partial class EnemyManager
{
	private List<Enemy> _enemies = new List<Enemy>();

	private void _Init() {
		maxDistToGoal = GetMaxDistToGoal();
	}

	private void _CreateEnemy()
	{
		Enemy enemy = _enemyPool.GetObject();
		EnemyData enemyData = _enemyDatas[0];
		enemy.transform.position = _spawnPoint.position;
		enemy.Init(enemyData, 0, this);
		_enemies.Add(enemy);
	}

	private void _DestroyEnemy(Enemy enemy)
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
		float maxHealth = 0;
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

	private float GetMaxDistToGoal()
	{
		float dist = Vector2.Distance(_spawnPoint.position, wayPoints[0].position);
		for (int i = 0; i < wayPoints.Length - 1; i++)
		{
			dist += Vector2.Distance(wayPoints[i].position, wayPoints[i + 1].position);
		}

		return dist;
	}

	private void _EnemyGoal()
	{

	}
}
