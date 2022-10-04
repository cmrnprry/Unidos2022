using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KitchenTool : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public KitchenPoint point;
    public GameObject tempParent;
    public GameObject OrigParent;
    public RectTransform rectTransform;
    public bool canSizzle = false;//ff

    public bool isChopper;
    public Image chopImage;
    public RectTransform chopSpot;

    //okay if we do this then we are hard coding in how we handle dotweens
    //public GameObject sandwichSpot;
    //public GameObject baseLocation;

    public Image ourImage;
    public List<Sprite> nextImages;
    int currentImage;

    public RecipeItem ifWater;

    public bool hasBeans;


    public void Start()
    {
        currentImage = -1;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (KitchenManager.Instance.HasItem())
        {
            if(name == "FryingPan" || name == "SaucePanTop"){//ff
              canSizzle = true;//ff
            }else{
              canSizzle = false;//ff
            }
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

    public void HideChopImage()
    {
        chopImage.raycastTarget = false;
    }
    public void UnhideChopImage()
    {
        chopImage.raycastTarget = true;
    }

    public void NextImage()
    {
        currentImage++;
        //okay so this is bad but fine
        if (point == KitchenPoint.SaucePan)
        {
            if(hasBeans)
            {
                ourImage.sprite = nextImages[currentImage];
            }
            //otherwise we have added cumin before the beans and shouldn't progress images
            //okay i think this is good.
        }
        else
        {
            ourImage.sprite = nextImages[currentImage];
        }
        
    }
}
