using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartOver : MonoBehaviour
{
    public InputActionReference select;
   

    // Update is called once per frame
    void Update()
    {
        if (select.action.triggered)
        {
            RestartLevel();
        }
    }
    public void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
