using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EMState
{
	waiting = 0,
	spawning = 1,
	spawnEnd = 2
}

public partial class EnemyManager // IO
{
	[HideInInspector] public Transform[] wayPoints;

	public EMState State { get; private set; }

	public Enemy targetFirst { get; private set; }
	public Enemy targetLast { get; private set; }
	public Enemy targetRandom { get; private set; }
	public Enemy targetStrongest { get; private set; }
	public float maxDistToGoal { get; private set; }

	public void Init() => _Init();

	public void DestroyEnemy(Enemy enemy) => _DestroyEnemy(enemy);

	public void EnemyGoal() => _EnemyGoal();

	public void SetGeneralTarget() => _SetGeneralTarget();

	public void InjectScenario(ScenarioList wave, float startDelay) => _InjectScenario(wave, startDelay);
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
	private void Update()
	{
		_UpdateState();
	}
}

public partial class EnemyManager
{
	private ScenarioList _currentWave = new ScenarioList();
	private List<Enemy> _enemies = new List<Enemy>();

	private void _Init() {
		maxDistToGoal = _GetMaxDistToGoal();
	}

	private void _CreateEnemy(int type, int hpOffset)
	{
		Enemy enemy = _enemyPool.GetObject();
		EnemyData enemyData = _enemyDatas[type];
		enemy.transform.position = _spawnPoint.position;
		enemy.Init(enemyData, hpOffset, this);
		_enemies.Add(enemy);
		SetGeneralTarget();
	}

	private void _DestroyEnemy(Enemy enemy)
	{
		_enemyPool.ReturnObject(enemy);
		_enemies.Remove(enemy);
	}

	private void _SetGeneralTarget()
	{
		if (!_enemies.Any())
		{
			targetFirst = null;
			targetLast = null;
			targetRandom = null;
			targetStrongest = null;
			return;
		}
		float maxHealth = 0;
		float minDist = float.MaxValue;
		float maxDist = 0f;
		for (int i = 0; i < _enemies.Count; i++)
		{
			if (_enemies[i].progressToGoal < minDist)
			{
				minDist = _enemies[i].progressToGoal;
				targetLast = _enemies[i];
			}
			if (_enemies[i].progressToGoal > maxDist)
            {
				maxDist = _enemies[i].progressToGoal;
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

	private float _GetMaxDistToGoal()
	{
		float dist = Vector2.Distance(_spawnPoint.position, wayPoints[0].position);
		for (int i = 0; i < wayPoints.Length - 1; i++)
		{
			dist += Vector2.Distance(wayPoints[i].position, wayPoints[i + 1].position);
		}

		return dist;
	}

	private void _InjectScenario(ScenarioList wave, float delay)
    {
		_currentWave = wave;
		StartCoroutine(_SpawnEnemyByWave(delay));
    }

	IEnumerator _SpawnEnemyByWave(float startDelay)
    {
		yield return new WaitForSeconds(startDelay);
		while(_currentWave.enemyList.Count > 0)
        {
			yield return new WaitForSeconds(_currentWave.spawnDelay);
			_CreateEnemy(_currentWave.enemyList[0], _currentWave.enemyHPOffset);
			_currentWave.enemyList.RemoveAt(0);
		}
		yield return null;
    }

	private void _UpdateState()
    {
		if (_currentWave.enemyList.Count != 0)
        {
			State = EMState.spawning;
        }
		else if (_currentWave.enemyList.Count == 0 && _enemies.Count != 0)
        {
			State = EMState.spawnEnd;
        }
		else
        {
			State = EMState.waiting;
        }
    }

	private void _EnemyGoal()
	{

	}
}
