using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoppingMinigame : MonoBehaviour
{
    public bool startMinigame;
    public bool minigameDone;
    public UIParticle particles;

    public int chopsLeft;
    

    void Start()
    {
        
    }

    void Update()
    {
        if(!minigameDone)
        {
            if (startMinigame)
            {
                if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
                {
                    particles.Play();
                    chopsLeft -= 1;
                }
            }
            if (chopsLeft <= 0)
            {
                minigameDone = true;
                KitchenManager.Instance.ChopDone();
            }
        }
       
    }

    
}
