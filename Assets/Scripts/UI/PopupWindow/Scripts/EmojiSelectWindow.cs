using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class EmojiSelectWindow // IO
{
    public void AddCallback(int index, UnityAction callback) => _addCallback(index, callback);
    public void SetCallback(int index, UnityAction callback) => _setCallback(index, callback);
}

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
    private readonly Dictionary<int, ImageButton> _dictionary = new();

    private void PutData()
    {
        int index = 0;
        foreach (Sprite sprite in spriteArray)
        {
            ImageButton imageButton = Instantiate(emojiButtonPrefab, transform);
            imageButton.Sprite(sprite);

            Jumper jumper = imageButton.GetComponent<Jumper>();
            imageButton.onClick = () =>
            {
                jumper.Jump(Vector3.up * 10, 1);
            };
            
            _dictionary.Add(index++, imageButton);
        }
    }

    private void _addCallback(int key, UnityAction callback)
    {
        try
        {
            _dictionary[key].GetComponent<Jumper>().AddOnComplete(callback);
        }
        catch
        {
            Debug.LogError($"can not add callback passed value is [{key}]");
        }
    }

    private void _setCallback(int key, UnityAction callback)
    {
        try
        {
            _dictionary[key].GetComponent<Jumper>().SetOnComplete(callback);
        }
        catch
        {
            Debug.LogError($"can not add callback passed value is [{key}]");
        }
    }
}
