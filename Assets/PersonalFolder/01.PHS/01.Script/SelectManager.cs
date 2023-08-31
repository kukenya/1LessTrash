using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static SelectManager;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool isSelect = false;

    public GameObject interactionUI;
    public GameObject selectObj;

    public RectTransform placeHolder;

    public float focusScale = 1.5f;

    private void Start()
    {
        interactionUI.SetActive(false);
    }

    public void OnInteractionUI(GameObject go)
    {
        if (interactionUI.activeSelf == true) return;
        isSelect = true;
        interactionUI.SetActive(true);
        selectObj = go;
        SetLayerUI(selectObj);
        selectObj.transform.SetParent(placeHolder);
        selectObj.transform.localPosition = Vector3.zero;
        selectObj.transform.localScale *= focusScale;
    }

    void SetLayerUI(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).childCount > 0)
            {
                SetLayerUI(gameObject.transform.GetChild(i).gameObject);
            }

            gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }

    void SetLayerDefault(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).childCount > 0)
            {
                SetLayerUI(gameObject.transform.GetChild(i).gameObject);
            }

            gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }


    public void OffInteractionUI()
    {
        isSelect = false;
        SetLayerDefault(selectObj);
    }

    public bool checkRecycleBin = false;

    private void Update()
    {
        CheckRecycleBin();
        
    }

    public LayerMask layerMask;

    RecycleBin bin;

    public bool recycleClear = false;

    public bool rayHit = false;

    public RecycleBin.RecycleBinType currentRecycleBinType;

    void CheckRecycleBin()
    {
        if (checkRecycleBin == false) 
        {
            if (bin)
            {

            }
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 99f, layerMask))
        {
            bin = hit.transform.GetComponent<RecycleBin>();
            currentRecycleBinType = bin.recycleBinType;
        }
        else if(bin)
        {

        }
    }
}
