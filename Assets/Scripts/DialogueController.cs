using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
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


    private IEnumerator ShowNextLine()
    {
        string nextLine = GetNextStoryBlock();
        nextLine = nextLine.Trim();
        currentTags = story.currentTags;
        currentChoices = story.currentChoices;

        if (currentTags.Contains("friend"))
        {
            other.SetActive(false);
            friend.SetActive(true);
            friendTextBox.text = "";

            yield return StartCoroutine(DisplayWords(nextLine, friendTextBox));
        }
        if (currentTags.Contains("comment"))
        {
            SetChoiceText(nextLine, false);
        }
        if (currentTags.Contains("other"))
        {
            friend.SetActive(false);
            other.SetActive(true);
            otherTextBox.text = "";
            yield return StartCoroutine(DisplayWords(nextLine, otherTextBox));
        }
        yield return StartCoroutine(HandleAVTags());
        if (nextLine == "")
        {
            makingChoice = true;
            CheckChoices();

            friend.SetActive(false);
            other.SetActive(false);
        }

        yield return null;
        showNextLineRoutine = null;
    }



}
