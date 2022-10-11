using System.Collections.Generic;
using UnityEngine;

public partial class BulletPool // IO
{
	public Bullet GetObject() => _GetObject();
	public void ReturnObject(Bullet bullet) => _ReturnObject(bullet);
}

public partial class BulletPool // SerializeField
{
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private int startInitializeCount;
}

public partial class BulletPool : MonoBehaviour 
{
	private readonly Queue<Bullet> _poolingObjectQueue = new();

	private void Awake()
	{
		Initialize(startInitializeCount);
	}
}

public partial class BulletPool // body
{
	private void Initialize(int count)
	{
		while (count-- > 0)
		{
			_poolingObjectQueue.Enqueue(CreateNewBullet());
		}
	}

	private Bullet CreateNewBullet() 
	{
		Bullet newObj = Instantiate(bulletPrefab, transform);
		newObj.Init(this);
		newObj.gameObject.SetActive(false);
		return newObj;
	}
	
	private Bullet _GetObject()
	{
		Bullet bullet = _poolingObjectQueue.Count == 0 ? CreateNewBullet() : _poolingObjectQueue.Dequeue();
		bullet.transform.SetParent(null); // 부모 오브젝트에서 나온다.
		bullet.gameObject.SetActive(true);
		return bullet;
	}
	
	private void _ReturnObject(Bullet bullet) 
	{
		_poolingObjectQueue.Enqueue(bullet);
		bullet.transform.SetParent(transform);
		bullet.gameObject.SetActive(false);
	}
}
