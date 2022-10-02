	using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {
	public GameObject bulletPrefab;
	public int startInitializeCount;

	private Vector3 poolPosition;
	private Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();
	
	// 오브젝트풀에 초기불렛 생성 
	private void Awake()
	{
		Initialize(startInitializeCount);
	}

	private Bullet CreateNewBullet() {
		Bullet newObj = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
		newObj.gameObject.SetActive(false);
		return newObj;
	}
	
	private void Initialize(int count) {
		for (int i = 0; i < count; i++)
			poolingObjectQueue.Enqueue(CreateNewBullet());
	}

	// 오브젝트 풀에 있는 총알을 가져오는 메서드
	// 풀에 남아 있는 총알이 없으면 추가로 불렛을 생성해준다.
	public Bullet GetObject() {
		if (poolingObjectQueue.Count > 0)
		{
			Bullet obj = poolingObjectQueue.Dequeue();
			obj.transform.SetParent(null); // 부모 오브젝트에서 나온다.
			obj.gameObject.SetActive(true);
			return obj;
		}
		else
		{
			Bullet newObj = CreateNewBullet();
			newObj.transform.SetParent(null); // 부모 오브젝트에서 나온다.
			newObj.gameObject.SetActive(true);
			return newObj;
		}
	}

	//가져다 쓴 불렛을 다시 오브젝트풀에 되돌려 놓는 메서드
	public void ReturnObject(Bullet bullet) {
		bullet.gameObject.SetActive(false);
		bullet.transform.SetParent(transform);
		poolingObjectQueue.Enqueue(bullet);
	}
}
