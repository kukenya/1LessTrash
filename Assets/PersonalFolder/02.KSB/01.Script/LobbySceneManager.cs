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

    void Start()
    {
        menu.SetActive(false);
        score.SetActive(false);
    }

    // 게임스타트 버튼
    public void OnClickGameStart()
    {
        print("게임 시작");

        // 게임씬으로 이동
        SceneManager.LoadScene("03.GameScene");
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
            // 메뉴창 열기
            menu.SetActive(true);
        }
        else if(n == 1)
        {
            // 메뉴창 닫기
            menu.SetActive(false);
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
        if(gameObject.name == "Plastic")
        {
            print("Plastic");
        }
    }
}
