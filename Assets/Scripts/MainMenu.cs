using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public InputActionReference select;

    public InputActionReference quit;


    void Update()
    {
        if (select.action.triggered)
        {
            Debug.Log("Starting game");
            PlayGame();
        }

        if (quit.action.triggered)
        {
            Debug.Log("Quittng game");
            QuitGame();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
