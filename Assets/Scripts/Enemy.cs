using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 멤버변수는 기본적으로 private입니다.
// 만일 외부에서 접근해야 하면 get/set함수를 만드시는 것이 훨씬 캡슐화에 좋습니다.
// 함수를 구현할 때 필요한 변수들은 자유롭게 추가하셔도 됩니다. 다만 private으로 해주세요.

public class Enemy : MonoBehaviour {
	[SerializeField] public EnemyManager _enemyManager;
	[SerializeField] public EnemyPool _enemyPool;
	[SerializeField] public EnemyData enemyData; //ScriptableObject

	// 골라인까지의 실제 거리, 현재 이동한 거리
	// 적의 골라인까지의 진척도 (0 ~ 1), get함수 필요

	// 적의 현재 체력 (해당 값 구하는 공식은 아직 못 알아냈습니다.)

	public void Init() {
	}
	public float speed = 5f;
	private Transform[] movePoint;
	private int currentLine = 0;
	private void Awake() {
		movePoint = new Transform[3];
		movePoint[0] = GameObject.Find("LinePoint1").transform;
		movePoint[1] = GameObject.Find("LinePoint2").transform;
		movePoint[2] = GameObject.Find("LinePoint3").transform;
	}

	void Update() {
		Move();
	}

	private void Move() {
		transform.position = Vector2.MoveTowards
		(transform.position, 
			movePoint[currentLine].position, 
			speed * Time.deltaTime);
		if (transform.position == movePoint[currentLine].transform.position)
			currentLine++;
		if (currentLine == movePoint.Length)
			Die();
	}
	private void Die() {
		_enemyManager.EnemyGoal();
		/** private die()
		* 몬스터가 체력이 다 닳거나 골라인에 도달하면 발생하는 함수
		* 해당 오브젝트를 오브젝트풀에 다시 넣어달라고 EnemyMamager에 요청한다
		*/
	}

	private void GetProgressToGoal()
	{
		/** private getProgressToGoal()
		* 골라인까지 남은 거리를 계산하는 함수 (update함수 내에서 실행)
		* 현재 이동한 총 거리 / 골라인까지의 실제 거리
		*/
	}
}