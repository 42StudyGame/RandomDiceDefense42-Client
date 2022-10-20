using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BossData", fileName = "Boss Data")]
public class BossData : EnemyData
{
	[SerializeField] private TowerManager towerManager;
	public int skillIndex;
	public float skillCoolTime;
	public delegate void SkillPointer();
	public SkillPointer[] skills = new SkillPointer[]{new SkillPointer(skill1), new SkillPointer(skill2)};

	static void skill1()
	{
		Debug.Log("스킬1 사용");
	}

	static void skill2()
	{
		Debug.Log("스킬2 사용");
	}
}
