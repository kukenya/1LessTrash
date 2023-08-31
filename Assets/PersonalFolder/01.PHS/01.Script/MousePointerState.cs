using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerState : MonoBehaviour
{
    public static MousePointerState instance;

    private void Awake()
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

    public enum MouseState
    {
        Normal,
        Wash,
        Hand
    }

    MouseState mouseState = MouseState.Normal;

    public MouseState MouseStateProperty
    {
        get
        {
            return mouseState;
        }

        set
        {
            if(mouseState == value)
            {
                mouseState = MouseState.Normal;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }
            mouseState = value;
            SwitchMouseCursor();
        }
    }

    public Texture2D washCursor;
    public Texture2D scissorsCursor;
    public Texture2D handCursor;

    void SwitchMouseCursor()
    {
        switch (MouseStateProperty)
        {
            case MouseState.Normal:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
            case MouseState.Wash:
                Cursor.SetCursor(washCursor, Vector2.zero, CursorMode.Auto);
                break;
            case MouseState.Hand:
                Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void OnClickWash()
    {
        MouseStateProperty = MouseState.Wash;
    }

    public void OnClickHand()
    {
        MouseStateProperty = MouseState.Hand;
    }
}
