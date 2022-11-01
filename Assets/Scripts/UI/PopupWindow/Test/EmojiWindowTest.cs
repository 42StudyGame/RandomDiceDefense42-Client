using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiWindowTest : MonoBehaviour
{
    [SerializeField] private EmojiSelectWindow emojiSelectWindow;
    
    private void Start()
    {
        emojiSelectWindow.AddCallback(0, () => { Debug.Log("I am first");});
        emojiSelectWindow.AddCallback(0, () => { Debug.Log("This is added");});
        emojiSelectWindow.AddCallback(1, () => { Debug.Log("I am second");});
        emojiSelectWindow.AddCallback(2, () => { Debug.LogError("I am third");});
        emojiSelectWindow.SetCallback(2, () => { Debug.Log("Im not introduce, if U can see like 'I am third', something wrong!\nbecause I am SetCallback! haha!");});
        emojiSelectWindow.AddCallback(3, () => { Debug.Log("I am out of range");});
    }
}
