using UnityEngine;

public partial class TowerEyesPosition // IO 
{
	public void FindEyesPosition(int grade) => _FindEyesPosition(grade);
}

public partial class TowerEyesPosition // SerializeField
{
	[SerializeField] private GameObject[] eyesPoint;
}

public partial class TowerEyesPosition : MonoBehaviour 
{
	private void Awake() 
	{
		Init();
		SetOneEyes();
	}
}

public partial class TowerEyesPosition // body 
{
	private void Init() 
	{
		for (int i = 0; i < eyesPoint.Length; i++)
		{
			eyesPoint[i].SetActive(false);
		}
	}
	
	private void SetOneEyes() 
	{
		eyesPoint[3].SetActive(true);
	}

	private void SetTweEyes()
	{
		eyesPoint[1].SetActive(true);
		eyesPoint[5].SetActive(true);

	}	
	
	private void SetThreeEyes()
	{
		eyesPoint[1].SetActive(true);
		eyesPoint[3].SetActive(true);
		eyesPoint[5].SetActive(true);
	}
	
	private void SetFourEyes()
	{
		eyesPoint[0].SetActive(true);
		eyesPoint[1].SetActive(true);
		eyesPoint[5].SetActive(true);
		eyesPoint[6].SetActive(true);
	}
	
	private void SetFiveEyes()
	{
		eyesPoint[0].SetActive(true);
		eyesPoint[1].SetActive(true);
		eyesPoint[3].SetActive(true);
		eyesPoint[5].SetActive(true);
		eyesPoint[6].SetActive(true);
	}
	
	private void SetSixEyes()
	{
		eyesPoint[0].SetActive(true);
		eyesPoint[1].SetActive(true);
		eyesPoint[2].SetActive(true);
		eyesPoint[4].SetActive(true);
		eyesPoint[5].SetActive(true);
		eyesPoint[6].SetActive(true);
	}

	private void _FindEyesPosition(int grade)
	{
		Init();
		if (grade == 1)
			SetOneEyes();
		if (grade == 2)
			SetTweEyes();
		if (grade == 3)
			SetThreeEyes();
		if (grade == 4)
			SetFourEyes();
		if (grade == 5)
			SetFiveEyes();
		if (grade == 6)
			SetSixEyes();
	}
}

