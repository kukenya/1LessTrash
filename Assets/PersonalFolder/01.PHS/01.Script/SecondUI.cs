using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondUI : MonoBehaviour
{
    public static SecondUI instance;

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

    GameObject targetGO;
    
    public GameObject TargetGO
    {
        get
        {
            return targetGO;
        }

        set
        {
            targetGO = value;
            targetGO.transform.SetParent(placeHolder);
            targetGO.transform.localPosition = Vector3.zero;
            targetGO.transform.localScale = targetGO.transform.localScale * scaleMultiply;
        }
    }

    public float scaleMultiply = 1.5f;

    public Transform placeHolder;
}
