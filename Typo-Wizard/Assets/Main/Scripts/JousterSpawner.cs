using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JousterSpawner : MonoBehaviour
{
    public GameObject jouster1; 
    public GameObject jouster2;
    
    public bool isSpawned1 = false;
    public float spawnInterval = 2;
    private float savedTime;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        savedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - savedTime >= spawnInterval) // is it time to spawn again?
        {
            Spawn();
            savedTime = Time.time; // store for next spawn
        }
    }
    
    void Spawn()
    {
        GameObject spawnPrefab;
        if (isSpawned1)
        {
            spawnPrefab = jouster2;            
        } else
        {
            spawnPrefab = jouster1;
        }

        isSpawned1 = !isSpawned1;
        // create a new gameObject
        GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;
    }
}
