using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {
	public float speed = 5f;
	private Transform[] movePoint;
	private int currentLine = 0;
	private void Start() {
		movePoint = new Transform[3];
		movePoint[0] = GameObject.Find("LinePoint1").transform;
		movePoint[1] = GameObject.Find("LinePoint2").transform;
		movePoint[2] = GameObject.Find("LinePoint3").transform;
	}

	void Update() {
		MovePath();
	}

	public void MovePath() {
		transform.position = Vector2.MoveTowards
			(transform.position, 
			movePoint[currentLine].position, 
			speed * Time.deltaTime);
		if (transform.position == movePoint[currentLine].transform.position)
			currentLine++;
		if (currentLine == movePoint.Length)
			Destroy(gameObject);
	}
}
