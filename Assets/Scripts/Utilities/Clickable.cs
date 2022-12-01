using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class Clickable // IO 
{
	public void OnPointerClick(PointerEventData eventData) => _OnPointerClick(eventData);
	public UnityAction OnClick;
}

public partial class Clickable : MonoBehaviour // MonoBehaviour 
{
}

public partial class Clickable : IPointerClickHandler 
{
	private void _OnPointerClick(PointerEventData eventData) 
	{
		OnClick();
	}
}
