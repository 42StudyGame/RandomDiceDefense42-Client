using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public partial class Draggable // IO
{
	public void OnBeginDrag(PointerEventData eventData) => _OnBeginDrag(eventData);
	public void OnDrag(PointerEventData eventData) => _OnDrag(eventData);
	public void OnEndDrag(PointerEventData eventData) => _OnEndDrag(eventData);
	public void Init(Tower tower, TowerManager towerManager) => _Init(tower, towerManager);

	public UnityAction _onBeginDrag;
	public UnityAction<PointerEventData> _onDrag;
	public UnityAction _onEndDrag;
	
}

public partial class Draggable : MonoBehaviour
{

}

public partial class Draggable : IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private void _OnBeginDrag(PointerEventData eventData) 
	{
		_onBeginDrag();
	}
	
	private void _OnDrag(PointerEventData eventData) 
	{
		_onDrag(eventData);
	}

	private void _OnEndDrag(PointerEventData eventData) 
	{
		_onEndDrag();
	}
}

public partial class Draggable // body 
{
	private void _Init(Tower tower, TowerManager towerManager)
	{
	}
}
