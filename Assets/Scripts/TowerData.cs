using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable/TowerData", fileName = "Tower Data")]
public class TowerData : ScriptableObject
{
	public float damage = 10f;
	public float attackSpeed = 1f;
	public Sprite Sprite;
	
	public TowerModify ModifyDelegate;
	public float Damage() => damage * ModifyDelegate();
	public float AttackSpeed() => attackSpeed * ModifyDelegate.Invoke();
}
