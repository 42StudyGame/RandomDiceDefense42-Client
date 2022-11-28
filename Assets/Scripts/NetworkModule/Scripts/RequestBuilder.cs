using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

public partial class RequestBuilder // IO
{
    public string QueryBuilder(KeyValuePair<string, object>[] source)
    {
        if (source == null || !source.Any()) return null;
        BuildQueryData(source);
        
        return _queryBuilder.ToString();
    }

    public List<IMultipartFormSection> MultipartFormBuilder(KeyValuePair<string, object>[] source)
    {
        if (source == null || !source.Any()) return null;
        BuildMultipartFormDataList(source);
        BuildMultipartFormFileList(source);

        return _formDataList;
    }

    public UnityWebRequest RequestGetType(FileSection section, string uri)
    {
        switch (section)
        {
            case FileSection.Texture:
                return UnityWebRequestTexture.GetTexture(uri);
            case FileSection.Sprite:
                return UnityWebRequestTexture.GetTexture(uri);
            default:
                return UnityWebRequest.Get(uri);
        }
    }
}

public partial class RequestBuilder
{
    private readonly List<IMultipartFormSection> _formDataList = new();
    private readonly StringBuilder _queryBuilder = new(string.Empty);
    
    private void BuildMultipartFormDataList(IEnumerable<KeyValuePair<string, object>> source)
    {
        foreach (var pair in source)
        {
            if (pair.Value is not string) continue;
            if (0 < _queryBuilder.Length) _queryBuilder.Append("&");
            _queryBuilder.Append($"{pair.Key}={pair.Value}");
        }

        if (0 < _queryBuilder.Length)
        {
            _formDataList.Add(new MultipartFormDataSection(_queryBuilder.ToString()));
        }
    }
    
    private void BuildMultipartFormFileList(IEnumerable<KeyValuePair<string, object>> source)
    {
        foreach (KeyValuePair<string, object> pair in source)
        {
            if (pair.Value is not FileForm fileForm) continue;
            _formDataList.Add(new MultipartFormFileSection(
                fileForm.Section.ToString(),
                new FileHandler().FileToByteArray(fileForm.Section, fileForm.FileData), 
                fileForm.FileName,
                "multipart/form-data"));
        }
    }

    private void BuildQueryData(KeyValuePair<string, object>[] source)
    {
        StringBuilder query = new StringBuilder(string.Empty);
        
        foreach (KeyValuePair<string, object> pair in source)
        {
            if (0 < query.Length)
            {
                query.Append("&");
            }
            query.Append(pair.Key);
            query.Append("=");
            query.Append(pair.Value);
        }

        _queryBuilder.Append("?");
        _queryBuilder.Append(query);
    }
}

public partial class RequestBuilder
{
    private List<IMultipartFormSection> SetMultipartForm()
        => _formDataList.Any() ? _formDataList : null;

    private string SetQuery()
        => 0 < _queryBuilder.Length ? _queryBuilder.ToString() : null;
}
