using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BulletPool // IO
{
	public Bullet GetObject() => _GetObject();
	public void ReturnObject(Bullet bullet) => _ReturnObject(bullet);
	public void ReturnAllObject() => _ReturnAllObject();
}

public partial class BulletPool // SerializeField
{
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private int startInitializeCount;
}

public partial class BulletPool : MonoBehaviour 
{
	private readonly Queue<Bullet> _poolingObjectQueue = new();
	private readonly Dictionary<int, Bullet> _rentalDictionary = new();

	private void Awake()
	{
		Initialize(startInitializeCount);
	}

	private void OnDisable()
	{
		_ReturnAllObject();
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
		Bullet bullet = _poolingObjectQueue.Count == 0 ?
			CreateNewBullet() :
			_poolingObjectQueue.Dequeue();
		_rentalDictionary[bullet.GetInstanceID()] = bullet;
		bullet.gameObject.SetActive(true);
		return bullet;
	}
	
	private void _ReturnObject(Bullet bullet)
	{
		int instanceId = bullet.GetInstanceID();
		
		if (!_rentalDictionary.ContainsKey(instanceId))
		{
			throw new Exception($"{instanceId} is not rental object");
		}

		_rentalDictionary.Remove(instanceId);
		_poolingObjectQueue.Enqueue(bullet);
		bullet.gameObject.SetActive(false);
	}

	private void _ReturnAllObject()
	{
		KeyValuePair<int, Bullet>[] array = _rentalDictionary.ToArray();
		foreach (KeyValuePair<int, Bullet> item in array)
		{
			_ReturnObject(item.Value);
		}
	}
}
