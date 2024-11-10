using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player2Controller : MonoBehaviour
{

    [SerializeField] private GameObject playerTwo;
    [SerializeField] private float movementSpeed, rotationSpeed;
    private Vector3 movementDirection;
    public InputActionReference p2movement;
    public InputActionReference p2attack;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }
    


    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (p2attack.action.triggered)
        {
            Debug.Log("Attack");
            PlayerAttack();
        }
    }

    void PlayerMovement()
    {
        movementDirection = p2movement.action.ReadValue<Vector2>();
        playerTwo.transform.Translate(Vector3.forward * movementDirection.y * movementSpeed * Time.deltaTime);
        playerTwo.transform.Rotate(Vector3.up * movementDirection.x * rotationSpeed * Time.deltaTime);
    }

    void PlayerAttack()
    {
        animator.SetBool("isAttacking", true);
    }
}
