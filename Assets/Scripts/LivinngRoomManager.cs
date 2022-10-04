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

        //where to put the birthday sign
        if (pick && (choice == "signChoice"))
        {
            Debug.Log("sign0");
            //door
            firstPlacement[0].gameObject.SetActive(true);
   
            
        }
        //wall
        else if (!pick && (choice == "signChoice"))
        {
            Debug.Log("sign1");
            firstPlacement[1].gameObject.SetActive(true);

        }


        //Wheree to put baloons
        if (pick && (choice == "baloonChoice"))
        {
            Debug.Log("baloonn");
            //chair
            secondPlacement[0].gameObject.SetActive(true);
            secondPlacement[1].gameObject.SetActive(true);
         
        }
        else if (!pick && (choice == "baloonChoice"))
        {
            Debug.Log("baloonn");
            secondPlacement[2].gameObject.SetActive(true);
            secondPlacement[3].gameObject.SetActive(true);
         
        }

        if (pick && (choice == "stramerChoice"))
        {
            //TV
            Debug.Log("Streamer");
            thirdPlacement[0].gameObject.SetActive(true);
   
        }
        else if (!pick && (choice == "stramerChoice"))
        {  //table
            Debug.Log("Streamer");
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
        GameObject.Find("Audio Controller").GetComponent<AudioController>().PlayCameraPic();//ff
    }

    public void showPlaces()
    {
        placingObjects.SetActive(true);
    }




}
