using UnityEngine;

public class FileHandler
{
    public byte[] FileToByteArray(FileSection section, object file)
    {
        switch (section)
        {
            case FileSection.Texture:
                Texture2D source = (Texture2D)file;
                Texture2D destination = new Texture2D(source.width, source.height);
                Color[] pixels = source.GetPixels(0, 0, source.width, source.height);
                destination.SetPixels(pixels);
                destination.Apply();
                return destination.EncodeToPNG();
            default:
                return null;
        }
    }

    public Sprite Texture2DToSprite(Texture2D texture2D)
        => Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * .5f);
}
