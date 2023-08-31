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

    // ȸ������ �˾�â
    public GameObject popup;
    public GameObject checkBox;

    // üũ�ڽ�
    public Text result;
    public Text reason;

    public bool signup = true;
    public bool signin = true;


    void Start()
    {
        inputEmail.text = "1@naver.com";
        inputPassword.text = "1";

        // �˾�â ��Ȱ��ȭ
        popup.SetActive(false);
        // üũ�ڽ� ��Ȱ��ȭ
        checkBox.SetActive(false);
        // ��ũ�Ѻ� ��Ȱ��ȭ
        regionScroll.SetActive(false);
        //
        regionSelectButton.color = Color.white;
    }

    #region ��ư


    // ��� ��ư�� ���� �� ȣ��
    public void OnClickCancel(int num)
    {
        // ȸ������ ��ҹ�ư
        if (num == 0)
        {
            // ȸ������ �˾�â ��Ȱ��ȭ
            popup.SetActive(false);

            // InputField �ʱ�ȭ
            popupEmail.text = "";
            popupPassword.text = "";
            regionSelectButton.text = "������ �����ϼ���.";
            regionSelectButton.color = Color.gray;
        }
        // üũ�ڽ� �ݱ��ư
        else if (num == 1)
        {
            if (signup == true)
            {
                // ȸ������ �˾�â ��Ȱ��ȭ
                popup.SetActive(false);
            }

            // üũ�ڽ� ��Ȱ��ȭ
            checkBox.SetActive(false);

            // InputField �ʱ�ȭ
            popupEmail.text = "";
            popupPassword.text = "";
            regionSelectButton.text = "������ �����ϼ���.";
            regionSelectButton.color = Color.gray;
        }
    }
    #endregion


    #region ȸ������ ��ư�� ���� �� ȣ��

    // ȸ������ �˾�
    public void OnClickPopup()
    {
        // ȸ������ �˾�â Ȱ��ȭ
        popup.SetActive(true);
    }

    public void OnClickSelectRegion()
    {
        // �������� ��ũ�ѹ� Ȱ��ȭ
        regionScroll.SetActive(true);
    }

    public void OnClickRegion(string s)
    {
        // �����̸�
        regionSelectButton.text = s;

        // �۾� ���� : ���
        regionSelectButton.color = Color.white;

        // ���� ��ũ�ѹ� ��Ȱ��ȭ
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
            // ȸ������ ���� -> ȸ������ ���� �˾� UI
            checkBox.SetActive(true);
            result.text = "ȸ������ ����";
            reason.text = "";
        }

        // �׷��� ������
        else
        {
            // ȸ������ ���� -> ���п��� �˾� UI
            checkBox.SetActive(true);
            result.text = "ȸ������ ����";
            //reason.text = "�̹� ȸ�����Ե� �̸����Դϴ�.";   // ���߿� �������� �޾ƿ��� �ڵ�� ����
            //reason.text = downloadHandler.text;   // ���߿� �������� �޾ƿ��� �ڵ�� ����
        }
    }

    //IEnumerator CoSignUp()
    //{
    //    print("ȸ������");

    //    // ȸ������ �õ�
    //    print("LoginManager.instance : " + LoginManager.instance);
    //    print("popupEmail : " + popupEmail);
    //    print("popupPassword : " + popupPassword);
    //    print("regionSelectButton : " + regionSelectButton);
    //    v

    //    // ��ٸ�
    //    yield return new WaitForSeconds(2f); // new WaitUntil(() => ȸ������ �Ϸ�);

    //    // ���� ���ܻ����� ������ 
    //    
    //}
    #endregion


    #region �α��� ��ư�� ���� �� ȣ��
    public void OnClickSignIn()
    {
        LoginManager.instance.Login(inputEmail.text, inputPassword.text, () => {
            StartCoroutine(CoSignIn());
        });
    }

    IEnumerator CoSignIn()
    {
        print("�α���");

        // �α��� �õ�

        // ��ٸ�
        yield return null; // new WaitUntil(() => �α��� �Ϸ�);

        // ���� ���ܻ����� ������
        if (signin == true)
        {
            // �α��� ���� -> �κ������ �̵�        
            SceneManager.LoadScene("02.LobbyScene");
        }

        // �׷��� ������
        else
        {
            // �α��� ���� -> ���п��� �˾� UI
            checkBox.SetActive(true);
            result.text = "�α��� ����";
            reason.text = "�̸��� Ȥ�� ��й�ȣ�� Ȯ���ϼ���.";    // ���߿� �������� �޾ƿ��� �ڵ�� ����
        }
    }
    #endregion


    #region ���Ӳ��� ��ư�� ���� �� ȣ��
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
