using System.Collections.Generic;
using UnityEngine;

public partial class EnemyPool // IO
{
	public void ReturnObject(Enemy enemy) => _ReturnObject(enemy);
	public Enemy GetObject() => _GetObject();
}

public partial class EnemyPool // SerializeField
{
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private int startInitializeCount;
}

public partial class EnemyPool : MonoBehaviour
{
	private void Awake()
	{
		Initialize(startInitializeCount);
	}
}

public partial class EnemyPool // body
{
	private readonly Queue<Enemy> _poolingObjectQueue = new();

	private Enemy CreateNewEnemy() 
	{
		Enemy newObj = Instantiate(enemyPrefab, transform);
		newObj.gameObject.SetActive(false);
		return newObj;
	}
	
	private void Initialize(int count) 
	{
		while (count-- > 0)
		{
			_poolingObjectQueue.Enqueue(CreateNewEnemy());
		}
	}
	
	private void _ReturnObject(Enemy enemy) 
	{
		_poolingObjectQueue.Enqueue(enemy);
		enemy.gameObject.SetActive(false);
	}
	
	private Enemy _GetObject()
	{
		Enemy enemy = _poolingObjectQueue.Count == 0 ? 
			CreateNewEnemy() : _poolingObjectQueue.Dequeue();

		enemy.gameObject.SetActive(true);
		return enemy;
	}
}
