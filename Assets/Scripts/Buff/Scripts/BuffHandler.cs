using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class BuffHandler // IO
{
    public void Append(int buffId) => _append(buffId);
    public (BuffType, float)[] EffectiveValue() => _effectiveValue();
}

public partial class BuffHandler // SerializeField
{
    [SerializeField] private BuffPool buffPool;
}

public partial class BuffHandler : MonoBehaviour
{
}

public partial class BuffHandler // body
{
    private readonly Dictionary<int, BuffData> _buffDictionary = new();
    
    private void _append(int buffId)
    {
        if (!RequestBuff(buffId, out BuffData buff))
        {
            return;
        }

        int stackCount = _buffDictionary.ContainsKey(buff.id) ? _buffDictionary[buff.id].stackCount : 0;  
        buff.stackCount = stackCount;
        buff.BeginTime = DateTime.Now;
        _buffDictionary[buff.id] = buff;
    }

    private (BuffType, float)[] _effectiveValue()
    {
        RemoveExpired();
        DateTime now = DateTime.Now;
        
        (BuffType, float)[] tupleArray = _buffDictionary
            .GroupBy(g => g.Value.buffType)
            .Select(g => 
                (g.Key, g.Sum(e => UpdateLastEffectiveTime(now, e.Value))))
            .ToArray();
        
        return tupleArray;
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
        
        foreach (KeyValuePair<int, BuffData> pair in _buffDictionary.ToArray())
        {
            TimeSpan timeSpan = now - pair.Value.BeginTime;
            if (pair.Value.duration < timeSpan.Seconds)
            {
                // Debug.Log($"key = {pair.Key}, Begin - now = {(now - pair.Value.BeginTime).Seconds}");
                //
                _buffDictionary.Remove(pair.Key);
            }
        }
    }
}
