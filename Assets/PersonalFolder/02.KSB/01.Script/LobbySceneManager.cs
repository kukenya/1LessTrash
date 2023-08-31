using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour
{
    // �޴� �˾�â
    public GameObject menu;

    // ���� �˾�â
    public GameObject score;

    // UI ĵ, ��ö
    public Text[] txtCan;
    public float recycleCan;
    public GameObject[] goCan;

    // UI ����
    public Text[] txtPaper;
    public float recyclePaper;
    public GameObject[] goPaper;

    // UI �ö�ƽ
    public Text[] txtPlastic;
    public float recyclePlastic;
    public GameObject[] goPlastic;


    void Start()
    {
        menu.SetActive(false);
        score.SetActive(false);
    }


    private void Update()
    {
        #region Can
        // ��Ȱ������ 30 ���϶��
        if (recycleCan <= 30 && recycleCan >= 0)
        {
            // ������ �׾
            goCan[0].SetActive(true);
            goCan[1].SetActive(false);
            goCan[2].SetActive(false);
        }
        // ��Ȱ������ 30 �ʰ�, 70 ���϶�� 
        else if (recycleCan > 30 && recycleCan <= 70)
        {
            goCan[0].SetActive(false);
            goCan[1].SetActive(true);
            goCan[2].SetActive(false);
        }
        else if (recycleCan < 70 && recycleCan <= 100)
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

        // ��Ȱ������ 30 ���϶��
        if (recyclePaper <= 30)
        {
            // ������ �׾
            goPaper[0].SetActive(true);
            goPaper[1].SetActive(false);
            goPaper[2].SetActive(false);
        }
        // ��Ȱ������ 30 �ʰ�, 70 ���϶�� 
        else if (recyclePaper > 30 && recyclePaper <= 70)
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

        // ��Ȱ������ 30 ���϶��
        if (recyclePlastic <= 30)
        {
            // ������ �׾
            goPlastic[0].SetActive(true);
            goPlastic[1].SetActive(false);
            goPlastic[2].SetActive(false);
        }
        // ��Ȱ������ 30 �ʰ�, 70 ���϶�� 
        else if (recyclePlastic > 30 && recyclePlastic <= 70)
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

        // UI ����
        UICan();
        UIPaper();
        UIPlastic();
    }

    // UI Can
    public void UICan()
    {
        txtCan[0].text = "���ⷮ : " + "����" + " ��";
        txtCan[1].text = "��Ȱ�뷮 : " + "����" + " ��";
        txtCan[2].text = "��Ȱ���� : " + recycleCan + "%";
    }

    // UI Paper
    public void UIPaper()
    {
        txtPaper[0].text = "���ⷮ : " + "����" + " ��";
        txtPaper[1].text = "��Ȱ�뷮 : " + "����" + " ��";
        txtPaper[2].text = "��Ȱ���� : " + recyclePaper + "%";
    }

    // UI Plastic
    public void UIPlastic()
    {
        txtPlastic[0].text = "���ⷮ : " + "����" + " ��";
        txtPlastic[1].text = "��Ȱ�뷮 : " + "����" + " ��";
        txtPlastic[2].text = "��Ȱ���� : " + recyclePlastic + "%";
    }


    // ���ӽ�ŸƮ ��ư
    public void OnClickGameStart()
    {
        print("���� ����");

        // ���Ӿ����� �̵�
        SceneManager.LoadScene("03.GameScene");
    }

    // �������� ��ư
    public void OnClickScore()
    {
        print("��������");

        // �������� �˾�â
    }

    // �޴� ��ư
    public void OnClickMenu(int n)
    {
        if (n == 0)
        {
            // �޴�â ����
            menu.SetActive(true);
        }
        else if (n == 1)
        {
            // �޴�â �ݱ�
            menu.SetActive(false);
        }
        else if (n == 2)
        {
            // �α׾ƿ�
            print("�α׾ƿ�");
            // Ÿ��Ʋ������ ���ư���
            SceneManager.LoadScene("01.TitleScene");
        }
        else if (n == 3)
        {
            // ���� ����
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
