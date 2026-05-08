using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void MainMenuLoad()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameSceneLoad()
    {
        SceneManager.LoadScene("Game");
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
