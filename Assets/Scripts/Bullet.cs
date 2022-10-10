using UnityEngine;

public class Bullet : MonoBehaviour
{
	private GameObject _target;
	private float movementSpeed = 1;
	private BulletPool _pool;
	private Collider2D _targetCollider;

	private void Awake() {
		// _target = GameObject.FindWithTag("Enemy");
		// _targetCollider = _target.GetComponent<Collider2D>();
		_pool = FindObjectOfType<BulletPool>();
	}

	// private void OnEnable() 
	// {
	// 	_target = GameObject.FindWithTag("Enemy");
	// }

	public void SetTarget(GameObject target)
	{
		_target = target;
		_targetCollider = _target.GetComponent<Collider2D>();
		// getComponent 안쓸수 있으면 좋음. 비싸고, 빈도도 잦음
	}

	private void Update() {
		if (_target)
			MovePath();
		else
			_pool.ReturnObject(this);
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col == _targetCollider)
			_pool.ReturnObject(this);
	}

	private void MovePath() {
		// if (!_target)
		if (!_target || !_target.activeInHierarchy)
			_pool.ReturnObject(this);
		transform.position = Vector3.MoveTowards
		(transform.position, 
			_target.transform.position, 
			movementSpeed * Time.deltaTime);
	}
	
	// z fighting
}
