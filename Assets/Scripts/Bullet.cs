using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Enemy _target;
	private float movementSpeed = 10f;
	private BulletPool _pool;
	private Collider2D _targetCollider;
	private float _damage;

	public void SetTarget(Enemy target)
	{
		_target = target;
		_targetCollider = _target.GetComponent<Collider2D>();
		// getComponent 안쓸수 있으면 좋음. 비싸고, 빈도도 잦음
	}

	public void SetDamage(float damage) {
		_damage = damage;
	}
	public void Init(BulletPool bulletPool) {
		_pool = bulletPool;
	}
	private void Update() {
		if (_target)
			Move();
		else
			_pool.ReturnObject(this);
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col == _targetCollider)
		{
			_pool.ReturnObject(this);
			_target.OnDamage(_damage);
		}
	}

	private void Move() {
		if (!_target || !_target.gameObject.activeInHierarchy)
			_pool.ReturnObject(this);
		transform.position = Vector3.MoveTowards
		(transform.position, 
			_target.transform.position, 
			movementSpeed * Time.deltaTime);
	}
	
	// z fighting
}
