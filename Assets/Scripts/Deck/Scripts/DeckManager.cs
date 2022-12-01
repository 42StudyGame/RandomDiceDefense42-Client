using System;
using UnityEngine;

public partial class DeckManager // IO
{
	public void Init() => _Init();
	public bool CheckSameTower(DeckTower baseDeckTower) => _CheckSameTower(baseDeckTower);
	public void DeckToList(DeckTower baseDeckTower, int slotId) => _DeckToList(baseDeckTower, slotId);
	public void DeckToDeck(DeckTower baseDeckTower, int slotId) => _DeckToDeck(baseDeckTower, slotId);
	
	public DeckDataConverter deckDataConverter;
	public GameManager gameManager;
}

public partial class DeckManager // SerializeField 
{
	[SerializeField] private DeckSlot[] slot;
	[SerializeField] private DeckTower[] towerList;
}

public partial class DeckManager : MonoBehaviour 
{
	private void OnEnable() 
	{
		Init();	
	}
}

public partial class DeckManager // body
{
	private void _Init() 
	{
		for (int i = 0; i < slot.Length; i++)
		{
			slot[i].Init(slot[i].deckTower.towerData.type);
		}
		for (int i = 0; i < towerList.Length; i++)
		{
			towerList[i].Init();
		}
	}
	
	private bool _CheckSameTower(DeckTower baseDeckTower)
	{
		for (int i = 0; i < slot.Length; i++)
		{
			if (slot[i].deckTower.towerData.type == baseDeckTower.towerData.type)
			{
				return true;
			}
		}
		return false;
	}

	private void _DeckToList(DeckTower baseDeckTower, int slotId) 
	{
		slot[slotId].deckTower.towerData = baseDeckTower.towerData;
		slot[slotId].deckTower.Init();
		deckDataConverter.deckTowerList.deckTower[slotId] = baseDeckTower.towerData.type;
	}

	private void _DeckToDeck(DeckTower baseDeckTower, int slotId)
	{
		for (int i = 0; i < slot.Length; i++)
		{
			if (slot[i].deckTower.towerData.type == baseDeckTower.towerData.type)
			{
				(deckDataConverter.deckTowerList.deckTower[i], deckDataConverter.deckTowerList.deckTower[slotId])
					= (deckDataConverter.deckTowerList.deckTower[slotId], deckDataConverter.deckTowerList.deckTower[i]);
				(slot[i].deckTower.towerData, slot[slotId].deckTower.towerData)
					= (slot[slotId].deckTower.towerData, slot[i].deckTower.towerData);
				slot[i].deckTower.Init();
				slot[slotId].deckTower.Init();
				return;
			}
		}
	}
}
