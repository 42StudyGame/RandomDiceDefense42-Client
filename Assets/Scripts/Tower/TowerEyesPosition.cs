using UnityEngine;

public partial class TowerEyesPosition // IO 
{
	public void FindEyesPosition(int grade) => _FindEyesPosition(grade);
	public void Init() => _FindEyesPosition(1);
}

public partial class TowerEyesPosition // SerializeField
{
	[SerializeField] private GameObject[] eyesPoint;
}

public partial class TowerEyesPosition : MonoBehaviour// body
{
	private readonly bool[][] _dots = new bool[][]
	{
		// 0 left top, 1 right top, 2 left mid, 3 center, 4 right mid, 5 left bot, 6 right bot
		new bool[] { false, false, false, true, false, false, false }, 
		new bool[] { false, true, false, false, false, true, false }, 
		new bool[] { false, true, false, true, false, true, false },
		new bool[] { true, true, false, false, false, true, true }, 
		new bool[] { true, true, false, true, false, true, true }, 
		new bool[] { true, true, true, false, true, true, true }
	};

	private void Activate(int grade) 
	{
		for (int i = 0; i < eyesPoint.Length; ++i)
		{
			eyesPoint[i].SetActive(_dots[grade - 1][i]);
		}
	}

	private void _FindEyesPosition(int grade)
	{
		Activate(grade);
	}
}