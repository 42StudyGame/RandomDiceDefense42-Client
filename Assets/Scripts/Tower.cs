using UnityEngine;

public class Tower : MonoBehaviour
{
	private TowerManager _towerManager;
	public TowerData towerData; //ScriptableObject

	private	bool _isEnable;
	protected int _towerGrade = 1;
	protected int _towerLevel = 1;
	protected int _towerStar = 1;
	private float _lastAttackTime;

	// protected virtual void Awake() {
	// }

	// /*
	//  * child tower sample begin
	// protected override void Awake() {
	// 	base.Awake();
	// 	// ...
	// }
	// private void Modify() {
	// 	towerData.ModifyDelegate = () => _towerLevel / 2;
	// }
	// public void Merge() {
	// 	// normal dice
	// 	++_towerGrade;
	// 	++_towerLevel;
	// 	
	// 	// random level dice
	// 	_towerGrade = Random.Range(1, 8);
	// 	_towerLevel = Random.Range(1, 8);
	// }
	//  * child tower sample end
	//  */
	// // public void Merge() {
	// // 	// normal dice
	// // 	++_towerGrade;
	// // 	++_towerLevel;
	// // 	
	// // 	// random level dice
	// // 	_towerGrade = Random.Range(1, 8);
	// // 	_towerLevel = Random.Range(1, 8);
	// // }
	
	public void Init(TowerManager towerManager) {
		_towerManager = towerManager;
		_isEnable = false;
		_lastAttackTime = Time.time;
	}

	// 기본적인 총알 발사 함수 (update함수 내에서 실행)
	// ReSharper disable Unity.PerformanceAnalysis
	private void Launch() {
		if (Time.time >= _lastAttackTime + towerData.attackSpeed)
		{
			_lastAttackTime = Time.time;
			_towerManager.Launch(this);
		}
	}

	private void Update() {
		Enemy target = _towerManager.GetTarget();
		if (target)
			Launch();
	}
}