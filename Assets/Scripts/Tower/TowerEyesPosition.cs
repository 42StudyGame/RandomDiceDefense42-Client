using UnityEngine;

public partial class TowerEyesPosition // IO 
{
	public void FindEyesPosition(int grade) => _FindEyesPosition(grade);
	public void Init() => _Init();
}

public partial class TowerEyesPosition // SerializeField
{
	[SerializeField] private GameObject[] eyesPoint;
}

public partial class TowerEyesPosition : MonoBehaviour// body
{
	private GameObject[,] _eyes = new GameObject[6, 6];
	private void _Init()
	{
		_eyes[0, 0] = eyesPoint[3];

		_eyes[1, 0] = eyesPoint[1];
		_eyes[1, 1] = eyesPoint[5];
		
		_eyes[2, 0] = eyesPoint[1];
		_eyes[2, 1] = eyesPoint[3];
		_eyes[2, 2] = eyesPoint[5];
		
		_eyes[3, 0] = eyesPoint[0];
		_eyes[3, 1] = eyesPoint[1];
		_eyes[3, 2] = eyesPoint[5];
		_eyes[3, 3] = eyesPoint[6];
		
		_eyes[4, 0] = eyesPoint[0];
		_eyes[4, 1] = eyesPoint[1];
		_eyes[4, 2] = eyesPoint[3];
		_eyes[4, 3] = eyesPoint[5];
		_eyes[4, 4] = eyesPoint[6];
		
		_eyes[5, 0] = eyesPoint[0];
		_eyes[5, 1] = eyesPoint[1];
		_eyes[5, 2] = eyesPoint[2];
		_eyes[5, 3] = eyesPoint[4];
		_eyes[5, 4] = eyesPoint[5];
		_eyes[5, 5] = eyesPoint[6];
		FalseEyes();
		ActiveEyes(1);
	}
	private void FalseEyes() 
	{
		for (int i = 0; i < eyesPoint.Length; i++)
		{
			eyesPoint[i].SetActive(false);
		}
	}

	private void ActiveEyes(int grade) 
	{
		grade--;
		for (int i = 0; _eyes[grade, i]; i++)
		{
			_eyes[grade, i].SetActive(true);			
		}
	}

	private void _FindEyesPosition(int grade)
	{
		FalseEyes();
		ActiveEyes(grade);
	}
}
