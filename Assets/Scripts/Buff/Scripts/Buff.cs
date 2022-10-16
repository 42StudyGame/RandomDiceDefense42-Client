using UnityEngine;

public partial class Buff // IO
{
    public void InjectData(BuffData buffData, Vector3 position) => _injectData(buffData, position);
    public BuffData GetData() => _buffData;
}

public partial class Buff // SerializeField
{
    [SerializeField] private SpriteRenderer spriteRenderer;
}

public partial class Buff : MonoBehaviour
{
    // mine 타입 버프는 rail에 설치하고 collider로 확인하면 됨
    // 아래는 1회성 데미지 주는 경우
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 접촉한 Enemy에게 데미지 전달 함수 있는지 확인하고, 그것 호출하면 됨
    }

    // mine 타입 버프는 rail에 설치하고 collider로 확인하면 됨
    // 접촉중인 내내 데미지 주는 경우는 아래것으로
    private void OnTriggerStay2D(Collider2D other)
    {
        // 접속한 Enemy에게 데미지 전달하고, interval도 체크하고 등등... 
    }
}

public partial class Buff // body
{
    private BuffData _buffData;

    private void _injectData(BuffData buffData, Vector3 position)
    {
        _buffData = buffData;
        transform.position = position;
        SetSprite(buffData);
    }

    private void SetSprite(BuffData buffData)
    {
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
