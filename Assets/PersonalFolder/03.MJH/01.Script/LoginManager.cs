using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Text;


[Serializable]
public class LoginData
{
    //public string id;
    public string login_id;
    public string pw;
    public string region;
    //public int clear;
    //public int sum;
    //public float probabilty;
}

public class MemberData
{
    public List<LoginData> members = new List<LoginData>();
    //public bool succes;
    
}

public class LoginManager : MonoBehaviour
{
    public static LoginManager instance;


    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(GetRequest("https://localhost:3000/register"));
        StartCoroutine(GetRequest("http://192.168.1.75:3000"));

        
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

        req.Dispose();
    }


    //회원가입
    public void SignUp(string id, string pw, string region)
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/register", (DownloadHandler downloadHandler) => {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("응답성공 : " +  downloadHandler.text);
        });

        LoginData signUpInfo = new LoginData();
        signUpInfo.login_id = id;
        signUpInfo.pw = pw;
        signUpInfo.region = region;

        info.body = JsonUtility.ToJson(signUpInfo);
        //request.Dispose();
        SendRequest(info);
       
    }

    //로그인
    public void Login(string id, string pw)
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/login", (DownloadHandler downloadHandler) => {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("응답성공 : " + downloadHandler.text);
        });

        LoginData signUpInfo = new LoginData();
        signUpInfo.login_id = id;
        signUpInfo.pw = pw;


        info.body = JsonUtility.ToJson(signUpInfo);
        //request.Dispose();
        SendRequest(info);
    }



    // Update is called once per frame
    void Update()
    {
        
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
