using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
	public Dice parentDice;
	public GameObject bullet;
	
	private GameObject target;
	private float lastAttakTime;
	private BulletPool _pool; 

	private void OnEnable() {
		lastAttakTime = Time.time;
		_pool = FindObjectOfType<BulletPool>();
	}

	private void Update() {
		if (Time.time >= lastAttakTime + parentDice.attackInterval)
		{
			AttackEnemy();
			lastAttakTime = Time.time;
		}
	}

	public void FindTarget() {
		target = GameObject.FindWithTag("Enemy");
	}
	
	public void AttackEnemy() {
		FindTarget();
		
		var bullet = _pool.GetObject();
		bullet.transform.position = transform.position;
		// GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
		bullet.transform.parent = this.transform;
	}

	public GameObject InformTarget() {
		return target;
	}
	public float InformSpeed() {
		return parentDice.attackSpeed;
	}
	public float InformDamage() {
		return parentDice.damage;
	}
	// 타겟팅
	// 탄알 생성
	// 탄알 > 타겟
	// 타겟의 OnDamage() 실행
}
