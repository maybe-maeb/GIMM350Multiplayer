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
    public float attackDuration = 0.15f;

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
            StartCoroutine("PlayerAttack");
        }
    }

    void PlayerMovement()


    {
        //animator.SetBool("isWalking", true);
        movementDirection = movement.action.ReadValue<Vector2>();
        movementDirection = new Vector3(movementDirection.x, 0, movementDirection.y).normalized;
        if(movementDirection.magnitude >= 0.2f)
        {
            Vector3 targetDir = new Vector3(movementDirection.x, 0, movementDirection.z).normalized;
            Vector3 newDir = Vector3.RotateTowards(playerOne.transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            playerOne.transform.rotation = Quaternion.LookRotation(newDir);
            playerOne.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
       /* playerOne.transform.Translate(Vector3.forward * movementDirection.y * movementSpeed * Time.deltaTime);
        playerOne.transform.Rotate(Vector3.up * movementDirection.x * rotationSpeed * Time.deltaTime);*/
    }

    IEnumerator PlayerAttack()
    {
        //animator.SetBool("isAttacking", true);
        Debug.Log("Starting player attack");
        attackCollide.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackCollide.SetActive(false);
        Debug.Log("Ending player attack");
    }

    //private void DisablePlayerMovement()
    //{
      //  animator.enabled = false;

    //}
}
    