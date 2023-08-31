using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    public RecycleObject cover;
    public RecycleObject bottle;
    public RecycleObject crushedBottle;

    public Canvas myCanvas;

    public bool wash = false;
    public bool hand = false;

    public bool Wash { get { return wash; } set { wash = value; CheckClear(); } }
    public bool Hand { get { return hand; } set { hand = value; CheckClear(); } }

    void CheckClear()
    {
        if(Wash && Hand)
        {
            print("병 완성");
            BottleType = Type.Recycle;
        }
    }

    public enum Type
    {
        Normal,
        Recycle
    }

    public Type type = Type.Normal;

    public Type BottleType { get { return type; } 
        set {
            if (value == Type.Recycle)
            {
                cover.gameObject.SetActive(false);
                bottle.gameObject.SetActive(false);
                crushedBottle.gameObject.SetActive(true);
            }
            type = value; 
        } 
    }

    void Start()
    {
        myCanvas = SelectManager.instance.transform.GetComponent<Canvas>();

        cover.onMouseDown += SelectObj;
        bottle.onMouseDown += SelectObj;

        cover.onMouseDrag += OnDrag;
        bottle.onMouseDrag += OnDrag;
        crushedBottle.onMouseDrag += OnDrag;

        cover.onMouseUp += OnUp;
        bottle.onMouseUp += OnUp;
        crushedBottle.onMouseUp += OnUp;
    }

    float currentTime = 0;
    public float maxTime = 0.5f;

    void OnDrag()
    {
        currentTime += Time.deltaTime;
        if(currentTime > maxTime)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
            SelectManager.instance.interactionUI.SetActive(false);
            SelectManager.instance.checkRecycleBin = true;
        }
    }

    public bool onTrash = false;

    void OnUp()
    {
        if (SelectManager.instance.isSelect)
        {
            if (SelectManager.instance.selectObj != this.gameObject) return;

            if (SelectManager.instance.rayHit)
            {
                Destroy(this.gameObject);
                SelectManager.instance.OffInteractionUI();
                if(SelectManager.instance.checkRecycleBin)
                {
                    print("분리수거 성공");
                }
                else
                {
                    print("분리수거 실패");
                }
            }
        }

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
                case MousePointerState.MouseState.Normal:
                    break;
                case MousePointerState.MouseState.Wash:
                    print("병 씻기");
                    Wash = true;
                    break;
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
