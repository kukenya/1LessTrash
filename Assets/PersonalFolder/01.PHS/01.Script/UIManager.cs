using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            myCanvas = GetComponent<Canvas>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Canvas myCanvas;

    public GameObject firstUI;
    public GameObject secondUI;

    private void Start()
    {
        this.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnFirstUI()
    {
        firstUI.SetActive(true);
        secondUI.SetActive(false);
    }

    public void OnSecondUI()
    {
        firstUI.SetActive(false);
        secondUI.SetActive(true);
    }
}
