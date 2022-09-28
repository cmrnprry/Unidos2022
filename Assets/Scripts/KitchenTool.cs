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

    public bool isChopper;
    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (KitchenManager.Instance.HasItem())
        {
            Debug.Log("Cursor Entering " + name + " GameObject");
            KitchenManager.Instance.ChangeLastToolHovered(this);
            transform.SetParent(tempParent.transform);
        }
        
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Cursor Exiting " + name + " GameObject");

    }
    public void SetOrigParent()
    {
        transform.SetParent(OrigParent.transform);
    }
}
