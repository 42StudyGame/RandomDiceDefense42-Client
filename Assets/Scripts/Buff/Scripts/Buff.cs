using UnityEngine;

public partial class Buff // IO
{
    public void InjectData(BuffData buffData) => _injectData(buffData);
    public BuffData GetData() => _buffData;
}

public partial class Buff // SerializeField
{
    [SerializeField] private SpriteRenderer spriteRenderer;
}

public partial class Buff : MonoBehaviour // body
{
    private BuffData _buffData;

    private void _injectData(BuffData buffData)
    {
        _buffData = buffData;
        
        if (buffData.decorateSprite.Length == 0)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            int spriteIndex = Mathf.Min(buffData.stackCount, buffData.decorateSprite.Length);
            spriteRenderer.sprite = buffData.decorateSprite[spriteIndex];
        }
    }
}
