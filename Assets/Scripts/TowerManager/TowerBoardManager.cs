using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TowerBoardManager // IO
{
    public void Init(List<Tower> towers) => _Init(towers);
}

public partial class TowerBoardManager // Body
{
    private List<Tower> _towers;
    
    private void _Init(List<Tower> towers)
    {
        _towers = towers;
    }

    private List<Tower> _GetNearTowers(Tower tower)
    {
        int num = _towers.IndexOf(tower);
        
        return null;
    }

    private int _HasSameTowerNum(Tower tower)
    {
        return 0;
    }
}
