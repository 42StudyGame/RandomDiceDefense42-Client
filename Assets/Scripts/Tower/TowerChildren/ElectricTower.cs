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
    }

    protected override void _Skill()
    {
        base._Skill();

        Enemy temp = _currentTarget;
        
        if (skillData)
        {
            for (int i = 0; i < skillData.offset; i++)
            {
                if (temp)
                {
                    temp.OnDamage(skillData.bSkillDmg);
                    temp = _towerManager.GetPrevTarget(temp.progressToGoal);
                }
            }
        }
    }
}
