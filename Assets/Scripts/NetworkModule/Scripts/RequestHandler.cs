using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public partial class RequestHandler
{
    public void RequestGet(string uri, UnityAction<(int, object)> callback = null, FileSection section = FileSection.None, params KeyValuePair<string, object>[] data) 
        => _RequestGet(uri, data, section, callback);
    public void RequestGet(string uri, UnityAction<(int, object)> callback = null, FileSection section = FileSection.None, Dictionary<string, object> data = null) 
        => _RequestGet(uri, data?.ToArray(), section, callback);
    public void RequestPost(string uri, UnityAction<(int, object)> callback = null, params KeyValuePair<string, object>[] data) 
        => _RequestPost(uri, data, callback);
    public void RequestPost(string uri, UnityAction<(int, object)> callback = null, Dictionary<string, object> data = null) 
        => _RequestPost(uri, data?.ToArray(), callback);

    public int networkCheckTic { get; set; }
    public int networkTimeout { get; set; }
}

public partial class RequestHandler: MonoBehaviour
{
    private readonly Dictionary<int, (int, object)> _requestResult = new();
    private int _requestId;

    private void _RequestGet(string uri, KeyValuePair<string, object>[] data, FileSection section, UnityAction<(int, object)> callback)
    {
        int requestId = _requestId++;
        string buildUri = $"{uri}{new RequestBuilder().QueryBuilder(section, data)}";
        StartCoroutine(GetRequest(buildUri, section, requestId));
        WaitTaskAndRunCallback(requestId, callback);
    }
    
    private void _RequestPost(string uri, KeyValuePair<string, object>[] data, UnityAction<(int, object)> callback)
    {
        int requestId = _requestId++;
        StartCoroutine(PostRequest(uri, new RequestBuilder().MultipartFormBuilder(data), requestId));
        WaitTaskAndRunCallback(requestId, callback);
    }
    
    private IEnumerator GetRequest(string uri, FileSection section, int requestId)
    {
        using UnityWebRequest webRequest = new RequestBuilder().RequestGetType(section, uri);
        webRequest.timeout = networkTimeout;

        yield return webRequest.SendWebRequest();
        DisposeWebRequestResult(section, webRequest, requestId);
    }

    private IEnumerator PostRequest(string uri, List<IMultipartFormSection> form, int requestId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);
        webRequest.timeout = networkTimeout;
        yield return webRequest.SendWebRequest();
        DisposeWebRequestResult(FileSection.None, webRequest, requestId);
    }
    
    private async void WaitTaskAndRunCallback(int requestId, UnityAction<(int, object)> callback)
    {
        while (!_requestResult.ContainsKey(requestId))
        {
            await Task.Delay(networkCheckTic);
        }
        
        callback?.Invoke(_requestResult[requestId]);
        _requestResult.Remove(requestId);
    }

    private void DisposeWebRequestResult(FileSection section, UnityWebRequest webRequest, int requestId)
    {
        (int statusCode, object payload) item = new ()
        {
            statusCode = (int)webRequest.responseCode
        };

        if (item.statusCode < 300)
        {
            SetPayload(section, webRequest, out item.payload);
        }
        else
        {
            item.payload = webRequest.error;
        }
        
        _requestResult.Add(requestId, item);
    }

    private static void SetPayload(FileSection section, UnityWebRequest webRequest, out object payload)
    {
        switch (section)
        {
            case FileSection.Image:
                // sprite를 담아줄까? texture2D를 담아줄까?
                // payload = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                Texture2D texture2D = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                payload = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * .5f);
                break;
            default:
                payload = webRequest.downloadHandler.text;
                break;
        }
    }
}
