using UnityEngine;
using UnityEngine.EventSystems;

public class IPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ÆË¾÷Ã¢ 
    public GameObject image;


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        image.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        image.SetActive(false);
    }
}
