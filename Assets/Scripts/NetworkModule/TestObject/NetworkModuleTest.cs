using System;
using System.Collections;
using UnityEngine;

public class NetworkModuleTest : MonoBehaviour
{
    [SerializeField] private NetworkModule networkModule;
    private const string uri0 = "naver0.com"; // wrong
    private const string uri1 = "google.com"; // correct
    
    private IEnumerator Start()
    {
        yield return null;
        NetReqTest(uri0);
        yield return null;
        NetReqTest(uri1);
    }

    private void NetReqTest(string uri)
    {
        if (!networkModule)
        {
            return;
        }
        
        networkModule.Request(uri, PrintResult);
    }

    private static void PrintResult((bool result, string payload) request)
    {
        Debug.Log($"result = {request.result}");
        Debug.Log($"payload = {request.payload}");
    }
}
