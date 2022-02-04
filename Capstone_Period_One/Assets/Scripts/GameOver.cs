using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenuScene;
    public string loadGameScene;

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastGame()
    {
        SceneManager.LoadScene(loadGameScene);
    }
}
