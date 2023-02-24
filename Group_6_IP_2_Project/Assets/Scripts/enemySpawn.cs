using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    float movementTimer = 0f; // initalises the movemnet timer, setting it to 0
    float spawnTimer = 0f; // initalises the spawn timmer, setting it to 0
    public GameObject[] enemys; // array of prefabs filled in the editor
    public List<GameObject> spawned = new List<GameObject>(); // creates a list to store the spawned objects 

    void Update()
    {
        movementTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (movementTimer > 1.5) // randomises the current location and rotation of the object if the movement timeer exceeds 1.5 seconds
        {
            transform.position = new Vector3(Random.Range(-60, -12), 0.7f, Random.Range(2, 50)); // creates a new reandom position and applie it
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0); // creates a new random retation and applies it 
            movementTimer = 0f; // resets the movement timer
        }

        if(spawnTimer > 2 && spawned.Count < 3) // spawns a new enemy entity if there are less that 3 in the spawned list and if the spawn timer has been exceded
        {
            GameObject chosenSpawn = enemys[Random.Range(0, enemys.Length)]; // select the prefab to spawn from the enemys array using a random range
            var currentspawn = Instantiate(chosenSpawn, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation); //instantiates a new clone of the prefab and sets its position to the position of this object. also stores it as a temp variable
            spawned.Add(currentspawn); // takes the object stored in the temp variable and adds it to the list of objects spawned
            spawnTimer = 0f; // resets the spawn timmer
        }
    }
}
