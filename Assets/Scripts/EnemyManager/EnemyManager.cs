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
	public EnemyLineManager enemyLine;
	public EMState State { get; private set; }
	
	public Enemy targetFirst { get; private set; }
	public Enemy targetLast { get; private set; }
	public Enemy targetRandom { get; private set; }
	public Enemy targetStrongest { get; private set; }

	public void Init() => _Init();
	public void CreateEnemy(int type, int hpOffset) => _CreateEnemy(type, hpOffset);
	public void DestroyEnemy(Enemy enemy) => _DestroyEnemy(enemy);
	public void DestroyBoss(Boss boss) => _DestroyBoss(boss);

	public void EnemyGoal(int damage) => _EnemyGoal(damage);

	public void SetGeneralTarget() => _SetGeneralTarget();

	public List<Enemy> GetNearEnemy(float pin, float offset) => _GetNearEnemy(pin, offset);
	public Enemy GetPrevTarget(float pin) => _GetPrevTarget(pin);
	public Enemy GetNexttarget(float pin) => _GetNexttarget(pin);

	public void InjectScenario(ScenarioList wave, float startDelay) => _InjectScenario(wave, startDelay);
}

public partial class EnemyManager // SerializeField
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private EnemyPool _enemyPool;
	[SerializeField] private EnemyData[] _enemyDatas;
	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private Boss _bossPrefab;
	[SerializeField] private ASkills[] _skills;
}

public partial class EnemyManager : MonoBehaviour
{
	private void Start()
	{
		// _CreateBoss(3, 1);
	}
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
		enemyLine.Init();
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

	private void _CreateBoss(int type, int hpOffset)
	{
		Boss boss = Instantiate(_bossPrefab, _spawnPoint.position, _spawnPoint.rotation);
		BossData bossData = (BossData)_enemyDatas[type];
		boss.Init(bossData, hpOffset, _gameManager, _skills[bossData.skillIndex].Skill);
		_enemies.Add(boss);
		SetGeneralTarget();
	}

	private void _DestroyEnemy(Enemy enemy)
	{
		_enemyPool.ReturnObject(enemy);
		_enemies.Remove(enemy);
		_gameManager.sp += enemy.sp;
		_gameManager.uiManager.SetSpText(_gameManager.sp.ToString());
	}

	private void _DestroyBoss(Boss boss)
	{
		_enemies.Remove(boss);
		_gameManager.sp += boss.sp;
		_gameManager.uiManager.SetSpText(_gameManager.sp.ToString());
		Destroy(boss.gameObject);
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


	private List<Enemy> _GetNearEnemy(float pin, float offset)
	{
		if (offset > 1 || offset < 0 || pin > 1 || pin < 0)
        {
			Debug.LogError("input value error");
			return null;
        }

		List<Enemy> result = new List<Enemy>();
		float min = pin - offset;
		float max = pin + offset;

		foreach(Enemy enemy in _enemies)
        {
			if (enemy.progressToGoal > min && enemy.progressToGoal < max)
            {
				result.Add(enemy);
            }
        }
		return result;
	}

	private Enemy _GetPrevTarget(float pin)
    {
		if (pin > 1 || pin < 0)
        {
			Debug.LogError("input value error");
			return null;
		}

		Enemy result = _enemies.LastOrDefault();

		foreach (Enemy enemy in _enemies)
        {
			if (enemy.progressToGoal < pin && enemy.progressToGoal > result.progressToGoal)
            {
				result = enemy;
            }
        }
		return (result);
    }
	private Enemy _GetNexttarget(float pin)
    {
		if (pin > 1 || pin < 0)
		{
			Debug.LogError("input value error");
			return null;
		}

		Enemy result = _enemies.FirstOrDefault();

		foreach (Enemy enemy in _enemies)
		{
			if (enemy.progressToGoal > pin && enemy.progressToGoal < result.progressToGoal)
			{
				result = enemy;
			}
		}
		return (result);
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
		if (_currentWave.boss != 0)
		{
			yield return new WaitForSeconds(_currentWave.spawnDelay);
			_CreateBoss(_currentWave.boss, _currentWave.enemyHPOffset);
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

	private void _EnemyGoal(int damage)
	{
		_gameManager.OnDamage(damage);
	}
}
