using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    [SerializeField] private BuffHandler buffHandler;

    protected override void _Launch()
    {
        base._Launch();
        _bullet.events.AddListener(_Skill);
        _currentTarget = _towerManager.GetTarget().targetFirst;
        _bullet.SetTarget(_currentTarget);
    }

    protected override void _Skill()
    {
        base._Skill();
        buffHandler.Attach(1);
    }
    
    // 
}
