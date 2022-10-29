using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BuffData", fileName = "Buff Data")]
public class BuffData : ScriptableObject
{
    public int id;
    public int stackCount;
    public float interval;
    public int repeatTimes;
    public float effectiveValue;
    public Sprite[] decorateSprite;
    public BuffType buffType;

    public BuffData Clone() => MemberwiseClone() as BuffData;
}
