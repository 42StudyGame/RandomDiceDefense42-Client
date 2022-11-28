using UnityEngine;

public class FileHandler
{
    public byte[] FileToByteArray(FileSection section, object file)
    {
        switch (section)
        {
            case FileSection.Image:
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
}
