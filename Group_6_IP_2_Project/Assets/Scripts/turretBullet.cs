using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    manager manager; // varaibles to store refrences to other class scripts 
    audioManager audioScript;
    public GameObject Managerobj; // varaible to store an object containing the game manager

    void Awake()
    {
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>(); // assings the audioManager script to the audioscritpt reference
        Managerobj = GameObject.FindGameObjectWithTag("ManagerTag"); // find the game object with the manager tag
        manager = Managerobj.GetComponent<manager>(); // and assings the manager script from it
    }

    void Start() 
    {
        StartCoroutine(BulletDeath()); // call the coroutine to destroy the object after a set time
    }

    public IEnumerator BulletDeath() // called when the object is instantiatd and destroyer the object after 3 seconds
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col) // code to activate on colition with another object
    {
        if (col.gameObject.tag == "Player") // if the object has hit has the player tage
        {
            CharacterController cc = col.gameObject.GetComponent<CharacterController>(); // it get the carahceter controler compoenenet and disables it
            cc.enabled = (false);
            col.gameObject.transform.position = new Vector3(Random.Range(-80, 80), Random.Range(1.1f,50), Random.Range(-80, 80)); // then it randomises the position of the player to somewhere withing the bounds of the playspace
            cc.enabled = (true); // reactivates the carachter controle
            manager.LowerHealth(); // lowers the player health
            audioScript.PlayBlood();// plays a sound to let the player know they have been hit 
            Destroy(gameObject); // and destoryer the bullet
        }
        if(col.gameObject.tag == "Enemy") // if it hit something with the enemy or enviroment tag it simply destoyers itself
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Enviroment")
        {
            Destroy(gameObject);
        }
    }
}
