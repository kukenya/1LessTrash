using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ReTry
    public void OnClickReTry()
    {
        // �ٽ��ϱ� -> ���Ӿ�
        SceneManager.LoadScene("03.GameScene");
    }

    // Title
    public void OnClickLobby()
    {
        // �κ������ -> �κ��
        SceneManager.LoadScene("02.LobbyScene");
    }

    // ExitGame
    public void OnClickExitGame()
    {
        // ��������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
