using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public partial class DeckTower // IO
{
	public TowerData towerData;
	public void Init() => _Init();
}

public partial class DeckTower // SerializeField
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Draggable draggable;
	[SerializeField] private SortingGroup sortingGroup;
	[SerializeField] private Collider2D collider2D;
}

public partial class DeckTower : MonoBehaviour
{

}

public partial class DeckTower // body
{
	private Vector2 _startPosition;

	void _Init()
	{
		spriteRenderer.sprite = towerData.sprite;
		_startPosition = transform.position;
		if (!draggable)
			return;
		draggable._onBeginDrag = ColliderOff;
		draggable._onDrag = MoveDragObject;
		draggable._onEndDrag = () =>
		{
			ColliderOn();
			BackToPosition();
		};
	}
	
	private void ColliderOff() 
	{
		sortingGroup.sortingOrder += 1;
		collider2D.enabled = false;
	}
	
	private void ColliderOn() 
	{
		sortingGroup.sortingOrder -= 1;
		collider2D.enabled = true;
	}

	private void BackToPosition() 
	{
		transform.position = _startPosition;
	}

	private void MoveDragObject(PointerEventData eventData) 
	{ 
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
	}
}