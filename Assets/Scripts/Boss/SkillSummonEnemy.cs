using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public partial class SkillSummonEnemy : ASkills // IO
{
    public override void Skill(Boss boss) => _Skill(boss);
}

public partial class SkillSummonEnemy // SerializeField
{
    [SerializeField] private EnemyManager _enemyManager;
}

public partial class SkillSummonEnemy
{
    private Boss _boss;
    
    private void _Skill(Boss boss)
    {
        _boss = boss;
        StartCoroutine(SummonEnemy());
    }
    
    IEnumerator SummonEnemy()
    {
        yield return new WaitForSeconds(4f);
        if (_boss.IsDestroyed())
        {
            yield return null;
        }
        else
        {
            _enemyManager.CreateEnemy(0, _boss.hpOffset);
            yield return new WaitForSeconds(0.5f);
        }
        if (_boss.IsDestroyed())
        {
            yield return null;
        }
        else
        {
            _enemyManager.CreateEnemy(0, _boss.hpOffset);
            yield return new WaitForSeconds(0.5f);
        }
        if (_boss.IsDestroyed())
        {
            yield return null;
        }
        else
        {
            _enemyManager.CreateEnemy(1, _boss.hpOffset);
            StartCoroutine(SummonEnemy());
        }
    }
}
