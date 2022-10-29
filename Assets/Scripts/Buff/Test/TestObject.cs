using System.Collections;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] private BuffHandler buffHandler;
    
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(2);
        buffHandler.Attach(0, BeginAction, EndAction);

        yield return new WaitForSecondsRealtime(2);
        buffHandler.Attach(0, BeginAction, EndAction);
        
        yield return new WaitForSecondsRealtime(2);
        buffHandler.Attach(0, BeginAction, EndAction);
    }

    private void BeginAction(BuffData data)
    {
        Debug.LogWarning($"{data.id} effected, current stack = {data.stackCount}, repeatable count = {data.repeatTimes}");
    }
    
    private void EndAction(BuffData data)
    {
        Debug.LogWarning($"{data.id} removed");
    }
}
