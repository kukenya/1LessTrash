using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

/// <summary>
/// ���� ����
/// Json >> email, password ��ġ ���� Ȯ�� >> ������ ���� ����
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

//�� ����ϱ� ���� ����
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

        ////info �� ������ ��û�� ������
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
            //print("��Ʈ��ũ ���� : " + req.downloadHandler.text);

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
    }
}
