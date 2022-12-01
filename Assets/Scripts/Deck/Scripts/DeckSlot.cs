using TMPro;
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
	[SerializeField] private int maxPowerUp = 800;
	[SerializeField] private int powerUpSpIncrease = 2;
	[SerializeField] private Droppable droppable;
	[SerializeField] private DeckManager deckManager;
	[SerializeField] private Clickable clickable;
	[SerializeField] private TextMeshPro levelText;
	[SerializeField] private TextMeshPro spConsumptionText;
}

public partial class DeckSlot : MonoBehaviour
{
	
}

public partial class DeckSlot // body 
{
	private PointerEventData _eventData;
	private int _spConsumption = 100;
	private int _level = 1;
	
	private void _Init(string towerType) 
	{
		if (droppable)
		{
			droppable._onDrop = DeckTowerChange;
		}
		
		clickable.OnClick = () =>
		{
			if (_spConsumption > maxPowerUp)
			{
				return;
			}
			else if (deckManager.gameManager.sp >= _spConsumption)
			{
				deckManager.gameManager.sp -= _spConsumption;
				_level++;
				_spConsumption *= 2;
				levelText.text = "LV." + _level;
				spConsumptionText.text = _spConsumption.ToString();
				deckManager.gameManager.uiManager.SetSpText(deckManager.gameManager.sp.ToString());
				if (_spConsumption > maxPowerUp)
				{
					levelText.text = "Max";
					spConsumptionText.text = "";
				}
				// TowerManager에서 해당 타입의 타워들 전체 levelUp
				// towerManager는 tower들을 다 가지고 있으니 현재 있는 타워들의 레벨을 올리는건 쉽지만, 앞으로 나올 주사위들의 레벨도 올려야한다.
			}
		};
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
