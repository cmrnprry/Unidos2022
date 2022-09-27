using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;


public class DraggableImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    private RectTransform rectTransform;
    private float halfWidth;
    private float halfHeight;
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        halfWidth = rectTransform.rect.width / 2;
        halfHeight = rectTransform.rect.height / 2;

    }
    public void OnPointerDown(PointerEventData data)
    {
        _dragging = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        _dragging = false;
        //Better to do this once, rather than every frame
        ScreenSpaceCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging == true)
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    //Okay this should work, but I also think we should 
    void ScreenSpaceCheck()
    {
        var tempPos = transform.position;
        //if our position is off the screen put us back in
        if (tempPos.x-halfWidth < 0) tempPos.x = 0 + halfWidth;
        if (tempPos.y-halfHeight < 0) tempPos.y = 0 + halfHeight; 
        if (tempPos.x+halfWidth > Screen.width) tempPos.x = Screen.width-halfWidth;
        if (tempPos.y+halfHeight > Screen.height) tempPos.y = Screen.height-halfHeight;

        transform.position = tempPos;
    }
}
