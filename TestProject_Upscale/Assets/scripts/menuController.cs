using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void menuGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void playGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void settingsGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void loadGame()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void creditsGame()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
