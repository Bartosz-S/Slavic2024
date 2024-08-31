using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string mainGameScene;
    [SerializeField]
    private string creditsScene;
    public void StartNewGame()
    {
        SceneManager.LoadScene(mainGameScene);
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
