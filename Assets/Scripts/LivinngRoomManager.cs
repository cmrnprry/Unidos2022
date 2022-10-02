using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class LivinngRoomManager : MonoBehaviour
{

    public TextAsset ink;
    private Story story;

    public List<GameObject> firstPlacement;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(ink.text);
        
    }

    // THis is hard coded im sorry
    // So basically make a variable for the decor question
    // and make a list of the places the player can place the decor
    // then, set the correct one active. 
    void readVars()
    {
        bool first = (bool) story.variablesState["FirstDecor"];

        if (first)
        {
            firstPlacement[0].gameObject.SetActive(true);
        }
        else { firstPlacement[1].gameObject.SetActive(true); }


    }

}
