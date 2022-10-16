using System.Linq;
using UnityEngine;

public enum BuffType
{
    Dot,
    Slow,
    Ground,
    Etc,
}

public partial class BuffPool // IO
{
    public BuffData RequestBuff(int id) => buffVariation.FirstOrDefault(e => e.id == id)?.Clone();
}

public partial class BuffPool : MonoBehaviour // SerializeField
{
    [SerializeField] private BuffData[] buffVariation;
}
