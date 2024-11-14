using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    //public GameObject player; dk how to change this one tbh
    [Header("Enemy, Players, and Distance")]
    [Tooltip("Drag the Enemy prefab here")]
    public NavMeshAgent enemy;
    public EnemyState currentState;

    private GameObject player1;
    private GameObject player2;
    private GameObject[] players;

    [Tooltip("Ignore Me")]
    public Transform nearestPlayer;

    [Tooltip("How far away the players must be before enemies stop chasing and fighting")]
    public float maxPlayerDistance;

    [Header("Shooting and Reloading")]
    [Tooltip("Fireball prefab goes here")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("Enemy's LaunchPoint goes here")]
    [SerializeField] private GameObject bulletLaunchPoint;

    [Tooltip("How quickly the bullet moves")]
    [SerializeField] private float bulletForce;

    [Tooltip("Ignore Me")]
    public bool reloading;
    [Tooltip("How long will enemies wait between shooting fireballs")]
    public float reloadTime;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        players = new GameObject[] { player1, player2 };
        SetState(new EnemyIdle(this));

        reloading = false;
    }

    void Update()
    {
        if (player1 != null || player2 != null)
        {
            currentState.CheckTransitions();
            currentState.Act();
        }
    }

    public void SetState(EnemyState es)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = es;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void FindNearestPlayer() 
    { 
        nearestPlayer = null; 
        float closestDistanceSqr = Mathf.Infinity; 

        foreach (var player in players) 
        { 
            if (player != null) 
            { 
                float distanceSqr = (transform.position - player.transform.position).sqrMagnitude; 
                if (distanceSqr < closestDistanceSqr) 
                { 
                    closestDistanceSqr = distanceSqr; 
                    nearestPlayer = player.transform; 
                } 
            }  
        }
    } 

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.SetActive(true);
        bullet.transform.position = bulletLaunchPoint.transform.position;
        bullet.transform.rotation = bulletLaunchPoint.transform.rotation;

        bullet.GetComponent<Rigidbody>().AddForce(bulletLaunchPoint.transform.forward * bulletForce, ForceMode.Impulse);
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        reloading = true;
        yield return new WaitForSecondsRealtime(reloadTime);
        reloading = false;
    } 
}