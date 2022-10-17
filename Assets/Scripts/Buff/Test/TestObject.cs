using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    [SerializeField] private BuffHandler buffHandler;
    
    private void Start()
    {
        buffHandler.Append(0);
        // buffHandler.Append(1);
        // buffHandler.Append(2);
        buffHandler.Append(3);
        buffHandler.Append(4);
    }

    private void Update()
    {
        (BuffType BuffType, float effectValue)[] array = buffHandler.GetEffectiveValueArray();

        if (array.Length <= 0 || !(array[0].effectValue > 0))
        {
            return;
        }
        
        foreach (var item in array)
        {
            Debug.Log($"BuffType = {item.BuffType}, EffectValue = {item.effectValue}");
        }
        Debug.Log("-----------------------------------------");
    }
}
