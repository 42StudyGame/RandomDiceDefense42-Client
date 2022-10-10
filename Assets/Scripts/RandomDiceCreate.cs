using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDiceCreate : MonoBehaviour {
	private List<Point> _remainPoints;
	private Point[] _points;
	private int _pointNum;
	
	public GameObject[] seletDice;
	private Point _point;
	
    void Awake() {
		_points = GetComponentsInChildren<Point>();
	}
	public void CreateTower() {
		_point = SelectPoint();
		if (_point != null && !_point.isTower)
		{
			Instantiate(seletDice[Random.Range(0, seletDice.Length)], _point.transform.position, Quaternion.identity);
			_point.isTower = true;
		}
	}
	
 	public Point SelectPoint() {
		_remainPoints = new List<Point>();
		for (int i = 0; i < _points.Length; i++)
		{
			if (_points[i].isTower == false && _points[i] != null)
				_remainPoints.Add(_points[i]);
		}
		if (_remainPoints.Count == 0)
			return (null);
		_pointNum = Random.Range(0, _remainPoints.Count);
		return (_remainPoints[_pointNum]);
	}
}
