using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerUpHandler
{

    public SpotManager spotManager;
    private PlaceableSpot mySpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData data)
    {
        PlaceableSpot tempSpot = mySpot;
        mySpot = spotManager.placeCharacter(transform);

        if (mySpot)
        {
            if (!mySpot.isFilled || mySpot == tempSpot)
            {
                Vector2 spotPos = mySpot.transform.position;
                gameObject.transform.position = spotPos;

                mySpot.isFilled = true;
            }
        }

        if (mySpot != tempSpot && tempSpot != null)
        {
            tempSpot.isFilled = false;
        }
    }
}
