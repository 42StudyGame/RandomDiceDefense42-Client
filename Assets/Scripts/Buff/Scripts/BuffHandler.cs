using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BuffHandler // IO
{
    public void Append(int buffId, Vector3 position = default) => _append(buffId, position);
    public (BuffType, float)[] GetEffectiveValueArray() => _getEffectiveValueArray();
}

public partial class BuffHandler // SerializeField
{
    [SerializeField] private BuffPool buffPool;
    [SerializeField] private Buff prefab;
}

public partial class BuffHandler : MonoBehaviour // body
{
    private readonly Dictionary<int, Buff> _buffDictionary = new();
    
    private void _append(int buffId, Vector3 position)
    {
        if (!RequestBuff(buffId, out BuffData buff))
        {
            return;
        }

        SetupBuff(buff, position);
    }

    private (BuffType, float)[] _getEffectiveValueArray()
    {
        RemoveExpired();
        DateTime now = DateTime.Now;
        (BuffType, float)[] tupleArray = _buffDictionary
            .GroupBy(g => g.Value.GetData().buffType)
            .Select(g => 
                (g.Key, g.Sum(e => UpdateLastEffectiveTime(now, e.Value.GetData()))))
            .ToArray();
        
        return tupleArray;
    }

    private void SetupBuff(BuffData buff, Vector3 position)
    {
        Buff buffObject;
        if (_buffDictionary.ContainsKey(buff.id))
        {
            buffObject = _buffDictionary[buff.id];
            buff.stackCount = buffObject.GetData().stackCount + 1;
        }
        else
        {
            buffObject = Instantiate(prefab, transform);
        }
        
        buff.BeginTime = DateTime.Now;
        buffObject.InjectData(buff, position);
        _buffDictionary[buff.id] = buffObject;
    }
    
    private float UpdateLastEffectiveTime(DateTime now, BuffData data)
    {
        TimeSpan timeSpan = now - data.LastEffectiveTime;

        if (!(data.interval <= timeSpan.Seconds))
        {
            return 0;
        }
        
        data.LastEffectiveTime = now;
        return data.effectiveValue;

    }
    
    private bool RequestBuff(int buffId, out BuffData buffData)
    {
        buffData = buffPool.RequestBuff(buffId);
        return buffData != null;
    }
    
    private void RemoveExpired()
    {
        DateTime now = DateTime.Now;
        
        foreach (KeyValuePair<int, Buff> pair in _buffDictionary.ToArray())
        {
            TimeSpan timeSpan = now - pair.Value.GetData().BeginTime;
            if (pair.Value.GetData().duration < timeSpan.Seconds)
            {
                Destroy(_buffDictionary[pair.Key].gameObject);
                _buffDictionary.Remove(pair.Key);
            }
        }
    }
}
