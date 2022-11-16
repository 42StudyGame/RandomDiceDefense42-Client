using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public partial class NetworkModule // IO
{
    public void RequestGet(string uri, UnityAction<(bool, string)> callback = null) => _RequestGet(uri, callback);
    public void RequestPost(string uri, UnityAction<(bool, string)> callback = null, params KeyValuePair<string, string>[] data) => _RequestPost(uri, data, callback);
    public void RequestPost(string uri, UnityAction<(bool, string)> callback = null, Dictionary<string, string> data = null) => _RequestPost(uri, data, callback);
}

public partial class NetworkModule : MonoBehaviour
{
    private readonly Dictionary<int, (bool, string)> _requestResult = new();
    private int _requestId;
    private const int Tic = 40; // almost 24 fps (little over)
    private const int TimeOut = 3000;

    private void _RequestGet(string uri, UnityAction<(bool, string)> callback)
    {
        int requestId = _requestId++;
        StartCoroutine(GetRequest(uri, requestId));
        WaitTaskAndRunCallback(requestId, callback);
    }
    
    private void _RequestPost(string uri, IEnumerable<KeyValuePair<string, string>> data, UnityAction<(bool, string)> callback)
    {
        int requestId = _requestId++;
        StartCoroutine(PostRequest(uri, data, requestId));
        WaitTaskAndRunCallback(requestId, callback);
    }

    private async void WaitTaskAndRunCallback(int requestId, UnityAction<(bool, string)> callback)
    {
        while (!_requestResult.ContainsKey(requestId))
        {
            await Task.Delay(Tic);
        }
        
        callback?.Invoke(_requestResult[requestId]);
        _requestResult.Remove(requestId);
    }

    private IEnumerator GetRequest(string uri, int requestId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        webRequest.timeout = TimeOut;
        yield return webRequest.SendWebRequest();
        DisposeWebRequestResult(webRequest, requestId);
    }

    private IEnumerator PostRequest(string uri, IEnumerable<KeyValuePair<string, string>> data, int requestId)
    {
        WWWForm form = ConvertToWWWForm(data);
        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        DisposeWebRequestResult(webRequest, requestId);
    }

    private void DisposeWebRequestResult(UnityWebRequest webRequest, int requestId)
    {
        (bool success, string payload) item = new ()
        {
            success = webRequest.result is UnityWebRequest.Result.Success
        };

        item.payload = item.success ? webRequest.downloadHandler.text : webRequest.error;
        _requestResult.Add(requestId, item);
    }

    private static WWWForm ConvertToWWWForm(IEnumerable<KeyValuePair<string, string>> source)
    {
        WWWForm form = new WWWForm();

        if (source == null) return null;
        
        foreach (KeyValuePair<string, string> element in source)
        {
            form.AddField(element.Key, element.Value);
        }
        
        return form;
    }
}

