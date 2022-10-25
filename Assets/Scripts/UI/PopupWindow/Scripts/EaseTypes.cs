using UnityEngine;
using UnityEngine.Events;

public enum EaseType
{
    Linear
}

public partial class Ease // IO
{
    public Vector2 NowValue() => _nowValue();
    public void SetReverse() => _setReverse();
}

public partial class Ease // body
{
    private float _duration;
    private float _beginTime;
    private EaseType _easeType;
    private UnityAction _onComplete;

    private Vector2 _vectorNormalizedOffset;
    private Vector2 _vectorBegin;
    private Vector2 _vectorEnd;

    public Ease(Vector2 begin, Vector2 end, float duration, EaseType easeType = EaseType.Linear, UnityAction onComplete = null)
    {
        _setData(begin, end, duration, easeType, onComplete);
    }
    
    private void _setData(Vector2 begin, Vector2 end, float duration, EaseType easeType, UnityAction onComplete)
    {
        _duration = duration;
        _beginTime = Time.time;
        _easeType = easeType;
        _onComplete = onComplete;
        
        _vectorNormalizedOffset = (end - begin) / duration;
        _vectorBegin = begin;
        _vectorEnd = end;
    }

    private Vector2 _nowValue()
    {
        return _easeType switch
        {
            EaseType.Linear => _linearProgress(),
            _ => _linearProgress()
        };
    }

    private void _setReverse()
    {
        _beginTime = Time.time;
        _vectorNormalizedOffset *= -1;
        (_vectorBegin, _vectorEnd) = (_vectorEnd, _vectorBegin);
    }
}

public partial class Ease // variation
{
    private Vector2 _linearProgress()
    {
        float timeSpan = Mathf.Min(Time.time - _beginTime, _duration);

        if (_onComplete != null && _duration.Equals(timeSpan))
        {
            _onComplete.Invoke();
        }

        return _vectorBegin + timeSpan * _vectorNormalizedOffset;
    }
}