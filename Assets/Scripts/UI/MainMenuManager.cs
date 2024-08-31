using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string mainMenuScene;
    [SerializeField]
    private string creditsScene;
    public void StartNewGame()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void ResumeGame()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GameCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }
}
