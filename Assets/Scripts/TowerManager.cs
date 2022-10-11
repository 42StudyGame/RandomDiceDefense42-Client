using System.Collections.Generic;
using UnityEngine;

public delegate int TowerModify();

public partial class TowerManager // IO
{
	public void Launch(Tower obj) => _Launch(obj);
	public Bullet GetBullet(Tower tower) => _GetBullet(tower);
	public void SetBullet(Bullet bullet) => _SetBullet(bullet);
	public void AddTower() => _AddTower();
	public Enemy GetTarget() => _GetTarget();
}

public partial class TowerManager // SerializeField
{
	[SerializeField] private GameManager gameManager;
	[SerializeField] private BulletPool bulletPool;
	[SerializeField] private RandomDiceCreate randomDiceCreate;
}
public partial class TowerManager : MonoBehaviour
{
	private void _DeleteTower(Tower tower) {
		_towers.Remove(tower);
		Destroy(tower);
	}
}

public partial class TowerManager // body
{
	private List<Tower> _towers = new List<Tower>();

	// ReSharper disable Unity.PerformanceAnalysis
	private void _Launch(Tower tower)
	{
		Bullet bullet = bulletPool.GetObject();
		bullet.transform.position = tower.transform.position;
		bullet.SetTarget(GetTarget());
		bullet.SetDamage(tower.towerData.damage);
	}

	private Bullet _GetBullet(Tower tower)
	{
		Bullet bullet = bulletPool.GetObject();
		bullet.transform.position = tower.transform.position;
		bullet.SetTarget(GetTarget());
		return (bullet);
	}

	private void _SetBullet(Bullet bullet)
	{
		bulletPool.ReturnObject(bullet);
	}

	private void _AddTower(/*int _class, int level, int star*/)
	{
		Tower tower = randomDiceCreate.CreateTower();
		_towers.Add(tower);
		tower?.Init(this);
	}

	private Enemy _GetTarget()
	{
		return gameManager.enemyManager.targetFirst;
	}
}
