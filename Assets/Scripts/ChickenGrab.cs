using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChickenGrab : DraggableImage
{
    public GameObject cube;
    public RecipeItem recCube;

    public override void OnPointerDown(PointerEventData data)
    {
        recCube.gameObject.SetActive(true);
        recCube.OnPointerDown(data);
    }
    public override void OnPointerUp(PointerEventData data)
    {
        recCube.OnPointerUp(data);
        recCube.gameObject.SetActive(false);
    }
}
