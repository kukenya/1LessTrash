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
        maxPage = 4;    // ���� ������� ���� ���� ����
        currPage = 1;

        // page.text = currPage + " / " + maxPage;
    }

    private void Update()
    {
        // ���� ��ư Ȱ��/��Ȱ��
        if (currPage >= maxPage)
        {
            nextBtn.SetActive(false);
        }
        else if (currPage < maxPage)
        {
            nextBtn.SetActive(true);
        }

        // ���� ��ư Ȱ��/��Ȱ��
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
        // �ٽ��ϱ� -> ���Ӿ�
        SceneManager.LoadScene("03.GameScene");
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
        for (int i = 0; i < notes.Length; i++)
        {
            notes[1].gameObject.GetComponent<Text>();
        }

        // maxPage �� �����Ѵ�.

    }

    public void OnClickPrev()
    {
        // ���������� ����
        currPage--;
        
        // ���� ����

    }

    public void OnClickNext()
    {
        // ���������� ����
        currPage++;
        
        // ���� ����
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
