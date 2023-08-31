using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class ObjectUIElement : MonoBehaviour
{
    public float upScaleMultiply = 1.2f;
    public float upScaleTime = 0.5f;

    public float originScaleSize = 1f;

    public GameObject targetGO;

    public enum State
    {
        First,
        Second
    }

    public State state = State.First;

    public Canvas myCanvas;

    private void Start()
    {
        //targetGO = transform.GetChild(0).gameObject;
    }

    void OnMouseDown()
    {
        switch (state)
        {
            case State.First:
                FirstMouseDown();
                break;
            case State.Second:
                SecondMouseDown();
                break;
        }
    }

    float currentTime = 0;
    public float maxTime = 0.5f;

    private void OnMouseDrag()
    {
        if (state == State.First) return;
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            targetGO.transform.position = myCanvas.transform.TransformPoint(pos);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("TrashCan"))
                {
                    
                }
            }
        }
    }

    private void OnMouseUp()
    {
        if (state == State.First) return;

        currentTime = 0;
        targetGO.transform.localPosition = Vector3.zero;
    }

    void SecondMouseDown()
    {
        switch (MousePointerState.instance.MouseStateProperty)
        {
            case MousePointerState.MouseState.Normal:
                print("기본");
                break;
            case MousePointerState.MouseState.Wash:
                print("씻기");
                break;
            case MousePointerState.MouseState.Hand:
                print("손");
                break;
        }
        MousePointerState.instance.MouseStateProperty = MousePointerState.MouseState.Normal;
    }

    void FirstMouseDown()
    {
        targetGO.transform.DOPause();
        state = State.Second;
        SecondUI.instance.TargetGO = this.gameObject;
    }

    void OnMouseEnter()
    {
        print("마우스 들어옴");
        if (state == State.Second) return;

        targetGO.transform.DOScale(targetGO.transform.localScale * upScaleMultiply, upScaleTime);
    }

    void OnMouseExit()
    {
        if (state == State.Second) return;

        targetGO.transform.DOScale(targetGO.transform.localScale / upScaleMultiply, upScaleTime);
    }
}
