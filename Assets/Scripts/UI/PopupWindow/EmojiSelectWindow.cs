using UnityEngine;

public partial class EmojiSelectWindow // SerializeField
{
    [SerializeField] private ImageButton emojiButtonPrefab;
    [SerializeField] private Sprite[] spriteArray;
}

public partial class EmojiSelectWindow : MonoBehaviour
{
    private void Awake()
    {
        PutData();
    }
}

public partial class EmojiSelectWindow // body
{
    private void PutData()
    {
        foreach (Sprite sprite in spriteArray)
        {
            ImageButton imageButton = Instantiate(emojiButtonPrefab, transform);
            imageButton.Sprite(sprite);

            Jumper jumper = imageButton.GetComponent<Jumper>();
            imageButton.onClick = () =>
            {
                jumper.Jump(Vector3.up * 10, 1);
            };
        }
    }
}
