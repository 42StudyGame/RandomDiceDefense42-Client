using UnityEngine;
using UnityEngine.EventSystems;

public partial class Draggable // IO
{
	public void OnBeginDrag(PointerEventData eventData) => _OnBeginDrag(eventData);
	public void OnDrag(PointerEventData eventData) => _OnDrag(eventData);
	public void OnEndDrag(PointerEventData eventData) => _OnEndDrag(eventData);
	public void OnDrop(PointerEventData eventData) => _OnDrop(eventData);
}

public partial class Draggable : MonoBehaviour
{
	private Vector2 _startPosition;
	// private RectTransform _rectTransform;
	private Canvas _canvas;

	private void Start() {
		_startPosition = transform.position;
		_canvas = FindObjectOfType<Canvas>();
	}
}

public partial class Draggable : IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler {
	private void _OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("Begin");
	}
	
	private void _OnDrag(PointerEventData eventData) 
	{
		transform.position += (Vector3)(eventData.delta / (_canvas.transform.position.z) * transform.localScale);
		Debug.Log("Drag");
	}

	private void _OnEndDrag(PointerEventData eventData) 
	{
		Debug.Log("End");
	}
	
	private void _OnDrop(PointerEventData eventData)
	{
		transform.position = _startPosition;
		Debug.Log("Drop");
	}
}

public partial class Draggable // body 
{
	
}
