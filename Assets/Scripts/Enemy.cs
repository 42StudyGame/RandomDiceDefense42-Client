using UnityEngine;

// 모든 멤버변수는 기본적으로 private입니다.
// 만일 외부에서 접근해야 하면 get/set함수를 만드시는 것이 훨씬 캡슐화에 좋습니다.
// 함수를 구현할 때 필요한 변수들은 자유롭게 추가하셔도 됩니다. 다만 private으로 해주세요.

public partial class Enemy // IO
{
	public float maxHealth { get; private set; }
	public float currHealth { get; private set; }
	public float speed { get; private set; }
	public int sp { get; private set; }
	public float progressToGoal { get; private set; }

	public void Init(EnemyData enemyData, int wave, EnemyManager enemyManager) => _Init(enemyData, wave, enemyManager);
	public void OnDamage(float damage) => _OnDamage(damage);
}

public partial class Enemy // SerializeField
{
	[SerializeField] private SpriteRenderer _enemySpriteRenderer;
	[SerializeField] private TextMesh textMesh;
}

public partial class Enemy : MonoBehaviour
{
	private void Awake()
	{
	}

	void Update() {
		Move();
		GetProgressToGoal();
	}
}

public partial class Enemy // body
{
	private EnemyManager _enemyManager;
	private Transform[] _wayPoints;
	private int _currentLine = 0;
	private float _runDistance;

	private void _Init(EnemyData enemyData, int wave, EnemyManager enemyManager)
	{
		_enemyManager = enemyManager;
		_wayPoints = _enemyManager.wayPoints;
		speed = enemyData.speed;
		maxHealth = enemyData.health;
		currHealth = enemyData.health;
		sp = enemyData.sp;
		_enemySpriteRenderer.sprite = enemyData.sprite;

		progressToGoal = 0f;
		_runDistance = 0f;
	}

	private void Move() {
		transform.position = Vector2.MoveTowards
		(transform.position,
			_wayPoints[_currentLine].position,
			speed * Time.deltaTime);

		if (Vector2.SqrMagnitude(transform.position - _wayPoints[_currentLine].transform.position) <= 0.01f)
			_currentLine++;
		if (_currentLine == _wayPoints.Length)
			Die();
	}

	private void Die() {
		_enemyManager.EnemyGoal();
		_enemyManager.DestroyEnemy(this);
	}

	private void _OnDamage(float damage) {
		currHealth -= damage;
		textMesh.text = currHealth.ToString();
		if (currHealth <= 0)
			Die();
	}

	private void GetProgressToGoal()
	{
		_runDistance += speed * Time.deltaTime;
		progressToGoal = _runDistance / _enemyManager.maxDistToGoal;
	}
}
