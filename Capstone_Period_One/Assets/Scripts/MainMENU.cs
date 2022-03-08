using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMENU : MonoBehaviour
{
    public GameObject continueButton;
    public string loadGameScene;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("You exited the game!");
    }
}
