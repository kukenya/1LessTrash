using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;


[System.Serializable]
public class UserData
{
    public string login_id;
    public int score;
}

[System.Serializable]
public class GameData
{
    public string name;
    public string category;
    public int count;
    public int clear;
}

[System.Serializable]
public class JsonData
{
    public UserData[] users;
    public GameData[] games;
}


public class RankingSys : MonoBehaviour
{

    private string serverURL = "http://192.168.1.20:8888/game/result";

    // Start is called before the first frame update
    void Start()
    {
        UserData user1 = new UserData { login_id = "user123", score = 6 };
        GameData game1 = new GameData { name = "brokendish", category = "normal", count = 3, clear = 3 };
        GameData game2 = new GameData { name = "box", category = "paper", count = 2, clear = 0 };

        // JsonData 积己 棺 单捞磐 且寸
        JsonData jsonData = new JsonData
        {
            users = new UserData[] { user1 },
            games = new GameData[] { game1, game2 }
        };

        string json = JsonUtility.ToJson(jsonData);

        // HTTP 夸没 积己
        StartCoroutine(SendJsonToServer(json));
    }

    private IEnumerator SendJsonToServer(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(serverURL, json))
        {
            request.method = "POST";
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending JSON to server: " + request.error);
            }
            else
            {
                Debug.Log("JSON sent successfully to server.");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}


