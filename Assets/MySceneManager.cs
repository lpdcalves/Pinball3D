using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PinBallGame");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
