using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    // 오답노트 팝업
    public GameObject note;

    // 오답노트 내용
    public GameObject[] notes;

    void Start()
    {
        // 팝업창 비활성화
        note.SetActive(false);

        // 오답노트 내용 비활성화
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }
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

    #region 오답노트
    public void OnClickNote()
    {
        // 오답인 문제의 번호를 받아온다. <- 서버에서


        // 오답노트 팝업이 뜬다.
        note.SetActive(true);

        // 오답 문제의 UI만 전부 활성화한다.

    }

    public void OnClickPrev()
    {
        // 이전 내용
        // currCount++;
    }

    public void OnClickNext()
    {
        // 다음 내용

    }

    public void OnClickClose()
    {
        note.SetActive(false);
    }
    #endregion

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
