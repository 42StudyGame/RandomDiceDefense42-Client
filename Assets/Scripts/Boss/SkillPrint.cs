using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillPrint : ASkills
{
    [SerializeField] protected TowerManager towerManager;
    public override void Skill()
    {
        Debug.Log("스킬사용~");
    }
}
