using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NetworkModuleTest : MonoBehaviour
{
    [SerializeField] private NetworkModule _networkModule;
    private const string Uri0 = "naver0.com"; // wrong
    private const string Uri1 = "google.com"; // correct
    private const string Post0 = "https://jsonplaceholder.typicode.com/posts";
    
    private IEnumerator Start()
    {
        yield return null;
        NetReqGetTest(Uri0);
        yield return null;
        NetReqGetTest(Uri1);
        yield return null;
        NetReqPostTest(Post0);
    }

    private void NetReqGetTest(string uri)
    {
        _networkModule?.RequestGet(uri, PrintResult);
    }

    private void NetReqPostTest(string uri)
    {
        if (_networkModule == null)
        {
            return;
        }
        
        KeyValuePair<string, string> pair = new("pair test key 0", "pair test value 0");
        
        Dictionary<string, string> data = new() { { "dic test key 0", "dic test value 0" } };

        _networkModule.RequestPost(uri, PrintResult, pair);
        _networkModule.RequestPost(uri, PrintResult, data);
        _networkModule.RequestPost(uri, PrintResult);
    }

    private static void PrintResult((bool result, string payload) request)
    {
        Debug.Log($"result = {request.result}");
        Debug.Log($"payload = {request.payload}");
    }
}
