using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public partial class Draggable // IO
{
	public void OnBeginDrag(PointerEventData eventData) => _OnBeginDrag(eventData);
	public void OnDrag(PointerEventData eventData) => _OnDrag(eventData);
	public void OnEndDrag(PointerEventData eventData) => _OnEndDrag(eventData);
	public void OnDrop(PointerEventData eventData) => _OnDrop(eventData);
	public void Init(Tower tower, TowerManager towerManager) => _Init(tower, towerManager);
}

public partial class Draggable : MonoBehaviour
{

}

public partial class Draggable : IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
	private void _OnBeginDrag(PointerEventData eventData) {
		_sortingGroup.sortingOrder += 1;
		_collider2D.enabled = false;	
	}
	
	private void _OnDrag(PointerEventData eventData) 
	{
		// transform.position += (Vector3)(eventData.delta / (_canvas.transform.position.z) * transform.localScale);
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
	}

	private void _OnEndDrag(PointerEventData eventData) 
	{
		_collider2D.enabled = true;
		transform.position = _tower.GetStartPosition();
		_sortingGroup.sortingOrder -= 1;
	}
	
	private void _OnDrop(PointerEventData eventData) 
	{
		Tower otherTower = eventData.pointerDrag.GetComponent<Tower>();
		
		if (_tower.towerData.type == otherTower.towerData.type
			&& _tower.GetGrade() == otherTower.GetGrade())
		{
			_towerManager.Merge(_tower, otherTower);
		}
	}
}

public partial class Draggable // body 
{
	private Tower _tower;
	private TowerManager _towerManager;
	private Collider2D _collider2D;
	private SortingGroup _sortingGroup;
	
	private void _Init(Tower tower, TowerManager towerManager)
	{
		_tower = tower;
		_towerManager = towerManager;
		_collider2D = GetComponent<Collider2D>();
		_sortingGroup = _tower.GetComponent<SortingGroup>();
	}
}
