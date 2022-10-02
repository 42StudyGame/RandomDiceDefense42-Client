using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/DiceData", fileName = "DiceData")]
public class DiceData : ScriptableObject {

	public float	damage;
	public float	attackSpeed;
	public float	attackInterval;
	public string	targeting;
}