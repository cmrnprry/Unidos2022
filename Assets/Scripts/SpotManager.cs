using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    public List<PlaceableSpot> spots; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlaceableSpot placeCharacter(Transform character)
    {
        PlaceableSpot closestSpot = null;
        foreach(PlaceableSpot spot in spots)
        {
            if(Vector2.Distance(spot.transform.position, character.position) < 75) { 
                if(closestSpot == null)
                    {
                        closestSpot = spot;
                    }
                if(Vector2.Distance(spot.transform.position,character.position) < Vector2.Distance(closestSpot.transform.position, character.position))
                {
                    closestSpot = spot;
                }
            }
        }
        return closestSpot;

    }
}
