using UnityEngine;

public partial class Tower // IO
{
	public void Init(TowerManager towerManager) => _Init(towerManager);
	public TowerData towerData;
	public int slotId;
	public void UpGrade() => _UpGrade();
	public void UpGrade(int num) => _UpGrade(num);
	public void DownGrade() => _DownGrade();
	public void DownGrade(int num) => _DownGrade(num);
	public int GetGrade() => _GetGrade();
	public void ResetEyesPosition() => _ResetEyesPosition();
}

public partial class Tower // SerializeField
{
	[SerializeField] private Draggable draggable;
	[SerializeField] private TowerEyesPosition towerEyesPosition;
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
		draggable.Init(this, towerManager);
	}

	private void Launch() 
	{
		if (Time.time >= _lastAttackTime + towerData.attackSpeed)
		{
			_lastAttackTime = Time.time;
			_towerManager.Launch(this);
		}
	}

	private int _GetGrade()
	{
		return TowerGrade;
	}

	private void _UpGrade() 
	{
		TowerGrade += 1;
	}
	
	private void _UpGrade(int num)
	{
		TowerGrade += num;
	}
	
	private void _DownGrade() 
	{
		TowerGrade -= 1;
	}
	
	private void _DownGrade(int num)
	{
		TowerGrade -= num;
	}

	private void _ResetEyesPosition() 
	{
		towerEyesPosition.FindEyesPosition(TowerGrade);	
	}
}