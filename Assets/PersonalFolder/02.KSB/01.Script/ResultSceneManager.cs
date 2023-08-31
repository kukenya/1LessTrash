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
    float[] num = { 2, 5, 7 };
    //float num2 = 5;
    //float num3 = 7;

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
        maxPage = notes.Length;    // 게임 결과에서 나온 오답 개수
        currPage = 1;

        qNumber.text = "문제 번호 : " + num[0];
    }

    private void Update()
    {

        #region 버튼
        // 다음 버튼 활성/비활성
        if (currPage >= maxPage) nextBtn.SetActive(false);
        else if (currPage < maxPage) nextBtn.SetActive(true);

        // 이전 버튼 활성/비활성
        if (currPage <= 1) prevBtn.SetActive(false);
        else if (currPage > 1) prevBtn.SetActive(true);
        #endregion

        #region Text
        if (currPage == 1)
        {           
            for (int i = 0; i < notes.Length; i++)
            {
                if(i == 0) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 2)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 1)notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 3)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 2) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 4)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 3) notes[i].SetActive(true);
                else  notes[i].SetActive(false);
            }
        }
        else if (currPage == 5)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 4) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 6)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 5) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 7)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 6) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 8)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 7) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        else if (currPage == 9)
        {
            for (int i = 0; i < notes.Length; i++)
            {
                if (i == 8) notes[i].SetActive(true);
                else notes[i].SetActive(false);
            }
        }
        #endregion
    }

    // ReTry
    public void OnClickReTry()
    {
        // 다시하기 -> 게임씬
        SceneManager.LoadScene("PHS");
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


        // maxPage 를 설정한다.

    }

    public void OnClickPrev()
    {
        // 페이지수를 감소
        currPage--;
        if(currPage == 1)
        {
            qNumber.text = "문제 번호 : " + num[0];
        }
        else if(currPage == 2)
        {
            qNumber.text = "문제 번호 : " + num[1];
        }
    }

    public void OnClickNext()
    {
        // 페이지수를 증가
        currPage++;
        if(currPage == 2)
        {
            qNumber.text = "문제 번호 : " + num[1];
        }
        else if(currPage == 3)
        {
            qNumber.text = "문제 번호 : " + num[2];
        }
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
