using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public partial class NetworkModule // IO
{
    public string Request(string uri) => _request(uri);
}

public partial class NetworkModule : MonoBehaviour
{
    private readonly Dictionary<int, (bool, string)> _requestResult = new ();
    private int _requestId = 0;

    private string _request(string uri)
    {
        int ticketId = _requestId++;
        StartCoroutine(GetRequest(uri, ticketId));
        
        // wait task here
        
        return null;
    }

    private IEnumerator GetRequest(string uri, int requestId)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();
        DisposeWebRequestResult(webRequest, requestId);
    }

    private void DisposeWebRequestResult(UnityWebRequest webRequest, int requestId)
    {
        (bool success, string payload) item = new ()
        {
            success = webRequest.result is UnityWebRequest.Result.Success or UnityWebRequest.Result.InProgress,
        };

        item.payload = item.success ? webRequest.downloadHandler.text : webRequest.error; 
        _requestResult.Add(requestId, item);
    }
}

