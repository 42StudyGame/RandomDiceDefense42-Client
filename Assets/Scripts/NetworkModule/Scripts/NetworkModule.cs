using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public partial class NetworkModule // IO
{
    public void Request(string uri, UnityAction<(bool, string)> callback = null) => _Request(uri, callback);
}

public partial class NetworkModule : MonoBehaviour
{
    private readonly Dictionary<int, (bool, string)> _requestResult = new();
    private int _requestId = 0;
    private const int Tic = 40; // almost 24 fps (little over)
    private const int TimeOut = 3000;

    private void _Request(string uri, UnityAction<(bool, string)> callback)
    {
        int requestId = _requestId++;
        StartCoroutine(GetRequest(uri, requestId));
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

    private void DisposeWebRequestResult(UnityWebRequest webRequest, int requestId)
    {
        (bool success, string payload) item = new ()
        {
            success = webRequest.result is UnityWebRequest.Result.Success
        };

        item.payload = item.success ? webRequest.downloadHandler.text : webRequest.error;
        _requestResult.Add(requestId, item);
    }
}

