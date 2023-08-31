using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    // �����Ʈ �˾�
    public GameObject note;

    // ���� ��ȣ
    public Text qNumber;

    // �����Ʈ ����
    public GameObject[] notes;

    // �����Ʈ ������
    int currPage;
    int maxPage;
    public Text page;
    float[] num = { 2, 5, 7 };
    //float num2 = 5;
    //float num3 = 7;

    // ��ư
    public GameObject prevBtn;
    public GameObject nextBtn;

    void Start()
    {
        // �˾�â ��Ȱ��ȭ
        note.SetActive(false);

        // �����Ʈ ���� ��Ȱ��ȭ
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }

        // �����Ʈ ������
        maxPage = notes.Length;    // ���� ������� ���� ���� ����
        currPage = 1;

        qNumber.text = "���� ��ȣ : " + num[0];
    }

    private void Update()
    {

        #region ��ư
        // ���� ��ư Ȱ��/��Ȱ��
        if (currPage >= maxPage) nextBtn.SetActive(false);
        else if (currPage < maxPage) nextBtn.SetActive(true);

        // ���� ��ư Ȱ��/��Ȱ��
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
        // �ٽ��ϱ� -> ���Ӿ�
        SceneManager.LoadScene("PHS");
    }

    // Title
    public void OnClickLobby()
    {
        // �κ������ -> �κ��
        SceneManager.LoadScene("02.LobbyScene");
    }

    #region �����Ʈ
    public void OnClickNote()
    {
        // ������ ������ ��ȣ�� �޾ƿ´�. <- ��������?
        

        // �����Ʈ �˾��� ���.
        note.SetActive(true);

        // ���� ������ UI�� ���� Ȱ��ȭ�Ѵ�.


        // maxPage �� �����Ѵ�.

    }

    public void OnClickPrev()
    {
        // ���������� ����
        currPage--;
        if(currPage == 1)
        {
            qNumber.text = "���� ��ȣ : " + num[0];
        }
        else if(currPage == 2)
        {
            qNumber.text = "���� ��ȣ : " + num[1];
        }
    }

    public void OnClickNext()
    {
        // ���������� ����
        currPage++;
        if(currPage == 2)
        {
            qNumber.text = "���� ��ȣ : " + num[1];
        }
        else if(currPage == 3)
        {
            qNumber.text = "���� ��ȣ : " + num[2];
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
        // ��������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
