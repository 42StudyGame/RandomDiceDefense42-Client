using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class SkillSilence: ASkills // IO
{
    public override void Skill(Boss boss) => _Skill(boss);
}

public partial class SkillSilence // SerializeField
{
    [SerializeField] private TowerManager _towerManager;
}

public partial class SkillSilence // MonoBehaviour
{
    private void Update()
    {
        if (_boss.IsDestroyed() && _coroutine != null)
        {
            Debug.Log("보스사망");
            for (int i = 0; i < _disabledTowers.Count; i++)
            {
                _disabledTowers[i].EnableTower();
            }
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
public partial class SkillSilence
{
    private Coroutine _coroutine;
    private Boss _boss;
    private List<Tower> _towers;
    private List<Tower> _disabledTowers = new List<Tower>();
    private void _Skill(Boss boss)
    {
        _boss = boss;
        _coroutine = StartCoroutine(Silence());
    }

    IEnumerator Silence()
    {
        yield return new WaitForSeconds(4f);
        _towers = _towerManager.GetTowerList();
        Debug.Log("침묵사용");
        Tower target = _towers[Random.Range(0, _towers.Count)];
        if (target == null)
        {
            _coroutine = StartCoroutine(Silence());
        }
        else
        {
            if (_disabledTowers.Count != _towers.Count)
            {
                for (int i = 0; i < _disabledTowers.Count; i++)
                {
                    if (_disabledTowers[i].Equals(target))
                    {
                        target = _towers[Random.Range(0,_towers.Count)];
                        i = 0;
                    }
                }
                target.DisableTower();
                _disabledTowers.Add(target);
            }
            _coroutine = StartCoroutine(Silence());
        }
    }
}
