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

    void Start()
    {
        menu.SetActive(false);
        score.SetActive(false);
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
        else if(n == 1)
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
        if(gameObject.name == "Plastic")
        {
            print("Plastic");
        }
    }
}
