using System.Collections.Generic;
using UnityEngine;

public partial class RandomDiceCreate // IO
{
	public Tower CreateTower() => _CreateTower();
	public Tower[] seletDice;	// selectDice
}

public partial class RandomDiceCreate // SerializeField
{
	[SerializeField] private Point[] points;
}

public partial class RandomDiceCreate : MonoBehaviour
{
	ra
}

public partial class RandomDiceCreate // body
{
	private readonly List<Point> _remainPoints = new();
	private int _pointNum;
	private Point _point;
	
	private Tower _CreateTower() 
	{
		_point = SelectPoint();
		if (_point != null && !_point.isTower)
		{
			Tower tower = Instantiate(seletDice[Random.Range(0, seletDice.Length)], _point.transform.position, Quaternion.identity);
			_point.isTower = true;
			return tower;
		}
		return null;
	}
	
	private Point SelectPoint() 
	{
		if (_remainPoints.Count == 0)
		{
			return null;
		}
		
		for (int i = 0; i < points.Length; i++)
		{
			if (points[i].isTower == false && points[i] != null)
			{
				_remainPoints.Add(points[i]);
			}
		}
		
		_pointNum = Random.Range(0, _remainPoints.Count);
		return _remainPoints[_pointNum];
	}
}