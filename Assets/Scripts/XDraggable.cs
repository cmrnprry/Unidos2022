using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;


public class XDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    public RectTransform rectTransform;
    private float halfWidth;
    private float halfHeight;

    private float setY;
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        halfWidth = rectTransform.rect.width / 2;
        halfHeight = rectTransform.rect.height / 2;
        Debug.Log(halfWidth);

    }
    public virtual void OnPointerDown(PointerEventData data)
    {
        _dragging = true;
        setY = gameObject.transform.position.y; 
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
        _dragging = false;
        //Better to do this once, rather than every frame
        ScreenSpaceCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging == true)
            transform.position = new Vector2(Input.mousePosition.x, setY);
    }

    //Okay this should work, but I also think we should 
    void ScreenSpaceCheck()
    {
        var tempPos = transform.position;
        //if our position is off the screen put us back in
        if (tempPos.x < 0) tempPos.x = 0;
        //if (tempPos.y - halfHeight < 0) tempPos.y = 0 + halfHeight;
        if (tempPos.x > Screen.width) tempPos.x = Screen.width;
      //  if (tempPos.y + halfHeight > Screen.height) tempPos.y = Screen.height - halfHeight;

        transform.position = tempPos;
    }
}
