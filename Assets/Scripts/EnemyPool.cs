using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private int startInitializeCount;
	
	private Queue<Enemy> poolingObjectQueue = new Queue<Enemy>();
	
	private void Awake()
	{
		Initialize(startInitializeCount);
	}

	private Enemy CreateNewEnemy() {
		Enemy newObj = Instantiate(enemyPrefab, transform).GetComponent<Enemy>();
		newObj.gameObject.SetActive(false);
		return newObj;
	}
	
	private void Initialize(int count) {
		for (int i = 0; i < count; i++)
			poolingObjectQueue.Enqueue(CreateNewEnemy());
	}

	public Enemy GetObject() {
		if (poolingObjectQueue.Count > 0)
		{
			Enemy obj = poolingObjectQueue.Dequeue();
			obj.transform.SetParent(null);
			obj.gameObject.SetActive(true);
			return obj;
		}
		else
		{
			Enemy newObj = CreateNewEnemy();
			newObj.transform.SetParent(null);
			newObj.gameObject.SetActive(true);
			return newObj;
		}
	}

	//가져다 쓴 Enemy를 다시 오브젝트풀에 되돌려 놓는 메서드
	public void ReturnObject(Enemy enemy) {
		enemy.gameObject.SetActive(false);
		enemy.transform.SetParent(transform);
		poolingObjectQueue.Enqueue(enemy);
	}
}
