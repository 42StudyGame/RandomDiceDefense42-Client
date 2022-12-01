using UnityEngine;

public class TowerListManager : MonoBehaviour 
{
	[SerializeField] private DeckTower[] deckTowers;
	private void OnEnable()
	{
		for (int i = 0; i < deckTowers.Length; i++)
		{
			deckTowers[i].Init();
		}
	}
}
