using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;


[Serializable]
public class JsonList<T>
{
    public List<T> results;
}

public class UserInfo
{
    public string login_id;

}

[Serializable]
public class ResultGame
{
    public int status;
    public string message;
}

[Serializable]
public class GameInfo
{
    public string name;
    public string status;
}

[System.Serializable]
public class RootData
{
    //public LoginData loginData;
    public ResultGame resultGame;
    public GameInfo[] gameInfo;
}


[System.Serializable]
public class ItemData
{
    public string name;
    public string category;
    public int count;
    public int clear;
}




public class LoginManager : MonoBehaviour
{
    public static LoginManager instance;

    public UserInfo myInfo = new UserInfo();


    public List<ItemData> listItemData = new List<ItemData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        


        string[] itemName = { "bottle", "milkbox", "box", "can", "note", "toothbrush", "brokendish"};
        string[] category = { "plastic", "paper", "paper", "can", "paper", "normal", "normal"};
        for (int i = 0; i < 7; i++)
        {
            ItemData data = new ItemData();

            data.name = itemName[i];
            data.category = category[i];
            data.clear = 0;
            data.count = 0;

            listItemData.Add(data);
        }

        //StartCoroutine(GetRequest("http://localhost:3000/register"));
        //StartCoroutine(GetRequest("http://192.168.1.75:8888"));
        StartCoroutine(GetRequest("http://192.168.1.20:8888"));

    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);

            }
            else
            {
                Debug.Log("Received: " + www.downloadHandler.text);
                //코드가 맞으면 접속

            }
        }
    }

    public void SendRequest(HttpInfo httpInfo)
    {
        StartCoroutine(CoSendRequest(httpInfo));
    }

    IEnumerator CoSendRequest(HttpInfo httpInfo)
    {
        UnityWebRequest req = null;

        //POST, GET, PUT, DELETE 분기
        switch (httpInfo.requestType)
        {
            case RequestType.GET:
                //Get방식으로 req 에 정보 셋팅
                req = UnityWebRequest.Get(httpInfo.url);
                break;
            case RequestType.POST:
                req = UnityWebRequest.Post(httpInfo.url, httpInfo.body);
                byte[] byteBody = Encoding.UTF8.GetBytes(httpInfo.body);
                req.uploadHandler = new UploadHandlerRaw(byteBody);
                //헤더 추가
                req.SetRequestHeader("Content-Type", "application/json");

                break;
            case RequestType.PUT:
                req = UnityWebRequest.Put(httpInfo.url, httpInfo.body);
                break;
            case RequestType.DELETE:
                req = UnityWebRequest.Delete(httpInfo.url);
                break;
            case RequestType.TEXTURE:
                req = UnityWebRequestTexture.GetTexture(httpInfo.url);
                break;
        }

        //서버에 요청을 보내고 응답이 올때까지 양보한다.
        yield return req.SendWebRequest();

        //만약에 응답이 성공했다면
        if (req.result == UnityWebRequest.Result.Success)
        {
            print("네트워크 응답 : " + req.downloadHandler.text);



            if (httpInfo.onReceive != null)
            {
                httpInfo.onReceive(req.downloadHandler);
            }
        }
        //통신 실패
        else
        {
            print("네트워크 에러 : " + req.error);
        }

        req.Dispose();
    }


    //회원가입
    public void SignUp(string id, string pw, string region)
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/register", (DownloadHandler downloadHandler) =>
        {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("응답성공 : " + downloadHandler.text);

            JObject json = JObject.Parse(downloadHandler.text);
            bool success = json["success"].ToObject<bool>();
            string message = json["message"].ToString();

            int userId = 0;
            if (json["userId"] != null)
            {
                userId = json["userId"].ToObject<int>();
            }


            if (success == false)
            {
                print("실패 : " + message);
                TitleSceneManager.instance.CheckBox(false);
            }
            else
            {
                //JsonList<LoginData> list = JsonUtility.FromJson<JsonList<LoginData>>(downloadHandler.text);

                //print(list.results[0].success);

                print("성공 : userid : " + userId + ", message : "+ message);
                TitleSceneManager.instance.CheckBox(true);
            }
            //byte[] byteData = new byte[downloadHandler.data.Length];
            //print(downloadHandler.data.Length);
            //string jsonData = Encoding.UTF8.GetString(byteData);

            //JsonList<LoginData> loginList = JsonUtility.FromJson<JsonList<LoginData>>(jsonData);

            //print(loginList.data.Count);
            //for(int i = 0; i < loginList.data.Count; i++)
            //{
            //    print(loginList.data[i]);
            //    if(loginList.data[i].success == true)
            //    {
            //        TitleSceneManager.instance.CheckBox(true);
            //    }
            //    else
            //    {
            //        TitleSceneManager.instance.CheckBox(false);
            //    }
            //}


        });

        JObject json = new JObject();
        json["login_id"] = id;
        json["pw"] = pw;
        json["region"] = region;


        info.body = json.ToString();
        //request.Dispose();
        SendRequest(info);

    }

    //로그인
    public void Login(string id, string pw, Action complete)
    {
        myInfo.login_id = id;

        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/login", (DownloadHandler downloadHandler) =>
        {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("응답성공 : " + downloadHandler.text);

            complete();

        });

        JObject json = new JObject();
        json["login_id"] = id;
        json["pw"] = pw;

        //LoginData signUpInfo = new LoginData();
        //signUpInfo.login_id = id;
        //signUpInfo.pw = pw;


        info.body = json.ToString();// JsonUtility.ToJson(signUpInfo);
        //request.Dispose();
        SendRequest(info);
    }

     

    public void Logout()
    {
        myInfo.login_id = null;
    }

    public void Score()
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/game/result", (DownloadHandler downloadHandler) =>
        {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("응답성공 : " + downloadHandler.text);
        });

        JObject json = new JObject();

        //user Json 만듬
        JObject user = new JObject();
        user["login_id"] = myInfo.login_id;
        user["score"] = 10;

        json["user"] = user;

        //data Json  만듬
        JArray jsonArray = new JArray();
        for(int i = 0; i < listItemData.Count; i++)
        {
            JObject itemData = new JObject();
            itemData["name"] = listItemData[i].name;
            itemData["category"] = listItemData[i].category;
            itemData["count"] = listItemData[i].count;
            itemData["clear"] = listItemData[i].clear;

            jsonArray.Add(itemData);
        }

        json["data"] = jsonArray;

        info.body = json.ToString();

        SendRequest(info);
    }


    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            for(int i = 0; i < 50; i++)
            {
                int rand = UnityEngine.Random.Range(0, 7);
                int success = UnityEngine.Random.Range(0, 2);

                listItemData[rand].count++;
                if(success == 1)
                {
                    listItemData[rand].clear++;
                }
            }
            print("111");

            Score();
        }
    }

    //public void Signup()
    //{
    //    HttpInfo info = new HttpInfo();
    //    info.Set(RequestType.POST, "/sign_up", (DownloadHandler) => { });

    //    MyInfo myInfo = new MyInfo();

    //    if(myInfo.id == "1" && myInfo.email == "Sincere@april.biz")
    //    {
    //        print("1번 접속");
    //    }

    //    HttpManager.instance.SendRequest(info);
    //}


}
