using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputActionReference select;

    public void Update()
    {
        if (select.action.triggered)
        {
            Debug.Log("Returning to menu");
            ReturnToMenu();
           
        }
    }

    

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
