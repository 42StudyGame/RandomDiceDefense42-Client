using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TowerSkillData")]
public class BuffSendingSkill : Skill
{
    public override void BeginCoolDown(GameObject parent)
    {
        Debug.Log("���� ��Ȱ��ȭ to" + parent.name);
    }
    public override void Activate(GameObject parent)
    {
        Debug.Log("���� Ȱ��ȭ to" + parent.name);
    }
}
