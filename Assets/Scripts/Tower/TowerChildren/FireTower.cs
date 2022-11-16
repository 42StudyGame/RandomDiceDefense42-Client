using System.Collections.Generic;

public class FireTower : Tower
{
    public FireTowerSkillData skillData;


    protected override void _Launch()
    {
        base._Launch();
        _bullet.events.AddListener(_Skill);
    }

    protected override void _Skill()
    {
        base._Skill();
        if (skillData)
        {
            List<Enemy> enemyList = _towerManager.GetNearTarget(_currentTarget.progressToGoal, skillData.offset);
            foreach (Enemy enemy in enemyList)
            {
                // TODO: Effect
                enemy.OnDamage(skillData.bSkillDmg);
            }
        }
    }
}
