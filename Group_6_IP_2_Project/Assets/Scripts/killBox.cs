using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBox : MonoBehaviour
{

    public Transform spawnPoint; // varaiable to store the spawn point object
    manager managerScript; // reference to hold the manager script
    public GameObject Manager; // varaible to hold the objet with the manager script attached
    

    void Awake()
    {
        managerScript = Manager.GetComponent<manager>(); //get the manager script form the manger object and set it 

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") // checks if the object that had entered the triger has the player tag
        {
            CharacterController cc = col.GetComponent<CharacterController>(); // get a reference for the character controler
            cc.enabled = false; // disable the character controler

            col.gameObject.transform.position = spawnPoint.position; // transfrom the object with the player tag to the spawn point possition
            cc.enabled = true; // reenables the character controler

            managerScript.LowerHealth();
        }

        if (col.gameObject.tag == "Enemy") // checks if the object that enterd has the enemy tag
        {

            basicEnemy boxHit; // refernce for the script on the object that enetered
            boxHit = col.gameObject.GetComponent<basicEnemy>(); // stores the script 
            boxHit.EnemyKill(); // and call the kill enemy function from that object

        }
    }
}
