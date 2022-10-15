using System.Linq;
using UnityEngine;

public partial class BuffPool // IO
{
    public BuffData RequestBuff(int id) => buffVariation.FirstOrDefault(e => e.id == id)?.Clone();
}

public partial class BuffPool // SerializeField
{
    [SerializeField] private BuffData[] buffVariation;
}

public partial class BuffPool : MonoBehaviour
{
}
