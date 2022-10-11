using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate int TowerModify(); 

public class TowerManager : MonoBehaviour {
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private BulletPool _bulletPool;
	[SerializeField] private RandomDiceCreate _randomDiceCreate;
	private Tower[] _towers;

	public void Launch(Tower obj) {
		Bullet bullet = _bulletPool.GetObject();
		bullet.transform.position = obj.transform.position;
		bullet.SetTarget(GetTarget()); // null -> enemey object
	}

	public Bullet GetBullet(Tower obj) {
		Bullet bullet = _bulletPool.GetObject();
		bullet.transform.position = obj.transform.position;
		bullet.SetTarget(GetTarget());
		return (bullet);
	}
	public void SetBullet(Bullet bullet) {
		_bulletPool.ReturnObject(bullet);
	}

	public void AddTower(/*int _class, int level, int star*/) {
		Tower tower = _randomDiceCreate.CreateTower();
		tower?.Init(this);
	}
	// /** int addTower(int class, int level, int star...)
	// * 타워 생성 함수.
	// * 게임 매니저가 갖고 있는 타워 생성 버튼이 눌리면 해당 함수가 실행되면서 타워 배열의 랜덤 위치에 랜덤한 타워가 생성됨
	// * 혹은 플레이어가 타워를 합칠 때도 사용(이는 추후 생각해보겠음)
	// * 매개변수는 타워를 생성할 때 필요한 변수.
	// * 이 함수에서 타워의 init함수를 호출해 변수값을 설정해줌
	// * 어떠한 이유로든 타워가 생성되지 않으면 오류 코드 등을 반환
	// */
	public Enemy GetTarget() { 
		return _gameManager.enemyManager.targetFirst;
	}

}
