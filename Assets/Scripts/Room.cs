using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject enemy;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public bool roomStarted;
    public GameObject teleporter;
    public bool isFirstRoom;
    public bool isLastRoom;
    public int enemySpawnCount = 5;
    
    void Start()
    {
        teleporter.SetActive(false);
        if (isFirstRoom) StartSpawning();
    }

    public void StartSpawning(){
        for (int i = 0; i < enemySpawnCount; i++){
            int r = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[r];
            GameObject spawnedEnemy = Instantiate(enemy, spawnPoint);
            spawnedEnemies.Add(spawnedEnemy);
            spawnedEnemy.transform.parent = null;
            spawnPoints.Remove(spawnPoints[r]);
        }

        roomStarted = true;
    }

    void Update()
    {
        if (roomStarted){
            bool allEnemiesDead = true;
            foreach (GameObject go in spawnedEnemies){
                if (go != null) allEnemiesDead = false;
            }

            if (allEnemiesDead){
                FinishRoom();
            }
        }
    }

    void FinishRoom(){
        if (isLastRoom) Debug.Log("Game win! Put game win logic here.");
        else {
            teleporter.SetActive(true);
        }
        roomStarted = false;
    }
}
