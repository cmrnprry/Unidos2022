using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class DialogueController : MonoBehaviour
{
    /*want to control text:
     * speed
     * size1 
     * position
    */

    /*want to ocntrol text box:
     * position
     * movement
     */
    [Header("Ink")]
    public TextAsset ink;
    public float wait = 0.15f;
    private Story story;
    private List<string> currentTags = new List<string>();

    private Coroutine coroutine;

    [Header("TextBoxes")]
    public TextMeshProUGUI textbox;


    [Header("Dialogue")]
    private bool isTyping;
    public bool WaitFor;
    public Sprite[] heads;
    public GameObject exclaimation;
    private string side = "";

    [Header("Scenes")]
    public GameObject junk;
    public GameObject living;
    public GameObject kitchen;

    //make this a singleton

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        story = new Story(ink.text);
        coroutine = StartCoroutine(ShowNextLine());
    }

    private IEnumerator ShowNextLine()
    {
        if (WaitFor)
            yield return new WaitUntil(() => !WaitFor);

        if (exclaimation.activeSelf)
        {
            //transition logic
            //hide
            exclaimation.SetActive(false);
            //show
        }

        if (story.canContinue)
        {
            string nextLine = story.Continue().Trim();
            currentTags = story.currentTags;

            if (currentTags.Count > 0)
            {
                CheckTags();
            }

            coroutine = StartCoroutine(incrementText(nextLine, textbox));
        }
        else
        {
            //end of game or choice
        }

        yield return null;

    }

    private void CheckTags()
    {
        foreach (string tag in currentTags)
        {
            tag.ToLower().Replace(" ", "");
            string[] split = tag.Split(':', StringSplitOptions.RemoveEmptyEntries);
            string TAG = split[0];
            switch (TAG)
            {
                case "speaker":
                    string character = split[1];
                    break;

                case "prompt":
                    side = split[1];
                    //set the side
                    exclaimation.SetActive(true);
                    WaitFor = true;
                    break;

                case "WaitUntil":
                    WaitFor = true;
                    break;

                default:
                    Debug.LogError($"Tag not found: {TAG}");
                    break;
            }
        }
    }

    public void SetBool(bool value)
    {
        WaitFor = value;
    }

    public IEnumerator incrementText(string text, TMP_Text currentTextbox)
    {
        //set the text
        isTyping = true;
        currentTextbox.alpha = 0;
        currentTextbox.text = text;

        yield return new WaitUntil(() => currentTextbox.gameObject.activeSelf);
        yield return new WaitForSeconds(0.15f);

        currentTextbox.ForceMeshUpdate();
        TMP_TextInfo textInfo = currentTextbox.textInfo;
        int totalCharacters = currentTextbox.textInfo.characterCount;

        for (int i = 0; i < totalCharacters; i++)
        {
            if (currentTextbox == null)
            {
                break;
            }

            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            TMP_MeshInfo meshinfo = textInfo.meshInfo[materialIndex];

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            var newVertexColors = meshinfo.colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Set all to full alpha
            newVertexColors[vertexIndex + 0].a = 255;
            newVertexColors[vertexIndex + 1].a = 255;
            newVertexColors[vertexIndex + 2].a = 255;
            newVertexColors[vertexIndex + 3].a = 255;

            currentTextbox.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            yield return new WaitForSeconds(wait);
        }

        //currentTextbox.alpha = 225;
        isTyping = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => Input.GetButtonDown("Continue"));
        coroutine = StartCoroutine(ShowNextLine());
    }
}
