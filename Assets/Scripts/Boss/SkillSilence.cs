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
    [SerializeField] private GameObject _effectPrefab;
}

public partial class SkillSilence // MonoBehaviour
{
    private void Update()
    {
        if (_boss.IsDestroyed() && _coroutine != null)
        {
            while (_disabledTowers.Count > 0)
            {
                _disabledTowers[_disabledTowers.Count - 1].EnableTower();
                _disabledTowers.RemoveAt(_disabledTowers.Count - 1);
            }
            
            while (_effectObjects.Count > 0)
            {
                Destroy(_effectObjects[_effectObjects.Count - 1]);
                _effectObjects.RemoveAt(_effectObjects.Count - 1);
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
    private float _skillCooltime = 5f;
    private float _moveStopTime = 1.5f;
    private List<Tower> _disabledTowers = new List<Tower>();
    private List<GameObject> _effectObjects = new List<GameObject>();
    private void _Skill(Boss boss)
    {
        _boss = boss;
        _coroutine = StartCoroutine(Silence());
    }

    IEnumerator Silence()
    {
        yield return new WaitForSeconds(_skillCooltime);
        List<Tower> temp = _towerManager.GetTowerList();

        if (_disabledTowers.Count < temp.Count)
        {
            List<Tower> _towers = new List<Tower>();
            for (int i = 0; i < temp.Count; i++)
            {
                _towers.Add(temp[i]);
            }
            for (int i = 0; i < _disabledTowers.Count; i++)
            {
                _towers.Remove(_disabledTowers[i]);
            }
            Tower target = _towers[Random.Range(0, _towers.Count)];
            _effectObjects.Add(Instantiate(_effectPrefab, target.transform.position, target.transform.rotation));
            target.DisableTower();
            _disabledTowers.Add(target);
            _boss.StopMove();
            yield return new WaitForSeconds(_moveStopTime);
            _boss.StartMove();
        }
        _coroutine = StartCoroutine(Silence());
    }
}
