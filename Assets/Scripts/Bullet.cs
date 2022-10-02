using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private GameObject target;
	private BulletSpawner parentBulletSpawner;
	private float movementSpeed;
	private BulletPool _pool;

	private void Start() {
		parentBulletSpawner = GetComponentInParent<BulletSpawner>();
		target = parentBulletSpawner.InformTarget();
		movementSpeed = parentBulletSpawner.InformSpeed();
		_pool = FindObjectOfType<BulletPool>();
	}

	void Update() {
		if (target)
			MovePath();
		else
			_pool.ReturnObject(this);
	}

	public void MovePath() {
		if (!target)
			Destroy(gameObject);
		transform.position = Vector3.MoveTowards
		(transform.position, 
			target.transform.position, 
			movementSpeed * Time.deltaTime);
		// 콜라이더추가해서 충돌 비교
		if (transform.position == target.transform.position)
		{
			target.GetComponent<EnemyHealth>().OnDamage(parentBulletSpawner.InformDamage());
			_pool.ReturnObject(this);
			// Destroy(gameObject);
		}
	}
}
