using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate int TowerModify(); 
	
public class TowerManager : MonoBehaviour {
	[SerializeField] private GameManager _gameManager;
	[SerializeField] private BulletPool _bulletPool;
	[SerializeField] private Tower[] _towers;
	[SerializeField] private RandomDiceCreate _randomDiceCreate;

	// public void Init<T>(T reference) where T : GameManager
	// {
	// 	_gameManager = reference;
	// 	// _randomDiceCreate = FindObjectOfType<RandomDiceCreate>();
	// }

	public void Launch(Tower obj) {
		Bullet bullet = _bulletPool.GetObject();
		bullet.transform.position = obj.transform.position;
		bullet.SetTarget(null); // null -> enemey object
		// bullet.transform.parent = obj.transform;
	}

	public void CreateTower() {
		// 타워 생성후 tower.init() 실행
	}
}
