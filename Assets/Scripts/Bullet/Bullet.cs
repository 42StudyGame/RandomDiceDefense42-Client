using UnityEngine;
using UnityEngine.Events;

public partial class Bullet // IO
{
	public UnityEvent events;

	public void SetTarget(Enemy target) => _SetTarget(target);
	public void SetDamage(float damage) => _SetDamage(damage);
	public void Init(BulletPool bulletPool) => _Init(bulletPool);
}

// public partial class Bullet // SerializeField
// {
// }

public partial class Bullet : MonoBehaviour
{
	private Enemy _target;
	private BulletPool _pool;
	private Collider2D _targetCollider;
	
	private void Update() 
	{
		if (_target)
		{
			Move();
		}
		else
		{
			_pool.ReturnObject(this);
		}
	}

	private void OnTriggerEnter2D(Collider2D col) 
	{
		if (col == _targetCollider)
		{
			_pool.ReturnObject(this);
			_target.OnDamage(_damage);
			events.Invoke();
			events.RemoveAllListeners();
		}
	}
}

public partial class Bullet // body
{
	private const float MovementSpeed = 10f;
	private float _damage;

	private void _SetTarget(Enemy target)
	{
		_target = target;
		_targetCollider = _target.GetComponent<Collider2D>();
	}

	private void _SetDamage(float damage) 
	{
		_damage = damage;
	}

	private void _Init(BulletPool bulletPool)
	{
		_pool = bulletPool;
	}

	private void Move() 
	{
		if (!_target || !_target.gameObject.activeInHierarchy)
		{
			_pool.ReturnObject(this);
		}

		transform.position = Vector3.MoveTowards
		(transform.position,
			_target.transform.position, 
			MovementSpeed * Time.deltaTime);
	}

	// z fighting
}
