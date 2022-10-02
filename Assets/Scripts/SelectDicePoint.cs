using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDicePoint : MonoBehaviour {
	private List<Point> remain_points;
	private Point[] _points;
	private int pointNum;
    void Awake() {
		_points = GetComponentsInChildren<Point>();
	}

 	public Point SelectPoint() {
		remain_points = new List<Point>();
		for (int i = 0; i < _points.Length; i++)
		{
			if (_points[i].isDice == false && _points[i] != null)
				remain_points.Add(_points[i]);
		}

		if (remain_points.Count == 0)
			return (null);
		pointNum = Random.Range(0, remain_points.Count);
		return (remain_points[pointNum]);
	}
}
