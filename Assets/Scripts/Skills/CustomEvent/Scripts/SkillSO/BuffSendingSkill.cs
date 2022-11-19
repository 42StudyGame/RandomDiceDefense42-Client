using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TowerSkillData")]
public class BuffSendingSkill : Skill
{
    public override void BeginCoolDown(GameObject parent)
    {
        Debug.Log("버프 비활성화 to" + parent.name);
    }
    public override void Activate(GameObject parent)
    {
        Debug.Log("버프 활성화 to" + parent.name);
    }
}
