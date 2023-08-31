using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public RecycleObject box;
    public RecycleObject boxFlip;

    public Canvas myCanvas;

    public bool hand = false;

    public bool Hand { get { return hand; } set { hand = value; CheckClear(); } }

    void CheckClear()
    {
        if (Hand)
        {
            print("박스 완성");
            BoxType = Type.Recycle;
        }
    }

    public enum Type
    {
        Normal,
        Recycle
    }

    public Type type = Type.Normal;

    public Type BoxType
    {
        get { return type; }
        set
        {
            if (value == Type.Recycle)
            {
                box.gameObject.SetActive(false);
                boxFlip.gameObject.SetActive(true);
            }
            type = value;
        }
    }

    private void Start()
    {
        myCanvas = SelectManager.instance.transform.GetComponent<Canvas>();

        box.onMouseDown += SelectObj;
        boxFlip.onMouseDown += SelectObj;

        box.onMouseDrag += OnDrag;
        boxFlip.onMouseDrag += OnDrag;

        box.onMouseUp += OnUp;
        boxFlip.onMouseUp += OnUp;
    }

    float currentTime = 0;
    public float maxTime = 0.5f;

    void OnDrag()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
            SelectManager.instance.interactionUI.SetActive(false);
            SelectManager.instance.checkRecycleBin = true;
        }
    }

    void OnUp()
    {
        currentTime = 0;
        transform.localPosition = Vector3.zero;
        SelectManager.instance.interactionUI.SetActive(true);
        SelectManager.instance.checkRecycleBin = false;
    }

    void SelectObj()
    {
        if (SelectManager.instance.isSelect)
        {
            if (SelectManager.instance.selectObj != this.gameObject) return;

            switch (MousePointerState.instance.MouseStateProperty)
            {
                case MousePointerState.MouseState.Hand:
                    print("병 뜯기");
                    Hand = true;
                    break;
            }
        }
        else
        {
            SelectManager.instance.OnInteractionUI(this.gameObject);
        }
    }

}
