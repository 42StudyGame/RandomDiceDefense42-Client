using UnityEngine;
using UnityEngine.EventSystems;

public partial class DeckSlot // IO 
{
	public int slotId;
	public void Init(string towerType) => _Init(towerType);

	public DeckTower deckTower;

}

public partial class DeckSlot // SerializeField
{
	[SerializeField] private Droppable droppable;
	[SerializeField] private DeckManager deckManager;
}

public partial class DeckSlot : MonoBehaviour
{
	
}

public partial class DeckSlot // body 
{
	private PointerEventData _eventData;
	private void _Init(string towerType) 
	{
		droppable._onDrop = DeckTowerChange;
	}

	private void DeckTowerChange(PointerEventData eventData) 
	{
		DeckTower baseDeckTower = eventData.pointerDrag.GetComponent<DeckTower>();
		if (!deckManager.CheckSameTower(baseDeckTower))
		{
			deckManager.DeckToList(baseDeckTower, slotId);
		}
		else
		{
			deckManager.DeckToDeck(baseDeckTower, slotId);
		}
		deckManager.deckDataConverter.SaveData();
	}
}
