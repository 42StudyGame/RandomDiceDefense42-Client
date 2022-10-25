using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public partial class Jumper // IO
{
    public void Jump(Vector2 offset, float duration) => _jump(offset, duration);
    public void AddOnComplete(UnityAction onComplete) => _addOnComplete(onComplete);
    public void SetOnComplete(UnityAction onComplete) => _setOnComplete(onComplete);
}

public partial class Jumper : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;
        _beginPosition = transform.localPosition;
    }
}

public partial class Jumper // body
{
    private Vector2 _beginPosition;
    private Coroutine _coroutine;
    private Ease _ease;
    private bool _done;
    private UnityAction _onComplete;

    private void _jump(Vector2 offset, float duration, EaseType easeType = EaseType.Linear, UnityAction onComplete = null)
    {
        DisposeCurrentRunning();
        SetEase(_beginPosition, _beginPosition + offset, duration * .5f, easeType, onComplete);
        
        _coroutine = StartCoroutine(Jumping());
    }

    private void _addOnComplete(UnityAction onComplete)
    {
        _onComplete += onComplete;
    }

    private void _setOnComplete(UnityAction onComplete)
    {
        _onComplete = onComplete;
    }

    private void SetEase(Vector2 begin, Vector2 end, float duration, EaseType easeType, UnityAction onComplete)
    {
        _ease = new Ease(begin, end, duration / 2, easeType, onComplete + FinishJob);
        _done = false;
    }

    private void FinishJob()
    {
        _done = true;
    }
    
    private void DisposeCurrentRunning()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            SetLocalPosition(_beginPosition);
        }

        _coroutine = null;
    }
    
    private void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
    
    private IEnumerator Jumping()
    {
        while (!_done)
        {
            transform.localPosition = _ease.NowValue();
            yield return null;
        }

        _done = false;
        _ease.SetReverse();
        
        while (!_done)
        {
            transform.localPosition = _ease.NowValue();
            yield return null;
        }

        SetLocalPosition(_beginPosition);
        _coroutine = null;
        _onComplete?.Invoke();
    }
}