using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenCabinet : MonoBehaviour, IPointerDownHandler
{
    public GameObject Opened;
    public GameObject Closed;
    public Image ourImage;
    public bool isOpened;
    // Start is called before the first frame update
    public void Start()
    {
        ourImage = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Pointer down");
        //Swap to open
        //disable until dialogue calls back into kitchen?
        if (!isOpened)
        {
            OpenUp();
        }
    }

    public void ResetCabinet()
    {
        Closed.SetActive(true);
        Opened.SetActive(false);
        isOpened = false;
        ourImage.raycastTarget = true;
    }
    
    public void OpenUp()
    {
        Closed.SetActive(false);
        Opened.SetActive(true);
        isOpened = true;
        ourImage.raycastTarget = false;
    }

}
