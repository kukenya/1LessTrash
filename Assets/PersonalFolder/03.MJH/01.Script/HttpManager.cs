using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

/// <summary>
/// 나의 정보
/// Json >> email, password 일치 여부 확인 >> 응답을 통해 진행
/// https
/// </summary>
[Serializable]
public struct MyInfo
{
    //public string id;
    public string login_id;
    public string pw;
    //public string region;
    //public int clear;
    //public int sum;
    //public float probabilty;
    public bool success;

}

public enum RequestType
{
    GET,
    POST,
    PUT,
    DELETE,
    TEXTURE
}

//웹 통신하기 위한 정보
public class HttpInfo
{
    public RequestType requestType;
    public string url = "";
    public string body;
    public Action<DownloadHandler> onReceive;

    public void Set(
        RequestType type,
        string u,
        Action<DownloadHandler> callback,
        bool useDefaultUrl = true)
    {
        requestType = type;
        if (useDefaultUrl) url = "http://192.168.1.20:8888";
        url += u;
        onReceive = callback;
    }
}


public class HttpManager : MonoBehaviour
{
    public static HttpManager instance;

    // Start is called before the first frame update
    void Start()
    {
        //HttpInfo info = new HttpInfo();
        //info.Set(RequestType.GET, "/login", (DownloadHandler downloadHandler) => {
        //    print("OnReceiveGet : " + downloadHandler.text);
        //});

        ////info.Set(RequestType.GET, "/todos", OnReceiveGet);

        ////info 의 정보로 요청을 보내자
        //SendRequest(info);
    }

    
    // Update is called once per frame
    void Update()
    {
        
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
            //print("네트워크 응답 : " + req.downloadHandler.text);

            if (httpInfo.onReceive != null)
            {
                httpInfo.onReceive(req.downloadHandler);
            }
        }
        //통신 실패
        else
        {
            print("네트워크 에레 : " + req.error);
        }
    }
}
