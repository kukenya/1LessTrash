using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour
{
    // 메뉴 팝업창
    public GameObject menu;

    // 전적 팝업창
    public GameObject score;

    // UI 캔, 고철
    public Text[] txtCan;
    public float recycleCan;
    public GameObject[] goCan;

    // UI 종이
    public Text[] txtPaper;
    public float recyclePaper;
    public GameObject[] goPaper;

    // UI 플라스틱
    public Text[] txtPlastic;
    public float recyclePlastic;
    public GameObject[] goPlastic;

    // 나무들
    public GameObject tree;

    // UI
    public GameObject[] UI;

    void Start()
    {
        menu.SetActive(false);
        score.SetActive(false);
    }


    private void Update()
    {
        #region Can
        // 재활용율이 30 이하라면
        if (recycleCan <= 10 && recycleCan >= 0)
        {
            // 나무가 죽어감
            goCan[0].SetActive(true);
            goCan[1].SetActive(false);
            goCan[2].SetActive(false);
        }
        // 재활용율이 30 초과, 70 이하라면 
        else if (recycleCan > 10 && recycleCan <= 19)
        {
            goCan[0].SetActive(false);
            goCan[1].SetActive(true);
            goCan[2].SetActive(false);
        }
        else if (recycleCan > 19 && recycleCan <= 100)
        {
            goCan[0].SetActive(false);
            goCan[1].SetActive(false);
            goCan[2].SetActive(true);
        }
        if (recycleCan > 100)
        {
            recycleCan = 100;
        }
        if (recycleCan < 0)
        {
            recycleCan = 0;
        }
        #endregion

        #region Paper
        Mathf.Clamp(recyclePaper, 0, 100);

        // 재활용율이 30 이하라면
        if (recyclePaper <= 10)
        {
            // 나무가 죽어감
            goPaper[0].SetActive(true);
            goPaper[1].SetActive(false);
            goPaper[2].SetActive(false);
        }
        // 재활용율이 30 초과, 70 이하라면 
        else if (recyclePaper > 10 && recyclePaper <= 19)
        {
            goPaper[0].SetActive(false);
            goPaper[1].SetActive(true);
            goPaper[2].SetActive(false);
        }
        else
        {
            goPaper[0].SetActive(false);
            goPaper[1].SetActive(false);
            goPaper[2].SetActive(true);
        }
        if (recyclePaper > 100)
        {
            recyclePaper = 100;
        }
        if (recyclePaper < 0)
        {
            recyclePaper = 0;
        }
        #endregion

        #region Plastic
        Mathf.Clamp(recyclePlastic, 0, 100);

        // 재활용율이 30 이하라면
        if (recyclePlastic <= 10)
        {
            // 나무가 죽어감
            goPlastic[0].SetActive(true);
            goPlastic[1].SetActive(false);
            goPlastic[2].SetActive(false);
        }
        // 재활용율이 30 초과, 70 이하라면 
        else if (recyclePlastic > 10 && recyclePlastic <= 19)
        {
            goPlastic[0].SetActive(false);
            goPlastic[1].SetActive(true);
            goPlastic[2].SetActive(false);
        }
        else
        {
            goPlastic[0].SetActive(false);
            goPlastic[1].SetActive(false);
            goPlastic[2].SetActive(true);
        }
        if (recyclePlastic > 100)
        {
            recyclePlastic = 100;
        }
        if (recyclePlastic < 0)
        {
            recyclePlastic = 0;
        }
        #endregion

        // UI 갱신
        UICan();
        UIPaper();
        UIPlastic();
    }

    // UI Can
    public void UICan()
    {
        txtCan[0].text = "배출량 : " + "서버" + " 톤";
        txtCan[1].text = "재활용량 : " + "서버" + " 톤";
        txtCan[2].text = "재활용율 : " + recycleCan + "%";
    }

    // UI Paper
    public void UIPaper()
    {
        txtPaper[0].text = "배출량 : " + "서버" + " 톤";
        txtPaper[1].text = "재활용량 : " + "서버" + " 톤";
        txtPaper[2].text = "재활용율 : " + recyclePaper + "%";
    }

    // UI Plastic
    public void UIPlastic()
    {
        txtPlastic[0].text = "배출량 : " + "서버" + " 톤";
        txtPlastic[1].text = "재활용량 : " + "서버" + " 톤";
        txtPlastic[2].text = "재활용율 : " + recyclePlastic + "%";
    }


    // 게임스타트 버튼
    public void OnClickGameStart()
    {
        print("게임 시작");

        // 게임씬으로 이동
        SceneManager.LoadScene("PHS");
    }

    // 전적보기 버튼
    public void OnClickScore()
    {
        print("전적보기");

        // 전적보기 팝업창
    }

    // 메뉴 버튼
    public void OnClickMenu(int n)
    {
        if (n == 0)
        {
            // 나무 비활성화
            tree.SetActive(false);

            // UI 비활성화
            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(false);
            }

            // 메뉴창 열기
            menu.SetActive(true);
        }
        else if (n == 1)
        {
            // 메뉴창 닫기
            menu.SetActive(false);

            // UI 활성화
            for (int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(true);
            }

            // 나무 활성화
            tree.SetActive(true);
        }
        else if (n == 2)
        {
            // 로그아웃
            print("로그아웃");
            // 타이틀씬으로 돌아가기
            SceneManager.LoadScene("01.TitleScene");
        }
        else if (n == 3)
        {
            // 게임 종료
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
        }
    }

    private void OnMouseEnter()
    {
        if (gameObject.name == "Plastic")
        {
            print("Plastic");
        }
    }
}
