using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomDiceCreate : MonoBehaviour {
	
	public GameObject[] seletDice;

	public GameObject SpawnPoint;
	private SelectDicePoint _selectDicePoint;
	private Point _point;
	void Awake() {
		_selectDicePoint = SpawnPoint.GetComponent<SelectDicePoint>();
	}

	public void CreateDice() {
		_point = _selectDicePoint.SelectPoint();
		if (_point != null && !_point.isDice)
		{
			Instantiate(seletDice[Random.Range(0, seletDice.Length)], _point.transform.position, Quaternion.identity);
			_point.isDice = true;
		}
	}

}
