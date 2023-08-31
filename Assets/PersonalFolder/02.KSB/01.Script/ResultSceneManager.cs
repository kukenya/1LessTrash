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
        // 다시하기 -> 게임씬
        SceneManager.LoadScene("03.GameScene");
    }

    // Title
    public void OnClickLobby()
    {
        // 로비씬으로 -> 로비씬
        SceneManager.LoadScene("02.LobbyScene");
    }

    // ExitGame
    public void OnClickExitGame()
    {
        // 게임종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
