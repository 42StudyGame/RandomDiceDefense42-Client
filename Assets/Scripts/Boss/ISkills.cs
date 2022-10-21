using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISkills : MonoBehaviour
{
    [SerializeField] protected TowerManager towerManager;
    public abstract void Skill();
}
