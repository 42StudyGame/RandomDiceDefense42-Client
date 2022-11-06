using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class SkillPrint : ASkills // IO
{
    public override void Skill(Boss boss) => _Skill(boss);
}

public partial class SkillPrint // Serializefiled
{
    [SerializeField] protected TowerManager towerManager;
}

public partial class SkillPrint // body
{
    private Boss _boss;
    
    private void _Skill(Boss boss)
    {
        _boss = boss;
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        yield return new WaitForSeconds(3f);
        if (!_boss.IsDestroyed())
        {
            Debug.Log("스킬 사용");
            StartCoroutine(Print2());
        }
    }

    IEnumerator Print2()
    {
        yield return new WaitForSeconds(3f);
        if (!_boss.IsDestroyed())
        {
            Debug.Log("스킬2 사용");
            StartCoroutine(Print());
        }
    }
}
