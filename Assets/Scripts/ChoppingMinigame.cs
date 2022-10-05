using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ChoppingMinigame : MonoBehaviour, IPointerDownHandler
{
    public bool startMinigame;
    public bool minigameDone;
    public bool chopping;
    public UIParticle particles;

    public int chopsLeft;
    public int maxChops;

    public Dictionary<Food, int> chopsLeftPerFood;

    void Start()
    {
        maxChops = 10;
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        chopping = true;
    }


    void Update()
    {
        if(!minigameDone)
        {
            if (startMinigame)
            {
                if (chopping)
                {
                    particles.Play();
                    KitchenManager.Instance.OnChop();
                    chopsLeft -= 1;
                    chopping = false;
                }
            }
            if (chopsLeft <= 0)
            {
                minigameDone = true;
                KitchenManager.Instance.ChopDone();
            }
        }
       
    }

    public void StartMinigame()
    {
        minigameDone = false;
        startMinigame = true;
        chopsLeft = maxChops;
    }

    
}
