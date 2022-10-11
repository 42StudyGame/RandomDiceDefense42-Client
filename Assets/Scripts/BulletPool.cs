using System.Collections.Generic;
using UnityEngine;
	
public class BulletPool : MonoBehaviour {
	// public GameObject bulletPrefab;
	public Bullet bulletPrefab;
	public int startInitializeCount;
	
	private readonly Queue<Bullet> _poolingObjectQueue = new Queue<Bullet>();
	
	private void Awake()
	{
		Initialize(startInitializeCount);
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private Bullet CreateNewBullet() {
		Bullet newObj = Instantiate(bulletPrefab, transform);
		newObj.Init(this);
		newObj.gameObject.SetActive(false);
		return newObj;
	}
	
	private void Initialize(int count) {
		for (int i = 0; i < count; i++)
			_poolingObjectQueue.Enqueue(CreateNewBullet());
	}

	public Bullet GetObject() {
		Bullet bullet = _poolingObjectQueue.Count == 0 ?
			CreateNewBullet() : _poolingObjectQueue.Dequeue();
		bullet.transform.SetParent(null); // 부모 오브젝트에서 나온다.
		bullet.gameObject.SetActive(true);
		return bullet;
	}

	public void ReturnObject(Bullet bullet) {
		_poolingObjectQueue.Enqueue(bullet);
		bullet.transform.SetParent(transform);
		bullet.gameObject.SetActive(false);
	}
}
