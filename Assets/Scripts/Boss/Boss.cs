using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class Boss : Enemy // IO
{
    public void Init(EnemyData enemyData, int hpOffset, GameManager gameManager, UnityAction<Boss> skills) => _Init(enemyData, hpOffset, gameManager, skills);
}


public partial class Boss// SerializeField
{
    [SerializeField] private Slider _healthSlider;
}

public partial class Boss// Monobehaviour
{
    private Coroutine _coroutine;

    protected override void Update()
    {
        base.Update();
        _healthSlider.transform.position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + (Vector2.down * 70);
    }
}

public partial class Boss // body
{
    private UnityAction<Boss> _skill;

    private void UseSkill()
    {
        _skill?.Invoke(this);
    }

    protected override void _Die()
    {
        _enemyManager.DestroyBoss(this);
        _enemyManager.enemyTarget.SetGeneralTarget();
    }
    protected override void _OnDamage(float damage) {
        base._OnDamage(damage);
        _healthSlider.value = currHealth;
    }
    public void _Init(EnemyData enemyData, int hpOffset, GameManager gameManager, UnityAction<Boss> skills)
    {
        base.Init(enemyData, hpOffset, gameManager.enemyManager);
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currHealth;
        _skill = skills;
        UseSkill();

        _damage = 2;
    }
}
