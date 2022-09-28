using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeItem : DraggableImage
{
    public Food current;
    public Food? transformed;

    public Vector3 lastPosition;
    public GameObject choppyBlock;
    public bool isChopping;
    public void OnChopped()
    {
        current = transformed.Value;
        //set image to new image
    }
    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);
        lastPosition = transform.position;
        KitchenManager.Instance.SetCurrentItem(this);
    }
    public override void OnPointerUp(PointerEventData data)
    {
        base.OnPointerUp(data);
        OverlapCheck(data);
        KitchenManager.Instance.ResetItem();
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
