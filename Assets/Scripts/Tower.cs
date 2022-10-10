using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] private TowerManager _towerManager;
	public TowerData towerData; //ScriptableObject

	private	bool _isEnable;
	protected int _towerGrade = 1;
	protected int _towerLevel = 1;
	private float _lastAttackTime;

	public void SetTowerManager(TowerManager towerManager) {
		_towerManager = towerManager; 
	}

	protected virtual void Awake() {
	}

	/*
	 * child tower sample begin
	protected override void Awake() {
		base.Awake();
		// ...
	}
	private void Modify() {
		towerData.ModifyDelegate = () => _towerLevel / 2;
	}
	public void Merge() {
		// normal dice
		++_towerGrade;
		++_towerLevel;
		
		// random level dice
		_towerGrade = Random.Range(1, 8);
		_towerLevel = Random.Range(1, 8);
	}
	 * child tower sample end
	 */
	
	public void Init() {
		_isEnable = false;
		_lastAttackTime = Time.time;
	}
	//
	// public void Merge() {
	// 	// normal dice
	// 	++_towerGrade;
	// 	++_towerLevel;
	// 	
	// 	// random level dice
	// 	_towerGrade = Random.Range(1, 8);
	// 	_towerLevel = Random.Range(1, 8);
	// }

	// 기본적인 총알 발사 함수 (update함수 내에서 실행)
	private void Launch() {
		if (Time.time >= _lastAttackTime + towerData.attackSpeed)
			_towerManager.Launch(this);
	}

	// private void OnEnable() {
	// 	// towerManager = FindObjectOfType<TowerManager>();
	// }

	private void Update()
	{
        Launch();
	}
}