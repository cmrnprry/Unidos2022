using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LivinngRoomManager : MonoBehaviour
{

    public TextAsset ink;
    private Story story;

    public RectTransform background;
    public Image pictureFlash;
    public List<GameObject> firstPlacement;
    public List<GameObject> secondPlacement;
    public List<GameObject> thirdPlacement;

    public GameObject placingObjects;

    // THis is hard coded im sorry
    // So basically make a variable for the decor question
    // and make a list of the places the player can place the decor
    // then, set the correct one active. 
    public void readVars(string choice, bool pick)
    {
        //bool first = (bool) story.variablesState["FirstDecor"];

        Debug.Log("Value " + pick + " Poop" + choice);


        //where to put the birthday sign
        if (pick && (choice == "signChoice"))
        {
           //door
            firstPlacement[0].gameObject.SetActive(true);
        }
        //wall
        else { firstPlacement[1].gameObject.SetActive(true); }


        //Wheree to put baloons 
        if (pick && (choice == "baloonChoice"))
        {
            //chair
            secondPlacement[0].gameObject.SetActive(true);
            secondPlacement[1].gameObject.SetActive(true);
        }
        else {
            //wall
            secondPlacement[2].gameObject.SetActive(true);
            secondPlacement[3].gameObject.SetActive(true);
        }

        if (pick && (choice == "stramerChoice"))
        {
            //TV
            thirdPlacement[0].gameObject.SetActive(true);
        }
        else{  //table
            thirdPlacement[1].gameObject.SetActive(true);
        }


    }


    public void SnapPicture() {
        pictureFlash.gameObject.SetActive(true);
        Sequence picture = DOTween.Sequence().
            Append(pictureFlash.DOFade(1f, 0.25f).OnComplete(() => pictureFlash.gameObject.SetActive(false))).
            Append(background.DOAnchorPosY(30, 1)).
            Join(background.transform.DOScale
            (new Vector3(0.75f, 0.75f, 0.75f), 1));
        picture.Play();


    }

    public void showPlaces()
    {
        placingObjects.SetActive(true);
    }


   

}
