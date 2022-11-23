using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TowerData", fileName = "Tower Data")]
public class TowerData : ScriptableObject 
{
	public string type = "Tower";
	public float damage = 10f;
	public float attackSpeed = 1f;
	public float gradeDamageIncrease = 1f;
	public float gradeAttackSpeedIncrease = 1f;
	public Sprite sprite;
	
	// public TowerModify ModifyDelegate;
	// public float Damage() => damage * ModifyDelegate();
	// public float AttackSpeed() => attackSpeed * ModifyDelegate.Invoke();
}
