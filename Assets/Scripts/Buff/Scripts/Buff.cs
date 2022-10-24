using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public partial class Buff // IO
{
    public void InjectData(BuffData buffData, UnityAction<BuffData> beginAction, UnityAction<BuffData> endAction, UnityAction<int> finallyAction) 
        => _injectData(buffData, beginAction, endAction, finallyAction);
    public BuffData BuffData() => _buffData;
    public void RemoveForce() => Destroy(gameObject);
}

public partial class Buff // SerializeField
{
    [SerializeField] private SpriteRenderer spriteRenderer;
}

public partial class Buff : MonoBehaviour
{
    private void OnDestroy()
    {
        _finallyAction?.Invoke(GetInstanceID());
    }
}

public partial class Buff // body
{
    private BuffData _buffData;
    private UnityAction<BuffData> _endAction;
    private UnityAction<BuffData> _beginAction;
    private UnityAction<int> _finallyAction;

    private void _injectData(BuffData buffData, UnityAction<BuffData> beginAction, UnityAction<BuffData> endAction, UnityAction<int> finallyAction)
    {
        SetData(buffData, beginAction, endAction, finallyAction);
        StartCoroutine(BuffLifeCycle());
    }

    private void SetData(BuffData buffData, UnityAction<BuffData> beginAction, UnityAction<BuffData> endAction, UnityAction<int> finallyAction)
    {
        _beginAction = beginAction;
        _endAction = endAction;
        _finallyAction = finallyAction;
        _buffData = buffData;
        sprite = null;
    }

    private Sprite sprite
    {
        set
        {
            if (0 < _buffData.decorateSprite.Length)
            {
                int spriteIndex = Mathf.Min(_buffData.stackCount, _buffData.decorateSprite.Length);
                spriteRenderer.sprite = _buffData.decorateSprite[spriteIndex];
            }
            else
            {
                spriteRenderer.sprite = value;
            }
        }
    }
    
    private IEnumerator BuffLifeCycle()
    {
        _beginAction?.Invoke(_buffData);
        yield return new WaitForSecondsRealtime(_buffData.interval);
        
        while (0 < _buffData.repeatTimes)
        {
            _buffData.repeatTimes -= 1;
            _beginAction?.Invoke(_buffData);
            yield return new WaitForSecondsRealtime(_buffData.interval);
        }
        
        _endAction?.Invoke(_buffData);
        Destroy(gameObject);
    }
}
