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

        if (p2attack.action.triggered)
        {
            Debug.Log("Attack");
            animator.SetTrigger("attack");
            //animator.Play("AttackState");
            PlayerAttack();
        }
    }

    void PlayerMovement()
    {
        movementDirection = p2movement.action.ReadValue<Vector2>();
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.y).normalized;
        if (movementDirection.magnitude >= 0.2f)
        {
            Vector3 targetDir = new Vector3(movementDirection.x, 0, movementDirection.z).normalized;
            Vector3 newDir = Vector3.RotateTowards(playerTwo.transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            playerTwo.transform.rotation = Quaternion.LookRotation(newDir);
            playerTwo.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }

    void PlayerAttack()
    {
        //animator.SetBool("isAttacking", true);
        Debug.Log("Opening player attack");
        attackCollide.SetActive(true);
    }
}
