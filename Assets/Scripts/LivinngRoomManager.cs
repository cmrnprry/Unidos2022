using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class LivinngRoomManager : MonoBehaviour
{

    public TextAsset ink;
    private Story story;
    public TextMeshProUGUI textbox;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(ink.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
