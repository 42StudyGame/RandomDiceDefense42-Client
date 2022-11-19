using UnityEngine;

[CreateAssetMenu(fileName = "Electric Tower Skill Data", menuName = "TowerSkillData/ElectricTower")]
public class ElectricTowerSkillData : ScriptableObject, ISkillData
{
    public int ID { get; set; }

    public int bSkillDmg = 30;
    public int cSkillDmg = 3;
    public int pSkillDmg = 20;

    public int offset = 3;
}
