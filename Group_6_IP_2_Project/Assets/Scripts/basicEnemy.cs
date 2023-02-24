using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class basicEnemy : MonoBehaviour
{
    public GameObject tracker; // varaible to store an object with the tracker script

    public float timeLoop = 0; // variable for the movement
    int speed = 3;

    public int health = 3; // varaible controling the health 

    public GameObject player; // varaibles for changing the object colour
    Color colour1 = Color.green;
    Color colour2 = Color.yellow;
    Color colour3 = Color.red;
    float duration = 1.0f;
    Renderer rend;

    enemySpawn enemySpawner; // reference to store the enemy spawner class
    public GameObject enemy; // variable to store the object this script is attached to

    void Awake()
    {
        tracker = GameObject.FindGameObjectWithTag("Player"); // find the player game object and assigns it to the tracker variable
        tracker.GetComponent<enemyTracker>().enemyList.Add(gameObject); // acceses the enermylist in the enemy tracker script on the player and adds this object to the list
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<enemySpawn>(); // acceses the enemySpawn class form the enemy spawner and assines it 
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        timeLoop += Time.deltaTime; // starts an increasing timer that can be referenced for movement

        float lerp = Mathf.PingPong(Time.time, duration) / duration;

        if (timeLoop < 5)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime); // move the object right while the timer is lest that 5
        }

        if (timeLoop > 5 && timeLoop < 10)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); // move the object left while the timer is lest than 10 but more than 5
        }

        if (timeLoop > 10) // resets the timer if it exceded 10 so the loop can start again
        {
            timeLoop = 0;
        }

        if (health < 1) //calls the kill function if health is less than 1
        {
            EnemyKill();
        }

        if (health == 3) //set the colour of the object at the start
        {
            rend.material.color = colour1;
        }

        if (health == 2) //alternates the colour btween geen and yellow when health has droped
        {
            rend.material.color = Color.Lerp(colour1, colour2, lerp);
        }

        if (health == 1) //alternates the colour betweeen yellow and red when on last health point
        {
            rend.material.color = Color.Lerp(colour2, colour3, lerp);
        }
    }

    public void LooseHealth() // a function that can be called by the ray cast to decrease the health of the box by one
    {
        health -= 1;
    }

    public void EnemyKill() // method to destory the enemy prefab
    {
        tracker.GetComponent<enemyTracker>().enemyList.Remove(gameObject); // removes the object from the enemylist on the tracker
        enemySpawner.spawned.Remove(enemy); // removes the object from the spawned list in spawner script
        Destroy(gameObject); // destroys the object
    }

}
