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
    private float fontSize = 36;
    private Story story;
    private List<string> currentTags = new List<string>();

    private Coroutine coroutine;

    [Header("TextBoxes")]
    public TextMeshProUGUI textbox;
    public GameObject friend;

    [Header("Dialogue")]
    private bool isTyping;
    private string effect;
    private float value;
    private Vector3 original;

    private void Start()
    {
        story = new Story(ink.text);
        coroutine = StartCoroutine(ShowNextLine());
    }


    private IEnumerator ShowNextLine()
    {
        if (story.canContinue)
        {
            string nextLine = story.Continue().Trim();
            currentTags = story.currentTags;

            //if (currentTags.Count > 0)
            //{
            //    CheckTags();
            //}


            coroutine = StartCoroutine(incrementText(nextLine, textbox));
        }
        else
        {
            //end of game or choice
        }

        yield return null;

    }

    //private void CheckTags()
    //{
    //    foreach (string tag in currentTags)
    //    {
    //        string[] split = tag.Split(',', StringSplitOptions.RemoveEmptyEntries);
    //        string effect = split[0];
    //        float v = (split.Length > 1) ? float.Parse(split[1]) : -1;
    //        switch (effect)
    //        {
    //            case "speed":
    //                ChangeTextSpeed(v);
    //                break;

    //            case "size":
    //                ChangeTextSize(v, textbox);
    //                break;

    //            case "increasing size":
    //                this.effect = "size";
    //                value = v;
    //                break;

    //            default:
    //                Debug.LogError($"Text Effect Not Found: {effect}");
    //                break;
    //        }


    //    }
    //}

    //private void ChangeTextSpeed(float value)
    //{
    //    if (value < 0)
    //    {
    //        wait = 0.15f;
    //    }
    //    else
    //    {
    //        wait = value;
    //    }
    //}

    //private void ChangeTextSize(float value, TMP_Text currentTextbox)
    //{
    //    fontSize = currentTextbox.fontSize;
    //    currentTextbox.fontSize *= value;
    //}

    //private void IncreasingTextSize(float end, TMP_Text currentTextbox, string text, TMP_TextInfo info)
    //{
    //    float start = currentTextbox.fontSize;
    //    float difference = Mathf.Abs(end - start);

    //    char[] characters = text.ToCharArray();
    //    int index = 0;

    //    TMP_MeshInfo[] cachedMeshInfo = info.CopyMeshInfoVertexData();
    //    foreach (char c in characters)
    //    {
    //        if (Char.IsWhiteSpace(c))
    //        {
    //            continue;
    //        }

    //        int materialIndex = info.characterInfo[index].materialReferenceIndex;
    //        Vector3[] destinationVertices = info.meshInfo[materialIndex].vertices;
    //        Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;
    //        int vertexIndex = info.characterInfo[index].vertexIndex;


    //        float charSize =  Mathf.Max(0.375f, index / (float)characters.Length);
    //        print(index / (float)characters.Length);
    //        Vector3 offset = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;
    //        destinationVertices[vertexIndex + 0] = ((sourceVertices[vertexIndex + 0] - offset) * charSize) + offset;
    //        destinationVertices[vertexIndex + 1] = ((sourceVertices[vertexIndex + 1] - offset) * charSize) + offset;
    //        destinationVertices[vertexIndex + 2] = ((sourceVertices[vertexIndex + 2] - offset) * charSize) + offset;
    //        destinationVertices[vertexIndex + 3] = ((sourceVertices[vertexIndex + 3] - offset) * charSize) + offset;

    //        index++;
    //    }


    //    for (int i = 0; i < info.meshInfo.Length; i++)
    //    {
    //        TMP_MeshInfo theInfo = info.meshInfo[i];
    //        theInfo.mesh.vertices = theInfo.vertices;
    //        currentTextbox.UpdateGeometry(theInfo.mesh, i);
    //    }

    //}

    //private void RestEffects(TMP_Text currentTextbox)
    //{
    //    wait = 0.15f;
    //    currentTextbox.fontSize = fontSize;

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
