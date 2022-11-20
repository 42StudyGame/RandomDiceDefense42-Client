using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class EnemyTargetManager     // IO
{
    public void Init(List<Enemy> enemies) => _Init(enemies);

    public Enemy targetFirst { get; private set; }
    public Enemy targetLast { get; private set; }
    public Enemy targetRandom { get; private set; }
    public Enemy targetStrongest { get; private set; }

	public bool HasTarget() => _HasTarget();


	public void SetGeneralTarget() => _SetGeneralTarget();

    public List<Enemy> GetNearTargets(float pin, float offset) => _GetNearTargets(pin, offset);
    public Enemy GetPrevTarget(float pin) => _GetPrevTarget(pin);
    public Enemy GetNexttarget(float pin) => _GetNexttarget(pin);
}

public partial class EnemyTargetManager     // body
{
    private List<Enemy> _enemies;

    private void _Init(List<Enemy> enemies)
    {
        _enemies = enemies;
    }

	private bool _HasTarget()
    {
		if (_enemies.Count != 0)
        {
			return true;
        }
		else
        {
			return false;
        }
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


	private List<Enemy> _GetNearTargets(float pin, float offset)
	{
		if (offset > 1 || offset < 0 || pin > 1 || pin < 0)
		{
			Debug.LogError("input value error");
			return null;
		}

		List<Enemy> result = new List<Enemy>();
		float min = pin - offset;
		float max = pin + offset;

		foreach (Enemy enemy in _enemies)
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
}
