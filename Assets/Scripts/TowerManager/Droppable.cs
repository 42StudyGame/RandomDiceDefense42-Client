using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class Droppable // IO
{
	public void OnDrop(PointerEventData eventData) => _OnDrop(eventData);
	
	public UnityAction<PointerEventData> _onDrop;
}

public partial class Droppable : MonoBehaviour
{
}

public partial class Droppable : IDropHandler
{
	private void _OnDrop(PointerEventData eventData)
	{
		_onDrop(eventData);
	}
}
