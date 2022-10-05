using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeItem : DraggableImage
{
    public Food current;
    public Food transformed;

    public Vector3 lastPosition;
    public GameObject OrigParent;
    public GameObject choppyBlock;
    public bool isChopping;



    public Image curImage;
    public Sprite transformedSprite;

    //removing on release
    public TextMeshProUGUI tbox;

    public void OnChopped()
    {
        isChopping = false;
        curImage.raycastTarget = true;
        TransformFood();

        transform.SetParent(OrigParent.transform);
        transform.position = lastPosition;
    }

    public override void OnPointerDown(PointerEventData data)
    {
        if (!isChopping)
        {
            base.OnPointerDown(data);
            lastPosition = transform.position;
            KitchenManager.Instance.SetCurrentItem(this);
        }

    }
    public override void OnPointerUp(PointerEventData data)
    {
        if (!isChopping)
        {
            base.OnPointerUp(data);
            OverlapCheck(data);
            KitchenManager.Instance.ResetItem();
        }
    }
    public void OverlapCheck(PointerEventData data)
    {
        KitchenManager.Instance.CheckStep(current);
        if(isChopping)
        {
            this.transform.SetParent(choppyBlock.transform);
        }
        transform.position = lastPosition;
    }

    public void HideFood()
    {
        //okay i guess we could also do something like play a dotween animation for the pot or something?
        lastPosition = KitchenManager.Instance.hideSpot.transform.position;
        transform.position = lastPosition;
    }

    public void TransformFood()
    {
        current = transformed;
        curImage.sprite = transformedSprite;
    }

    public void StartChop()
    {
        curImage.raycastTarget = false;
        isChopping = true;
    }
}
