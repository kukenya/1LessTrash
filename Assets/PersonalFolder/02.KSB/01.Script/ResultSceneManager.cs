using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    // �����Ʈ �˾�
    public GameObject note;

    // �����Ʈ ����
    public GameObject[] notes;

    void Start()
    {
        // �˾�â ��Ȱ��ȭ
        note.SetActive(false);

        // �����Ʈ ���� ��Ȱ��ȭ
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
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
        // ������ ������ ��ȣ�� �޾ƿ´�. <- ��������


        // �����Ʈ �˾��� ���.
        note.SetActive(true);

        // ���� ������ UI�� ���� Ȱ��ȭ�Ѵ�.

    }

    public void OnClickPrev()
    {
        // ���� ����
        // currCount++;
    }

    public void OnClickNext()
    {
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
