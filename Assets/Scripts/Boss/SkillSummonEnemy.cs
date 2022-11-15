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

public partial class SkillSummonEnemy // MonoBehaviour
{
    private void Update()
    {
        if (_boss.IsDestroyed() && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
public partial class SkillSummonEnemy
{
    private Boss _boss;
    private Coroutine _coroutine;
    
    private void _Skill(Boss boss)
    {
        _boss = boss;
        _coroutine = StartCoroutine(SummonEnemy());
    }
    
    IEnumerator SummonEnemy()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Enemy 소환");
        _enemyManager.CreateEnemy(0, _boss.hpOffset);
        yield return new WaitForSeconds(0.5f);
        _enemyManager.CreateEnemy(0, _boss.hpOffset);
        yield return new WaitForSeconds(0.5f);
        _enemyManager.CreateEnemy(1, _boss.hpOffset);
        _coroutine = StartCoroutine(SummonEnemy());
    }
}
