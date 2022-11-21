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
    [SerializeField] private GameObject _effectToBossPrefab;
    [SerializeField] private GameObject _effectToStartPointPrefab;
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
    private float _skillCooltime = 5f;
    private float _spawnDelay = 0.5f;
    private float _moveStopTime = 0.5f;
    
    private void _Skill(Boss boss)
    {
        _boss = boss;
        _coroutine = StartCoroutine(_SummonEnemy());
    }
    
    IEnumerator _SummonEnemy()
    {
        yield return new WaitForSeconds(_skillCooltime);
        _boss.StopMove();
        GameObject effectToBoss = Instantiate(_effectToBossPrefab, _boss.transform.position, _boss.transform.rotation);
        GameObject effectToStartPoint = Instantiate(_effectToStartPointPrefab, _enemyManager.enemyLine.wayPoints[0].transform.position,
            _enemyManager.enemyLine.wayPoints[0].transform.rotation);
        
        _enemyManager.CreateEnemy(0, _boss.hpOffset);
        yield return new WaitForSeconds(_spawnDelay);
        _enemyManager.CreateEnemy(0, _boss.hpOffset);
        yield return new WaitForSeconds(_spawnDelay);
        _enemyManager.CreateEnemy(1, _boss.hpOffset);
        yield return new WaitForSeconds(_moveStopTime);
        _boss.StartMove();
        Destroy(effectToBoss);
        Destroy(effectToStartPoint);
        _coroutine = StartCoroutine(_SummonEnemy());
    }
}
