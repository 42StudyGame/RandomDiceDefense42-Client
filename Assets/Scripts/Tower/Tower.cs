using UnityEngine;

public partial class Tower // IO
{
	public void Init(TowerManager towerManager) => _Init(towerManager);
	public TowerData towerData;
	[HideInInspector] public int slotId;
	public void UpGrade(int num = 1) => _UpGrade(num);
	public void DownGrade(int num = 1) => _DownGrade(num);
	public int GetGrade() => _GetGrade();
	public void ResetEyesPosition() => _ResetEyesPosition();
	public Vector2 GetStartPosition() => _GetSartPosition();
}

public partial class Tower // SerializeField
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Draggable draggable;
	[SerializeField] private TowerEyesPosition towerEyesPosition;
}

public partial class Tower : MonoBehaviour
{
	protected TowerManager _towerManager;

	private void Update()
	{
		bool hasTarget = _towerManager.GetTarget().HasTarget();
		if (hasTarget && (Time.time >= _lastAttackTime + towerData.attackSpeed / (TowerGrade * towerData.gradeAttackSpeedIncrease)))
		{
			_Launch();
		}
	}
}

public partial class Tower // body
{
	private float _lastAttackTime;
	private Vector2 _startPosition;
	private bool _isEnable = false;

	protected Bullet _bullet = null;
	protected Enemy _currentTarget = null;

	protected int TowerGrade = 1;
	protected int TowerLevel = 1;
	protected int TowerStar = 1;

	private void _Init(TowerManager towerManager)
	{
		spriteRenderer.sprite = towerData.sprite;
		_towerManager = towerManager;
		draggable.Init(this, towerManager);
		towerEyesPosition.Init();
		_startPosition = transform.position;
		_lastAttackTime = Time.time;
	}

	protected virtual void _Launch() 
	{
		_lastAttackTime = Time.time;
		_bullet = _towerManager.GetBullet(this);
		_bullet.transform.position = transform.position;
		_bullet.SetDamage(towerData.damage * TowerGrade);
	}

	protected virtual void _Skill()
	{
		
	}

	private int _GetGrade()
	{
		return TowerGrade;
	}

	private void _UpGrade(int num = 1)
	{
		TowerGrade += num;
	}
	
	private void _DownGrade(int num = 1)
	{
		TowerGrade -= num;
	}

	private void _ResetEyesPosition() 
	{
		towerEyesPosition.FindEyesPosition(TowerGrade);	
	}

	private Vector2 _GetSartPosition() 
	{
		return _startPosition;
	}
}