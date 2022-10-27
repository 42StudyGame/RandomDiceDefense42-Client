using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyLineManager // IO
{
    public float maxDistToGoal { get; private set; }
    
    public void Init() => _Init();
    
    public Vector2 PinToVec(float pin) => _PinToVec(pin);
    public float VecToPin(Vector2 vec) => _VecToPin(vec);
}

public partial class EnemyLineManager // SerializedField
{
    public Transform[] wayPoints;
}

public partial class EnemyLineManager : MonoBehaviour
{
    
}

public partial class EnemyLineManager // Body
{
    private List<float> _pathLen = new List<float>();
    private List<float> _normPathLen = new List<float>();
    private float _recFinalLen = 0f;

    private void _Init()
    {
        maxDistToGoal = 0f;
        // calculate lenght per path and total length
        for (int i = 1; i < wayPoints.Length; i++)
        {
            float dist = Vector2.Distance(wayPoints[i - 1].position, wayPoints[i].position);
            _pathLen.Add(dist);
            maxDistToGoal += dist;
        }

        _recFinalLen = 1 / maxDistToGoal;

        for (int i = 0; i < _pathLen.Count; i++)
        {
            _normPathLen.Add(_pathLen[i] * _recFinalLen);
        }
    }
    
    private Vector2 _PinToVec(float pin)
    {
        if (pin > 1f || pin < 0f)
        {
            Debug.LogError("PinToVec Input error");
            return Vector2.zero;
        }

        Vector2 res;
        float sum = 0f;
        int i = 0;

        while (pin > sum)
        {
            sum += _normPathLen[i];
            i++;
        }
        sum -= _normPathLen[--i];
        
        float offset = (pin - sum) / _normPathLen[i];
        res = Vector2.Lerp(wayPoints[i].position, wayPoints[i + 1].position, offset);
        return res;
    }

    private float _VecToPin(Vector2 vec)
    {
        int valid = -1;
        for (int i = 1; i < wayPoints.Length; i++)
        {
            if (_IsCBetweenAB(wayPoints[i - 1].position, wayPoints[i].position, vec))
            {
                valid = i;
                break;
            }
        }

        if (valid == -1)
        {
            Debug.LogError("vector not on enemyline");
            return (-1f);
        }

        valid -= 1;
        float res = 0f;
        for (int j = 0; j < valid; j++)
        {
            res += _normPathLen[j];
        }
        res += _InverseLerp(wayPoints[valid].position, wayPoints[valid + 1].position, vec)
               * _normPathLen[valid];
        return (res);
    }

    private bool _IsCBetweenAB(Vector2 a, Vector2 b, Vector2 c)
    {
        if (Vector2.Distance(a, c) + Vector2.Distance(b, c) - Vector2.Distance(a, b) < 0.1f)
        {
            return true;
        }
        return false;
    }

    private float _InverseLerp(Vector2 a, Vector2 b, Vector2 value)
    {
        Vector2 AB = b - a;
        Vector2 AV = value - a;
        return Vector2.Dot(AV, AB) / Vector2.Dot(AB, AB);
    }
}