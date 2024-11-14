using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Player1Controller : MonoBehaviour
{

    [SerializeField] private GameObject playerOne;
    [SerializeField] private float movementSpeed, rotationSpeed;
    private Vector3 movementDirection;
    public InputActionReference movement;
    public InputActionReference attack;
    public Animator animator;
    public GameObject attackCollide;

    // Start is called before the first frame update
    void Start()
    {
        attackCollide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (attack.action.triggered)
        {
            Debug.Log("Attack");
            animator.SetTrigger("attack");
            //animator.Play("AttackState");
            PlayerAttack();
        }
    }

    void PlayerMovement()
    {
        //animator.SetBool("isWalking", true);
        movementDirection = movement.action.ReadValue<Vector2>();
        playerOne.transform.Translate(Vector3.forward * movementDirection.y * movementSpeed * Time.deltaTime);
        playerOne.transform.Rotate(Vector3.up * movementDirection.x * rotationSpeed * Time.deltaTime);
    }

    void PlayerAttack()
    {
        //animator.SetBool("isAttacking", true);
        Debug.Log("Opening player attack");
        attackCollide.SetActive(true);
    }
}
    