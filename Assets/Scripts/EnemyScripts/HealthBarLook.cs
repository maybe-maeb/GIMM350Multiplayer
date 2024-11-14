using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLook : MonoBehaviour
{
    [Tooltip("Put the main camera here")]
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.Log("Main Camera not found.");
        }
    }

    void Update()
    {
        if (mainCam != null)
        {
            transform.LookAt(mainCam.transform.position); 
        }

    }
}