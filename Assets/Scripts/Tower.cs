using UnityEngine;

public partial class Tower // IO
{
	public void Init(TowerManager towerManager) => _Init(towerManager);
	public TowerData towerData;
	public int slotId;
}

public partial class Tower : MonoBehaviour
{
	private TowerManager _towerManager;

	private void Update() 
	{
		Enemy target = _towerManager.GetTarget();
		if (target)
		{
			Launch();
		}
	}
}

public partial class Tower // body
{
	private float _lastAttackTime;
	private	bool _isEnable;
	protected int TowerGrade = 1;
	protected int TowerLevel = 1;
	protected int TowerStar = 1;

	private void _Init(TowerManager towerManager) 
	{
		_towerManager = towerManager;
		_isEnable = false;
		_lastAttackTime = Time.time;
	}

	private void Launch() 
	{
		if (Time.time >= _lastAttackTime + towerData.attackSpeed)
		{
			_lastAttackTime = Time.time;
			_towerManager.Launch(this);
		}
	}
}