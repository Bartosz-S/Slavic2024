using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    public void GoBackToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
