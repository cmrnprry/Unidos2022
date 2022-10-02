using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ToggleMenu(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void StartGame()
    {
        AudioController.instance.SetAudio(1);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
