using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public partial class BuffHandler // IO
{
    public void Attach(int buffId, UnityAction<BuffData> beginAction = null, UnityAction<BuffData> endAction = null) => _attach(buffId, beginAction, endAction);
}

public partial class BuffHandler // SerializeField
{
    [SerializeField] private BuffPool buffPool;
    [SerializeField] private Buff prefab;
}

public partial class BuffHandler : MonoBehaviour
{
}

public partial class BuffHandler // body
{
    private readonly Dictionary<int, Buff> _buffDictionary = new();

    private void _attach(int buffId, UnityAction<BuffData> beginAction, UnityAction<BuffData> endAction)
    {
        if (RequestBuff(buffId, out BuffData buffData))
        {
            SetupBuff(buffData, beginAction, endAction, RemoveBuff);
        }
    }

    private void RemoveBuff(int objectId)
    {
        if (!_buffDictionary.ContainsKey(objectId))
        {
            return;
        }

        if (_buffDictionary[objectId] != null)
        {
            Destroy(_buffDictionary[objectId]);
        }
        
        _buffDictionary.Remove(objectId);
    }

    private void SetupBuff(BuffData buffData, UnityAction<BuffData> beginAction, UnityAction<BuffData> endAction, UnityAction<int> finallyAction)
    {
        Buff buffObject = Instantiate(prefab, transform);
        int buffObjectId = buffObject.GetInstanceID();
        
        buffObject.InjectData(buffData, beginAction, endAction, finallyAction);
        _buffDictionary.Add(buffObjectId, buffObject);
    }

    private Buff Current(int buffId)
    {
        return _buffDictionary.FirstOrDefault(e => e.Value.BuffData().id == buffId).Value;
    }
    
    private bool RequestBuff(int buffId, out BuffData buffData)
    {
        buffData = buffPool.RequestBuff(buffId);
        Buff current = Current(buffId);

        if (current == null)
        {
            return buffData != null;
        }
        
        buffData.stackCount = current.BuffData().stackCount + 1;
        current.RemoveForce();
        return buffData != null;
    }
}
