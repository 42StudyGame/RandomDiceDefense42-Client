using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal struct RequestUrl
{
    public string requestType;
    public string requestUrl;
}


public class NetworkModuleTest : MonoBehaviour
{
    [SerializeField] private NetworkModule networkModule;
    [SerializeField] private Sprite sprite;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Canvas canvas;
    private const string Uri0 = "naver0.com"; // wrong
    private const string Uri1 = "google.com"; // correct
    private const string Post0 = "https://jsonplaceholder.typicode.com/posts";
    private const string PostUpload = "http://localhost:3000/api/uploadimage";
    
    private IEnumerator Start()
    {
        yield return null;
        NetReqGetTest(Uri0);
        yield return null;
        NetReqGetTest(Uri1);
        yield return null;
        NetReqGetQueryTest(Post0);
        yield return null;
        NetReqPostTest(Post0);
        yield return null;
        NetReqPostUpload(PostUpload);
    }

    private void NetReqGetTest(string uri)
    {
        networkModule?.RequestGet(uri, PrintResult);
    }

    private void NetReqGetQueryTest(string uri)
    {
        KeyValuePair<string, object> pair = new("userId", "1");
        networkModule?.RequestGet(uri, PrintResult, FileSection.None, pair);
        
        Dictionary<string, object> data = new() { { "userId", "1" }, {"id", "1"} };
        networkModule?.RequestGet(uri, PrintResult, FileSection.None, data);
    }

    
    private void NetReqPostTest(string uri)
    {
        if (networkModule == null)
        {
            return;
        }
        
        KeyValuePair<string, object> pair = new("pair test key 0", "pair test value 0");
        Dictionary<string, object> data = new() { { "dic test key 0", "dic test value 0" } };
    }

    private void NetReqPostUpload(string uri)
    {
        if (networkModule == null)
        {
            return;
        }
        
        KeyValuePair<string, object> imgPair = new("image.png", new FileForm(FileSection.Texture, "image.png", sprite.texture));
        networkModule.RequestPost(uri, PrintResultAndDownload, imgPair);
    }

    private void PrintResultAndDownload(WebResponse response)
    {
        Debug.Log($"statusCode = {response.StatusCode}");
        Debug.Log($"payload = {response.Payload}");

        var req = JsonUtility.FromJson<RequestUrl>(response.Payload.ToString());
        var reqType = req.requestType;
        var reqUrl = req.requestUrl;

        // networkModule.RequestGet(reqUrl, DrawImage, FileSection.Sprite);
        networkModule.RequestGet(reqUrl, DrawImage, FileSection.Sprite);
    }

    private void DrawImage(WebResponse response)
    {
        Debug.Log($"request.statusCode in DrawImage: {response.StatusCode}");

        if (response.StatusCode >= 300)
        {
            Debug.LogError($"request.statusCode in DrawImage: {response.StatusCode}");
            Debug.LogError($"request.payload in DrawImage: {response.Payload}");
            return;
        }
        
        // Texture2D texture2D = (Texture2D)request.payload;
        // Sprite toSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * .5f);
        // image.sprite = toSprite;
        image.sprite = (Sprite)response.Payload;
    }
    
    private void PrintResult(WebResponse response)
    {
        Debug.Log($"statusCode = {response.StatusCode}");
        Debug.Log($"payload = {response.Payload}");
    }
}
