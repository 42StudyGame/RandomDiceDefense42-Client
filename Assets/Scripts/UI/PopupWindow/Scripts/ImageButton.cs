using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ImageButton : IPointerClickHandler // IO
{
    public UnityAction onClick { private get; set; }
    public void Sprite(Sprite sprite) => _sprite(sprite);
    public void OnPointerClick(PointerEventData eventData) => _onPointerClick();
}

public partial class ImageButton // SerializeField
{
    [SerializeField] private Image image;
}

public partial class ImageButton : MonoBehaviour
{
}

public partial class ImageButton // body
{
    private void _onPointerClick()
    {
        onClick?.Invoke();
    }

    private void _sprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
