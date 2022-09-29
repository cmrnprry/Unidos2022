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
    public GameObject choppyBlock;
    public bool isChopping;

    public Image curImage;
    public TextMeshProUGUI tbox;
    public void OnChopped()
    {
        current = transformed;
        isChopping = false;

        //set image to new image
        curImage.color = Color.green;
        tbox.text = transformed.ToString();
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
        transform.position = lastPosition;
        if(isChopping)
        {
            //set us to our block
            this.transform.SetParent(choppyBlock.transform);
        }
    }


}
