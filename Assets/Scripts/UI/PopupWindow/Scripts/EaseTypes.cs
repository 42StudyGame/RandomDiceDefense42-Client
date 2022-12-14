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

public partial class Ease // Common
{
    private float _duration;
    private float _beginTime;
    private EaseType _easeType;
    private UnityAction _onComplete;
    
    private Vector2 _nowValue()
    {
        return _easeType switch
        {
            EaseType.Linear => _linearProgress(),
            _ => _linearProgress()
        };
    }

    private void _setCommonData(float duration, EaseType easeType, UnityAction onComplete)
    {
        _duration = duration;
        _beginTime = Time.time;
        _easeType = easeType;
        _onComplete = onComplete;
    }
}

public partial class Ease // EaseType.Linear
{
    private Vector2 _vectorNormalizedOffset;
    private Vector2 _vectorBegin;
    private Vector2 _vectorEnd;

    public Ease(Vector2 begin, Vector2 end, float duration, EaseType easeType = EaseType.Linear, UnityAction onComplete = null)
    {
        _setCommonData(duration, easeType, onComplete);
        _setVectorData(begin, end, duration);
    }
    
    private void _setVectorData(Vector2 begin, Vector2 end, float duration)
    {
        _vectorNormalizedOffset = (end - begin) / duration;
        _vectorBegin = begin;
        _vectorEnd = end;
    }

    private void _setReverse()
    {
        _beginTime = Time.time;
        _vectorNormalizedOffset *= -1;
        (_vectorBegin, _vectorEnd) = (_vectorEnd, _vectorBegin);
    }
    
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

public partial class Ease // EaseType.something
{
}