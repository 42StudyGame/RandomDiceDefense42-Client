using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire Tower Data", menuName = "TowerSkillData/FireTower")]
public class FireTowerSkillData : ScriptableObject, ISkillData
{
    public int ID { get; set; }

    public int basicSkillDamage = 20;
    public int classSkillDamage = 3;
    public int powerSkillDamage = 20;

    public float offset = 0.05f;
}
