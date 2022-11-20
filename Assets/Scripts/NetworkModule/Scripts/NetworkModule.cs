using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class NetworkModule // IO
{
    public void RequestGet(string uri, UnityAction<(bool, string)> callback = null) 
        => _requestHandler.RequestGet(uri, callback);
    public void RequestPost(string uri, UnityAction<(bool, string)> callback = null, params KeyValuePair<string, string>[] data) 
        => _requestHandler.RequestPost(uri, callback, data);
    public void RequestPost(string uri, UnityAction<(bool, string)> callback = null, Dictionary<string, string> data = null) 
        => _requestHandler.RequestPost(uri, callback, data);
}

public partial class NetworkModule: MonoBehaviour
{
    [SerializeField] private RequestHandler _requestHandler;
}
