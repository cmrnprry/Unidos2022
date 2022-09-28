using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KitchenTool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public KitchenPoint point;
    public GameObject tempParent;
    public GameObject OrigParent;
    public RectTransform rectTransform;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    //on overlap ask if we fulfill recipe step
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (KitchenManager.Instance.HasItem())
        {
            Debug.Log("Cursor Entering " + name + " GameObject");
            KitchenManager.Instance.ChangeLastToolHovered(this);
            transform.SetParent(tempParent.transform);
        }
        //Output to console the GameObject's name and the following message
        
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");

    }
    public void SetOrigParent()
    {

        transform.SetParent(OrigParent.transform);
    }
}
