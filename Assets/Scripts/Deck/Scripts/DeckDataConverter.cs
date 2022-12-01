using UnityEngine;

public partial class DeckDataConverter // IO
{
	public void SaveData() => _SaveData();
	public DeckTowerList LoadData() => _LoadData();
	public void ApplyJsonToDeck() => _ApplyJsonToDeck();

	public DeckTowerList deckTowerList = new DeckTowerList();
}

public partial class DeckDataConverter // SerializeField
{
}

public partial class DeckDataConverter  : MonoBehaviour
{
}

public partial class DeckDataConverter // body
{
	[SerializeField] private DeckSlot[] deckSlots;
	[SerializeField] private TowerData[] towerDataList;
	private readonly JsonConverter _jsonConverter = new JsonConverter();
	private readonly string _jsonPath = System.IO.Directory.GetCurrentDirectory() + "/Assets/JsonData/";

	public void Start()
	{
		ApplyJsonToDeck();
	}
	public void Init() 
	{
		// ApplyJsonToDeck();
	}
	
	private void _SaveData() 
	{
		string jsonDeckData = _jsonConverter.ObjectToJson(deckTowerList);
		_jsonConverter.CreateJsonFile(_jsonPath, "DeckData", jsonDeckData);
	}
	
	private DeckTowerList _LoadData()
	{
		return _jsonConverter.LoadJsonFile<DeckTowerList>(_jsonPath, "DeckData");
	}

	private void _ApplyJsonToDeck() 
	{
		deckTowerList = LoadData();
		for (int i = 0; i < deckSlots.Length; i++)
		{
			for (int j = 0; j < towerDataList.Length; j++)
			{
				if (deckTowerList.deckTower[i] == towerDataList[j].type)
				{
					deckSlots[i].deckTower.towerData = towerDataList[j];
					deckSlots[i].deckTower.Init();
					break;
				}
			}
		}
	}
}
