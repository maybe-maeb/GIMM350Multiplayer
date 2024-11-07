using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //Keeps track of what players are in the teleporter
    private bool p1In;
    private bool p2In;

    //The camera gameobject
    private Transform cam;

    //Has the teleport started?
    private bool hasTeleported;

    [Header("Camera movement")]
    //How far the camera should move to the right.
    [SerializeField] private float moveIncrement = -20;
    //How long it should take the camera to move
    [SerializeField] private float movementDuration = 1;
    //Keeps track of whether the lerp for movement has started or not
    private bool beganMovement;
    //The starting position
    private Vector3 movementStartPos;
    //The ending position
    private Vector3 movementEndPos;
    //The progress of the movement lerp
    private float lerpProgress;

    [Header("Rooms")]
    [SerializeField] private GameObject nextRoom;
    private GameObject currentRoom;

    [Header("Particles")]
    //The particles shown before players are teleported
    [SerializeField] private GameObject teleportLocationWarmupParticles;
    //How long the warmup particles should be shown before the player is teleported
    [SerializeField] private float warmupTime;
    //The particles shown when the players are teleported
    [SerializeField] private GameObject finishTeleportParticles;

    void Start(){
        //Get a reference to the camera so we can move it later
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;

        //Teleporters should be a child of the room gameobject, so this just gets that parent object
        currentRoom = transform.parent.gameObject;
    }

    void Update()
    {
        //If both players are in the teleporter, call StartTeleport();
        if (p1In && p2In) StartTeleport();

        //If the camera has started moving...
        if (beganMovement){
            //Move the camera to its new position over movementDuration seconds
            if (lerpProgress / movementDuration < 1) {
                cam.transform.position = Vector3.Lerp(movementStartPos, movementEndPos, lerpProgress / movementDuration);
                lerpProgress += Time.deltaTime;
		    }

            //This checks if the lerp is complate. If it is, it teleports the camera to the final position (in case something went wrong), 
            //and sets the bool which activates the lerp to false. At the end, it calls FinishTeleport which actually moves the player and such
            if (lerpProgress / movementDuration > 1){
                cam.transform.position = movementEndPos;
                beganMovement = false;
                StartCoroutine("FinishTeleport");
            }
        }
    }

    private IEnumerator FinishTeleport(){
        //Get the spawn positions in the next room
        List<Transform> spawnPositions = new List<Transform>();
        foreach(Transform child in nextRoom.GetComponentsInChildren<Transform>()){
            if (child.CompareTag("Rooms/PlayerSpawnPosition")){
                spawnPositions.Add(child);
            }
        }

        //Throw an error if there aren't enough spawn positions
        if (spawnPositions.Count < 2) Debug.LogError("Only one spawn position in next room!");

        //Get a reference to the players
        GameObject p1 = GameObject.FindGameObjectWithTag("Players/Player1");
        GameObject p2 = GameObject.FindGameObjectWithTag("Players/Player2");

        //Creates some "warmup" particles that are shown before the players actually teleport
        foreach(Transform pos in spawnPositions){
            Vector3 particlesLocation = new Vector3(pos.position.x - 1, pos.position.y, pos.position.z);
            GameObject warmupParticles = Instantiate(teleportLocationWarmupParticles, particlesLocation, Quaternion.identity);
            //Destroy these particles after warmupTime seconds
            Destroy(warmupParticles, warmupTime);
        }

        //Wait warmupTime seconds
        yield return new WaitForSeconds(warmupTime);

        //Create the particles when the players are moved
        foreach(Transform pos in spawnPositions){
            Vector3 particlesLocation = new Vector3(pos.position.x - 1, pos.position.y, pos.position.z);
            GameObject teleportParticles = Instantiate(finishTeleportParticles, particlesLocation, Quaternion.identity);
            //Destroy them after a second so they don't clog the hierarchy
            Destroy(teleportParticles, 1f);
        }

        //Actually move the players
        p1.transform.position = spawnPositions[0].position;
        p2.transform.position = spawnPositions[1].position;

        //This line disables the room the players came from to save performance
        //This line must be the last line in the function
        currentRoom.SetActive(false);

        nextRoom.GetComponent<Room>().StartSpawning();
    }

    private void StartTeleport(){
        //If the players have already teleported, they won't teleport again
        if (hasTeleported) return;

        Debug.Log("Both players are in the teleporter. Teleporting...");

        //Figure out where the camera is now and where it should be after it's done moving
        movementStartPos = cam.position;
        movementEndPos = new Vector3(cam.position.x + moveIncrement, cam.position.y, cam.position.z);

        //Tell Update() to start moving the camera
        beganMovement = true;

        //Prevent StartTeleport() from being called again
        hasTeleported = true;
    }

    //Keeps track of who is in the teleporter
    public void OnTriggerEnter(Collider col){
        if (col.transform.gameObject.CompareTag("Players/Player1")) p1In = true;
        else if (col.transform.gameObject.CompareTag("Players/Player2")) p2In = true;
    }

    //Keeps track of who is in the teleporter
    public void OnTriggerExit(Collider col){
        if (col.transform.gameObject.CompareTag("Players/Player1")) p1In = false;
        else if (col.transform.gameObject.CompareTag("Players/Player2")) p2In = false;
    }
}
