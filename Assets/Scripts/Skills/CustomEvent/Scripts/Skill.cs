using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : ScriptableObject
{
    public new string name;
    public int id;

    public float cooldownTime;
    public float activeTime;

    public UnityEvent skillEvent;

    public virtual void BeginCoolDown(GameObject parent) { }
    public virtual void Activate(GameObject parent) { }
}
