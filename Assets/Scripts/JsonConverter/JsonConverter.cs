using System.IO;
using System.Text;
using UnityEngine;

// referenced blog : https://wergia.tistory.com/164
// This class is for converting JSON files to any types
// To use it, add this class reference on other classes
public class JsonConverter
{
    // convert string to JSON format
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    // convert JSON format to string
    public T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }


    // save JSON files written on string
    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // load JSON files to generic types
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
