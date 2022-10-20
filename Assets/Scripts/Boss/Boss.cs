using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Boss : Enemy // IO
{
    public void Init(BossData bossData, int hpOffset, GameManager gameManager) => _Init(bossData, hpOffset, gameManager);
}


public partial class Boss// SerializeField
{
    [SerializeField] private Slider _healthSlider;
}

public partial class Boss// Monobehaviour
{
    protected override void Update()
    {
        base.Update();
        _healthSlider.transform.position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + (Vector2.down * 70);
        if (!_isCoolTime)
        {
            StartCoroutine(UseSkill());
        }
    }
}

public partial class Boss // body
{
    private TowerManager _towerManager;
    private float _skillCoolTime = 2.0f;
    private bool _isCoolTime = false;
    private BossData.SkillPointer _skills;
    
    IEnumerator UseSkill()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_skillCoolTime);
        _skills();
        _isCoolTime = false;
    }
    protected override void _OnDamage(float damage) {
        base._OnDamage(damage);
        _healthSlider.value = currHealth;
    }
    public void _Init(BossData bossData, int hpOffset, GameManager gameManager)
    {
        base.Init(bossData, hpOffset, gameManager.enemyManager);
        _skills = bossData.skills[bossData.skillIndex];
        _towerManager = gameManager.towerManager;
        _skillCoolTime = bossData.skillCoolTime;
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currHealth;
    }
}
