using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyTurret : MonoBehaviour
{
    public Vector3 targetDir;
    public GameObject player;
    public float speed = 1;
    public float maxFollowAngle = 0.4f;
    float maxVisibilityDistance = 50;

    public float timeToEngage = 0f; // list of variables to deterimite of the weapon can engage the target it tracking
    public float timeToEngageMax = 5f;
    public bool canEngage = false;

    public Text tracking; // text varaible for Tracking UI Element

    bool turretActive; // variable used to simplify later bool functions

    public GameObject bullet; // variables for bullet spawning
    public GameObject bulletSpawn;
    int thrust = 500;
    public float shootTimer = 0f;
    public float shootTimermax = 1f;

    audioManager audioScript;

    void Awake()
    {
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // find the player object through use of the player tag

        StartCoroutine(CheckDistance()); //starts the coroutine
        turretActive = false; // set the turret to inactive 
    }

    void Update()
    {
        tracking.text = "not targeted";

        if (turretActive)
        {
            targetDir = player.transform.position - transform.position; // sets the targedir vector 3 to the difference between the player and the turret
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.deltaTime, 0.0f); // creats a new vector 3 to rotate the turet to wards the player at the speed in over time
            transform.rotation = Quaternion.LookRotation(newDirection); // applies the new vector 3

            if(timeToEngage == 0) // plays a sound clip when the time is 0
            {
                audioScript.PlayIntruder();
            }

            timeToEngage += Time.deltaTime; //increments the time to engage 

            tracking.text = "Targeted";

            if (timeToEngage > timeToEngageMax) //adds a time that changes a bool to true when the timer is up. this can be used as a parameter to determine if the turrent can fire yet
            {
                canEngage = true;
                timeToEngage = 6f;
                tracking.text = "Engaged";
            }
            
        }
        else
        {
            timeToEngage = 0; // set the time to engage to 0 if teh turet is not active
        }

        if(turretActive && canEngage) // statment to alow the turret to shoot after the engagment wait time is up
        {
            Fire();
        }
    }

    private bool PlayerVisible() // bool that returns true if the player is withing the sight cone of the turret 
    {
        float dot = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized); // dot float is the magnitude angle between the turret and the player

        if (dot > maxFollowAngle) // if the dot float is greater than the limit of the followangle then the player is withing range so returns true
        {
            return true;
        }
        else // otherwise returns falls
        {
            return false;
        }
    }

    private bool PlayerWithinDistance() // bool that returns true if the player is withing range of the turret
    {
        float distance = Vector3.Distance(player.transform.position, transform.position); // distance uses the Vector 3 distance between the player and turret posisitons

        if (distance < maxVisibilityDistance) // if the distanc is lower than the max the player is withing range and returns true
        {
            return true;
        }
        else // otherwise returns false
        {
            return false;
        }
    }

    public void Fire()
    {
        shootTimer += Time.deltaTime; //starts time between shots timers

        if(shootTimer > shootTimermax)
        {

            audioScript.PlayTurret();
            var bulletins = Instantiate(bullet, new Vector3(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y, bulletSpawn.transform.position.z), transform.localRotation); //sets the spawn location to the bullet spawner

            bulletins.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust); //adds force to the bullet ridgid body
            shootTimer = 0f; //resets time between shots
        }
    }

    public IEnumerator CheckDistance()
    {
        for (; ; ) // infinate for loop so it can be calle in the start function and continue to run
        {
            turretActive = PlayerWithinDistance() ? PlayerVisible() : false; // sets the turretactive bool to true of both playerwithindistance and player visible return true
            yield return new WaitForSeconds(0.1f); // waits for .1 of a second.
        }
    }
}
