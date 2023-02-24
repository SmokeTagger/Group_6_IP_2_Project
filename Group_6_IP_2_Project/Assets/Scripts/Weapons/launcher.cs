using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to provide functionality to the pistol allowing it fire rocket prefabs and produce sounds
/// </summary>
public class launcher : MonoBehaviour
{
    public bool canShoot;
    public float rate;  // variables for the fire rate
    public float rateMax;
    public bool reloading;

    public int ammo; // variables for reloading
    public int ammoMax;
    public float ammoTimer;
    public float ammoTimerMax;

    public GameObject flash; //variables for the muzzel flash

    public GameObject rocketLeft; // variables for spawning rockets
    public GameObject rocketRight;
    public GameObject rocketSpawn;
    int thrust = 500;

    audioManager audioScript; //empty refences to store class's from other scripts
    manager managerScript;

    void Awake()
    {
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>(); //finds the audio manager to call the sound functions in it
        managerScript = GameObject.FindGameObjectWithTag("ManagerTag").GetComponent<manager>(); //find the manager script to edit ammo and hit counter values
    }

    void Start()
    {
        // Reloadding initialisation
        ammoMax = 2;
        ammo = ammoMax;
        reloading = false;
        ammoTimer = 0f;
        ammoTimerMax = 3f;

        // fire rate inatialisation
        rate = 0f;
        rateMax = 0.5f;
        canShoot = true;
    }

    void Update()
    {
        managerScript.ammoLeft = ammo; //updates the ammo text
        managerScript.ammoFull = ammoMax;

        if (ammo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            reloading = true; // changes the reloading bool to return true if we hit the relaod button (R) or if were out of ammo
        }

        if (reloading == true)
        {
            ammoTimer += Time.deltaTime; // while were reloading we will add the time pased to the ammo timer each frame

            if (ammoTimer > ammoTimerMax) // checks if our reload timer has exxeded the max time limit
            {
                audioScript.PlayLauncherReload();
                ammo = ammoMax; // resets out ammo to the max
                reloading = false; // sets us to no longer be relaoding
                ammoTimer = 0f; // resets out reload timer
            }
        }

        if (canShoot == false)
        {
            rate = rate + Time.deltaTime; // adds the amount of time passed as to the rate value

            if (rate > rateMax) // checks of the rate value has exeded the maximum
            {
                canShoot = true; // allows us to shoot
                rate = 0f; //resets the rate of fire
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true && reloading == false && Time.timeScale == 1) //calls the FireRocketRight and firebullet function if the left mouse key is pressed if we are able to shoot and we're not relaoding and the game is running
        {
            FireRocketLeft();
            audioScript.PlayLauncher();
            flash.gameObject.SetActive(true); //activates muzzel flash
            canShoot = false;
        }
        else
        {
            flash.gameObject.SetActive(false); // deactivates muzzel flash when button released
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot == true && reloading == false && Time.timeScale == 1) //calls the FireRocketRight and firebullet function if the right mouse key is pressed if we are able to shoot and we're not relaodin and the game is running
        {
            FireRocketRight();
            audioScript.PlayLauncherBack();
            flash.gameObject.SetActive(true); //activates muzzel flash
            canShoot = false;
        }
        else
        {
            flash.gameObject.SetActive(false); // deactivates muzzel flash when button released
        }
    }

    public void FireRocketLeft() // function to spawn a rocekt left prefab
    {
        ammo -= 1;

        var rocket = Instantiate(rocketLeft, new Vector3(rocketSpawn.transform.position.x, rocketSpawn.transform.position.y, rocketSpawn.transform.position.z), transform.rotation); //sets the spawn location to the rocket spawner

        rocket.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust); //adds force to the rockets ridgid body
    }
    public void FireRocketRight() // function to spawn a rocekt right prefab
    {
        ammo -= 1;

        var rocket = Instantiate(rocketRight, new Vector3(rocketSpawn.transform.position.x, rocketSpawn.transform.position.y, rocketSpawn.transform.position.z), transform.rotation); //sets the spawn location to the rocket spawner

        rocket.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust); //adds force to the rocekts ridgid body
    }
}
