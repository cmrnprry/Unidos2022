using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceableSpot : MonoBehaviour
{
    public bool isFilled;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFilled)
        {
            gameObject.GetComponent<Image>().color = Color.green;

        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
