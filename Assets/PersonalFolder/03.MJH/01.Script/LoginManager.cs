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
public class JsonList<T>
{
    public List<T> results;
}


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
    public bool success;

}

[Serializable]
public class LoginResult
{
    public bool success;
    public string message;
    public int userId;
}

    public class MemberData
{
    public List<LoginData> members = new List<LoginData>();
    //public bool succes;

}

public class LoginManager : MonoBehaviour
{
    public static LoginManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

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
                //�ڵ尡 ������ ����

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

        //POST, GET, PUT, DELETE �б�
        switch (httpInfo.requestType)
        {
            case RequestType.GET:
                //Get������� req �� ���� ����
                req = UnityWebRequest.Get(httpInfo.url);
                break;
            case RequestType.POST:
                req = UnityWebRequest.Post(httpInfo.url, httpInfo.body);
                byte[] byteBody = Encoding.UTF8.GetBytes(httpInfo.body);
                req.uploadHandler = new UploadHandlerRaw(byteBody);
                //��� �߰�
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

        //������ ��û�� ������ ������ �ö����� �纸�Ѵ�.
        yield return req.SendWebRequest();

        //���࿡ ������ �����ߴٸ�
        if (req.result == UnityWebRequest.Result.Success)
        {
            print("��Ʈ��ũ ���� : " + req.downloadHandler.text);



            if (httpInfo.onReceive != null)
            {
                httpInfo.onReceive(req.downloadHandler);
            }
        }
        //��� ����
        else
        {
            print("��Ʈ��ũ ���� : " + req.error);
        }

        req.Dispose();
    }


    //ȸ������
    public void SignUp(string id, string pw, string region)
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/register", (DownloadHandler downloadHandler) =>
        {
            //Post ������ �������� �� �����κ��� ���� �ɴϴ�~
            print("���伺�� : " + downloadHandler.text);

            LoginResult result = JsonUtility.FromJson<LoginResult>(downloadHandler.text);

            if (result != null && result.success == false)
            {
                print("���� : " + result.message);
                TitleSceneManager.instance.CheckBox(false);
            }
            else
            {
                //JsonList<LoginData> list = JsonUtility.FromJson<JsonList<LoginData>>(downloadHandler.text);

                //print(list.results[0].success);

                print("���� : userid : " + result.userId + ", message : "+ result.message);
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

        LoginData signUpInfo = new LoginData();
        signUpInfo.login_id = id;
        signUpInfo.pw = pw;
        signUpInfo.region = region;

        info.body = JsonUtility.ToJson(signUpInfo);
        //request.Dispose();
        SendRequest(info);

    }

    //�α���
    public void Login(string id, string pw)
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/login", (DownloadHandler downloadHandler) =>
        {
            //Post ������ �������� �� �����κ��� ���� �ɴϴ�~
            print("���伺�� : " + downloadHandler.text);
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
    //        print("1�� ����");
    //    }

    //    HttpManager.instance.SendRequest(info);
    //}


}
