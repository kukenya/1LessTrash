using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    public RecycleBin.RecycleBinType recycleObjectType = RecycleBin.RecycleBinType.Paper;

    public Action onMouseEnter;
    public Action onMouseExit;
    public Action onMouseDrag;
    public Action onMouseDown;
    public Action onMouseUp;

    public Canvas myCanvas;

    private void Start()
    {
        myCanvas = SelectManager.instance.transform.GetComponent<Canvas>();
    }

    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }

    private void OnMouseDrag()
    {
        //onMouseDrag?.Invoke();
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    private void OnMouseDown()
    {
        onMouseDown?.Invoke();
    }

    private void OnMouseUp()
    {
        onMouseUp?.Invoke();
    }

}
