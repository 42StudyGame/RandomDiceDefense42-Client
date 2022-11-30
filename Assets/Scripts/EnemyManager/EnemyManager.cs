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

	public EnemyTargetManager enemyTarget { get; private set; } = new EnemyTargetManager();

	public EMState State { get; private set; }

	public void Init() => _Init();
	public void CreateEnemy(int type, int hpOffset) => _CreateEnemy(type, hpOffset);
	public void DestroyEnemy(Enemy enemy) => _DestroyEnemy(enemy);
	public void DestroyBoss(Boss boss) => _DestroyBoss(boss);

	public void EnemyGoal(int damage) => _EnemyGoal(damage);

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
		enemyTarget.Init(_enemies);
	}

	private void _CreateEnemy(int type, int hpOffset)
	{
		Enemy enemy = _enemyPool.GetObject();
		EnemyData enemyData = _enemyDatas[type];
		enemy.transform.position = _spawnPoint.position;
		enemy.Init(enemyData, hpOffset, this);
		_enemies.Add(enemy);
		enemyTarget.SetGeneralTarget();
	}

	private void _CreateBoss(int type, int hpOffset)
	{
		Boss boss = Instantiate(_bossPrefab, _spawnPoint.position, _spawnPoint.rotation);
		BossData bossData = (BossData)_enemyDatas[type];
		boss.Init(bossData, hpOffset, _gameManager, _skills[bossData.skillIndex].Skill);
		_enemies.Add(boss);
		enemyTarget.SetGeneralTarget();
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
