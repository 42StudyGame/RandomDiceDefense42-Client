using UnityEngine;

[CreateAssetMenu(fileName = "Fire Tower Skill Data", menuName = "TowerSkillData/FireTower")]
public class FireTowerSkillData : ScriptableObject, ISkillData
{
    public int ID { get; set; }

    public int bSkillDmg = 20;
    public int cSkillDmg = 3;
    public int pSkillDmg = 20;

    public float offset = 0.05f;
}
