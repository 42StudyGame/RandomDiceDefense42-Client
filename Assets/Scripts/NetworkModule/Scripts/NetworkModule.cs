using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FileForm 
{
    public readonly FileSection Section;
    public readonly string FileName;
    public readonly object FileData;

    public FileForm(FileSection section, string fileName, object fileData)
    {
        Section = section;
        FileName = fileName;
        FileData = fileData;
    }
}

public class WebResponse
{
    public readonly int StatusCode;
    public object Payload;

    public WebResponse(int statusCode)
    {
        StatusCode = statusCode;
    }
}

public enum FileSection
{
    None = 0,
    Texture,
}

public partial class NetworkModule // IO
{
    public void RequestGet(string uri, UnityAction<WebResponse> callback = null, FileSection section = FileSection.None, params KeyValuePair<string, object>[] data)
        => requestHandler.RequestGet(uri, callback, section, data);
    public void RequestGet(string uri, UnityAction<WebResponse> callback = null, FileSection section = FileSection.None, Dictionary<string, object> data = null)
        => requestHandler.RequestGet(uri, callback, section, data);
    public void RequestPost(string uri, UnityAction<WebResponse> callback = null, params KeyValuePair<string, object>[] data) 
        => requestHandler.RequestPost(uri, callback, data);
    public void RequestPost(string uri, UnityAction<WebResponse> callback = null, Dictionary<string, object> data = null) 
        => requestHandler.RequestPost(uri, callback, data);
}

public partial class NetworkModule // setting values
{
    public int networkCheckTic = 10; // almost 24 fps (little over)
    public int networkTimeout = 5000;
}

public partial class NetworkModule: MonoBehaviour // SerializeField
{
    [SerializeField] private RequestHandler requestHandler;
}

public partial class NetworkModule
{
    private void Awake()
    {
        InitRequestHandler();
    }
}

public partial class NetworkModule
{
    private void InitRequestHandler()
    {
        if (!requestHandler) return;
        requestHandler.networkCheckTic = networkCheckTic;
        requestHandler.networkTimeout = networkTimeout;
    }
}