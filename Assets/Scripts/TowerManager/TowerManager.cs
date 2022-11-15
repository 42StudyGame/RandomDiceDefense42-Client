using System.Collections.Generic;
using UnityEngine;

// public delegate int TowerModify();

public partial class TowerManager // IO
{
	public void Launch(Tower obj) => _Launch(obj);
	public Bullet GetBullet(Tower tower) => _GetBullet(tower);
	public void SetBullet(Bullet bullet) => _SetBullet(bullet);
	public Enemy GetTarget() => _GetTarget();
	public bool AddTower() => _AddTower();
	public void DestroyTower(Tower tower) => _DestroyTower(tower);

	public void Merge(Tower baseTower, Tower otherTower) => _Merge(baseTower, otherTower);
}

public partial class TowerManager // SerializeField
{
	[SerializeField] private GameManager gameManager;
	[SerializeField] private BulletPool bulletPool;
	[SerializeField] private RandomDiceCreate randomDiceCreate;
	[SerializeField] private int maxGrade = 6;
}
public partial class TowerManager : MonoBehaviour
{
	private void _DestroyTower(Tower tower)
	{
		_towers.Remove(tower);
		randomDiceCreate.ReleaseTower(tower);
		Destroy(tower.gameObject);
	}
}

public partial class TowerManager // body
{
	private readonly List<Tower> _towers = new List<Tower>();

	// ReSharper disable Unity.PerformanceAnalysis
	private void _Launch(Tower tower)
	{
		Bullet bullet = bulletPool.GetObject();
		bullet.transform.position = tower.GetStartPosition();
		bullet.SetTarget(GetTarget());
		bullet.SetDamage(tower.towerData.damage * tower.GetGrade());
	}

	private Bullet _GetBullet(Tower tower)
	{
		Bullet bullet = bulletPool.GetObject();
		return (bullet);
	}

	private void _SetBullet(Bullet bullet)
	{
		bulletPool.ReturnObject(bullet);
	}

	private bool _AddTower(/*int _class, int level, int star*/)
	{
		Tower tower = randomDiceCreate.CreateTower();
		if (!tower)
		{
			return false;
		}
		_towers.Add(tower);
		tower.Init(this);
		return (true);
	}

	private Enemy _GetTarget()
	{
		return gameManager.enemyManager.targetFirst;
	}

	private void _Merge(Tower baseTower, Tower otherTower) {
		if (baseTower.GetGrade() >= maxGrade)
			return;
		baseTower.towerData = 
			randomDiceCreate.diceDeck[Random.Range(0, randomDiceCreate.diceDeck.Length)].towerData;
		baseTower.Init(this);
		baseTower.UpGrade();
		baseTower.ResetEyesPosition();
		DestroyTower(otherTower);
	}
}
