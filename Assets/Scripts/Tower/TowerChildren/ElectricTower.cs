using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTower : Tower
{
    public ElectricTowerSkillData skillData;
    
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

        Enemy temp = _currentTarget;
        Enemy temp2;
        
        if (skillData)
        {
            for (int i = 0; i < skillData.offset; i++)
            {
                if (temp)
                {
                    temp.OnDamage(skillData.bSkillDmg);
                    temp2 = _towerManager.GetTarget().GetPrevTarget(temp.progressToGoal);
                    if (temp2 == temp)
                        break;
                    temp = temp2;
                }
            }
        }
    }
}
