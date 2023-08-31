using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    // 오답노트 팝업
    public GameObject note;

    // 문제 번호
    public Text qNumber;

    // 오답노트 내용
    public GameObject[] notes;

    // 오답노트 페이지
    int currPage;
    int maxPage;
    public Text page;

    // 버튼
    public GameObject prevBtn;
    public GameObject nextBtn;

    void Start()
    {
        // 팝업창 비활성화
        note.SetActive(false);

        // 오답노트 내용 비활성화
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }

        // 오답노트 페이지
        maxPage = 4;    // 게임 결과에서 나온 오답 개수
        currPage = 1;

        // page.text = currPage + " / " + maxPage;
    }

    private void Update()
    {
        // 다음 버튼 활성/비활성
        if (currPage >= maxPage)
        {
            nextBtn.SetActive(false);
        }
        else if (currPage < maxPage)
        {
            nextBtn.SetActive(true);
        }

        // 이전 버튼 활성/비활성
        if (currPage <= 1)
        {
            prevBtn.SetActive(false);
        }
        else if (currPage > 1)
        {
            prevBtn.SetActive(true);
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
        // 오답인 문제의 번호를 받아온다. <- 서버에서?
        

        // 오답노트 팝업이 뜬다.
        note.SetActive(true);

        // 오답 문제의 UI만 전부 활성화한다.
        for (int i = 0; i < notes.Length; i++)
        {
            notes[1].gameObject.GetComponent<Text>();
        }

        // maxPage 를 설정한다.

    }

    public void OnClickPrev()
    {
        // 페이지수를 감소
        currPage--;
        
        // 이전 내용

    }

    public void OnClickNext()
    {
        // 페이지수를 증가
        currPage++;
        
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
