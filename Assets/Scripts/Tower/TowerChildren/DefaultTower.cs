
public class DefaultTower : Tower
{
    protected override void _Launch()
    {
        base._Launch();
        
        _currentTarget = _towerManager.GetTarget().targetFirst;
        _bullet.SetTarget(_currentTarget);
    }

    protected override void _Skill()
    {
        base._Skill();
    }
}
