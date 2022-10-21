using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Boss : Enemy // IO
{
    public void Init(BossData bossData, int hpOffset, GameManager gameManager, ISkills skills) => _Init(bossData, hpOffset, gameManager, skills);
}


public partial class Boss// SerializeField
{
    [SerializeField] private Slider _healthSlider;
}

public partial class Boss// Monobehaviour
{
    private Coroutine _coroutine;

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    protected override void Update()
    {
        base.Update();
        _healthSlider.transform.position = (Vector2)Camera.main.WorldToScreenPoint(transform.position) + (Vector2.down * 70);
    }
}

public partial class Boss // body
{
    private TowerManager _towerManager;
    private float _skillCoolTime = 2.0f;
    private bool _isCoolTime = false;
    private delegate void SkillPointer();
    private SkillPointer _skillPointer;
    
    IEnumerator UseSkill()
    {
        if (_skillPointer != null)
        {
            yield return new WaitForSeconds(_skillCoolTime);
            _skillPointer();
            _coroutine = StartCoroutine(UseSkill());
        }
    }
    protected override void _OnDamage(float damage) {
        base._OnDamage(damage);
        _healthSlider.value = currHealth;
    }
    public void _Init(BossData bossData, int hpOffset, GameManager gameManager, ISkills skills)
    {
        base.Init(bossData, hpOffset, gameManager.enemyManager);
        _skillPointer = new SkillPointer(skills.Skill);
        _towerManager = gameManager.towerManager;
        _skillCoolTime = bossData.skillCoolTime;
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currHealth;
        _coroutine = StartCoroutine(UseSkill());
    }
}
