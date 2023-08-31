using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TitleSceneManager : MonoBehaviour
{
    public static TitleSceneManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Input Field
    public InputField inputEmail;
    public InputField inputPassword;

    // Popup Input Field
    public InputField popupEmail;
    public InputField popupPassword;
    public Text regionSelectButton;
    public GameObject regionScroll;

    // 회원가입 팝업창
    public GameObject popup;
    public GameObject checkBox;

    // 체크박스
    public Text result;
    public Text reason;

    public bool signup = true;
    public bool signin = true;


    void Start()
    {
        inputEmail.text = "1@naver.com";
        inputPassword.text = "1";

        // 팝업창 비활성화
        popup.SetActive(false);
        // 체크박스 비활성화
        checkBox.SetActive(false);
        // 스크롤뷰 비활성화
        regionScroll.SetActive(false);
        //
        regionSelectButton.color = Color.white;
    }

    #region 버튼


    // 취소 버튼을 누를 때 호출
    public void OnClickCancel(int num)
    {
        // 회원가입 취소버튼
        if (num == 0)
        {
            // 회원가입 팝업창 비활성화
            popup.SetActive(false);

            // InputField 초기화
            popupEmail.text = "";
            popupPassword.text = "";
            regionSelectButton.text = "지역을 선택하세요.";
            regionSelectButton.color = Color.gray;
        }
        // 체크박스 닫기버튼
        else if (num == 1)
        {
            if (signup == true)
            {
                // 회원가입 팝업창 비활성화
                popup.SetActive(false);
            }

            // 체크박스 비활성화
            checkBox.SetActive(false);

            // InputField 초기화
            popupEmail.text = "";
            popupPassword.text = "";
            regionSelectButton.text = "지역을 선택하세요.";
            regionSelectButton.color = Color.gray;
        }
    }
    #endregion


    #region 회원가입 버튼을 누를 때 호출

    // 회원가입 팝업
    public void OnClickPopup()
    {
        // 회원가입 팝업창 활성화
        popup.SetActive(true);
    }

    public void OnClickSelectRegion()
    {
        // 지역선택 스크롤바 활성화
        regionScroll.SetActive(true);
    }

    public void OnClickRegion(string s)
    {
        // 지역이름
        regionSelectButton.text = s;

        // 글씨 색깔 : 흰색
        regionSelectButton.color = Color.white;

        // 지역 스크롤바 비활성화
        regionScroll.SetActive(false);
    }

    public void OnClickSignUp()
    {
        //StartCoroutine(CoSignUp());
        print(regionSelectButton.text);
        LoginManager.instance.SignUp(popupEmail.text, popupPassword.text, regionSelectButton.text);
    }

    public void CheckBox(bool isactive/*, DownloadHandler downloadHandler*/)
    {
        if (signup == isactive)
        {
            // 회원가입 성공 -> 회원가입 성공 팝업 UI
            checkBox.SetActive(true);
            result.text = "회원가입 성공";
            reason.text = "";
        }

        // 그렇지 않으면
        else
        {
            // 회원가입 실패 -> 실패원인 팝업 UI
            checkBox.SetActive(true);
            result.text = "회원가입 실패";
            //reason.text = "이미 회원가입된 이메일입니다.";   // 나중에 서버에서 받아오는 코드로 변경
            //reason.text = downloadHandler.text;   // 나중에 서버에서 받아오는 코드로 변경
        }
    }

    //IEnumerator CoSignUp()
    //{
    //    print("회원가입");

    //    // 회원가입 시도
    //    print("LoginManager.instance : " + LoginManager.instance);
    //    print("popupEmail : " + popupEmail);
    //    print("popupPassword : " + popupPassword);
    //    print("regionSelectButton : " + regionSelectButton);
    //    v

    //    // 기다림
    //    yield return new WaitForSeconds(2f); // new WaitUntil(() => 회원가입 완료);

    //    // 만약 예외사항이 없으면 
    //    
    //}
    #endregion


    #region 로그인 버튼을 누를 때 호출
    public void OnClickSignIn()
    {
        LoginManager.instance.Login(inputEmail.text, inputPassword.text, () => {
            StartCoroutine(CoSignIn());
        });
    }

    IEnumerator CoSignIn()
    {
        print("로그인");

        // 로그인 시도

        // 기다림
        yield return null; // new WaitUntil(() => 로그인 완료);

        // 만약 예외사항이 없으면
        if (signin == true)
        {
            // 로그인 성공 -> 로비씬으로 이동        
            SceneManager.LoadScene("02.LobbyScene");
        }

        // 그렇지 않으면
        else
        {
            // 로그인 실패 -> 실패원인 팝업 UI
            checkBox.SetActive(true);
            result.text = "로그인 실패";
            reason.text = "이메일 혹은 비밀번호를 확인하세요.";    // 나중에 서버에서 받아오는 코드로 변경
        }
    }
    #endregion


    #region 게임끄기 버튼을 누를 때 호출
    public void OnClickExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}
