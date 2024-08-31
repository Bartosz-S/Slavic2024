using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string m_SceneName;
    public void StartNewGame()
    {
        SceneManager.LoadScene(m_SceneName);
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

    }
}
