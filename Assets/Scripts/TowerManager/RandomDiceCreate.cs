using System.Linq;
using UnityEngine;

public partial class RandomDiceCreate // IO
{
	public void ReleaseTower(Tower tower) => _ReleaseTower(tower);
	public Tower CreateTower() => _CreateTower();
	public Tower[] diceDeck;	// selectDice
}

public partial class RandomDiceCreate // SerializeField
{
	[SerializeField] private Point[] points;
}
//
// public partial class RandomDiceCreate : MonoBehaviour
// {
// }

public partial class RandomDiceCreate : MonoBehaviour // body
{
	private Tower NewTower(Point point)
	{
		Tower newTower = Instantiate(diceDeck[Random.Range(0, diceDeck.Length)], point.transform.position, Quaternion.identity);
		newTower.slotId = point.slotId;
		return newTower;
	}

	private Tower _CreateTower()
	{
		Point[] array = points
			.Where(e => !e.occupied)
			.ToArray();

		if (array.Length == 0)
		{
			return null;
		}
		
		int index = Random.Range(0, array.Length);
		
		Point selected = array[index];
		selected.occupied = true;
		return NewTower(selected);
	}

	private void _ReleaseTower(Tower tower)
	{
		points[tower.slotId].occupied = false;
	}
}