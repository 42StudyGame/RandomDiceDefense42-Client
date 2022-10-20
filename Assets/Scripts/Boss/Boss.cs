using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : Enemy // IO
{
    public void Init(BossData bossData, int hpOffset, GameManager gameManager) => _Init(bossData, hpOffset, gameManager);
}


public partial class Boss// SerializeField
{
    [SerializeField] private TowerManager _towerManager;
    //[SerializeField] slider; // 체력바
}

public partial class Boss// Monobehaviour
{
    protected override void Update()
    {
        base.Update();
        if (!_isCoolTime)
        {
            StartCoroutine(useSkill());
        }
    }
}

public partial class Boss // body
{
    private float _skillCoolTime = 2.0f;
    private bool _isCoolTime = false;
    private BossData.SkillPointer _skills;
    
    IEnumerator useSkill()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_skillCoolTime);
        _skills();
        _isCoolTime = false;
    }

    public void _Init(BossData bossData, int hpOffset, GameManager gameManager)
    {
        base.Init(bossData, hpOffset, gameManager.enemyManager);
        _skills = bossData.skills[bossData.skillIndex];
        _towerManager = gameManager.towerManager;
    }
}
