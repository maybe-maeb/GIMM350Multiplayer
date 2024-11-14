using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    private float moveSpeed = 5f;
    private Vector3 movementDirection;
    public InputActionReference movementP1;
    public InputActionReference movementP2;
    public InputActionAsset player2IAA;

    private float rotationSpeed = 25f;
    private Vector3 lookDirection;
    public InputActionReference lookP1;
    public InputActionReference lookP2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * movementDirection.y * moveSpeed * Time.deltaTime);
        this.gameObject.transform.Translate(Vector3.right * movementDirection.x * moveSpeed * Time.deltaTime);

        this.gameObject.transform.Rotate(Vector3.up * lookDirection.x * rotationSpeed * Time.deltaTime);
    }
}
