using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
/// <summary>
/// Script to provide functionality to the rifle allowing it fire, produce sounds and interact with objects in the world
/// </summary>
public class gun : MonoBehaviour
{
    public Camera cam;

    float force = 1000f;
    public GameObject[] bulletHolePrefabs; // creates an array of bullethole prefabs to pull from

    public bool canShoot;
    public float rate;  // variables for the fire rate
    public float rateMax;
    public bool reloading;

    public int ammo; // variables for reloading
    public int ammoMax;
    public float ammoTimer;
    public float ammoTimerMax;

    public GameObject flash; //variables for the muzzel flash

    public GameObject bullet; // variables for bullet spawning
    public GameObject bulletSpawn;
    int thrust = 1000;

    audioManager audioScript;//empty refences to store class's from other scripts
    manager managerScript;

    bool single = true; // variables for the fire rate 
    bool burst = false;
    bool auto = false;
    float autoFire = 0;

    void Awake()
    {
        managerScript = GameObject.FindGameObjectWithTag("ManagerTag").GetComponent<manager>();//find the manager script to edit ammo and hit counter values
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>(); //finds the audio manager to call the sound functions in it
    }

    void Start()
    {
        // Reloadding initialisation
        ammoMax = 10;
        ammo = ammoMax;
        reloading = false;
        ammoTimer = 0f;
        ammoTimerMax = 2f;

        // fire rate inatialisation
        rate = 0f;
        rateMax = 0.5f;
        canShoot = true;
    }
    void Update()
    {
        managerScript.ammoLeft = ammo; //updates the ammo text
        managerScript.ammoFull = ammoMax;

        if (Input.GetKeyDown(KeyCode.Alpha7)) //these if statments activate and deactivate bools to control witch if statment is run later to provided a fire rate
        {
            single = true;
            burst = false;
            auto = false;
            audioScript.PlayClick(); // also plays an audio sound so you know youve done it
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            single = false;
            burst = true;
            auto = false;
            audioScript.PlayClick();

        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            single = false;
            burst = false;
            auto = true;
            audioScript.PlayClick();

        }


        if (ammo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            reloading = true; // changes the relading bool to return true if we hit the relaod button (R) or if were out of ammo
        }

        if(reloading == true)
        {
            ammoTimer += Time.deltaTime; // while were reloading we will add the time pased to the ammo timer each frame

            if(ammoTimer > ammoTimerMax) // checks if our reload timer has exxeded the max time limit
            {
                audioScript.PlayRifleReload();
                ammo = ammoMax; // resets out ammo to the max
                reloading = false; // sets us to no longer be relaoding
                ammoTimer = 0f; // resets out reload timer
            }
        }

        if(canShoot == false) 
        {
            rate = rate + Time.deltaTime; // adds the amount of time passed as to the rate value

            if (rate > rateMax) // checks of the rate value has exeded the maximum
            {
                canShoot = true; // allows us to shoot
                rate = 0f; //resets the rate of fire
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true && reloading == false && Time.timeScale == 1) //runs through the relvent if statment for Rof if the left mouse key is pressed if we are able to shoot and we're not relaoding and teh game is running
        {
            flash.gameObject.SetActive(true); //activates muzzel flash

            if (single) // if our single fire is selcted  we just fire the gun, fire a bullet play a sound and starts the can shoot loop
            {
                FireLeft();
                FireBullet();
                audioScript.PlayRifle();
                canShoot = false;
            }

            if (burst) // if burst is selectd it runs a co-rotine shown later
            {
                BurstLeft();
            }
        }
        else if(Input.GetKey(KeyCode.Mouse0) && canShoot == true && reloading == false && Time.timeScale == 1 && auto) //this runs the auto fire stamtment if the key is held down and the other variables return as they should
        {
            autoFire += Time.deltaTime; // creates a time delay between running the same code as the single fire. stop the code from being called every frame
            if (autoFire > 0.2)
            {
                FireLeft();
                FireBullet();
                audioScript.PlayRifle();
                autoFire = 0;
            }

        }
        else
        {
            flash.gameObject.SetActive(false); // deactivates muzzel flash when button released
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot == true && reloading == false && Time.timeScale == 1) //calls the fireright function if the right mouse key is pressed if we are able to shoot and we're not relaodin and the game is running
        {
            if (single) // if our single fire is selcted  we just fire the gun, fire a bullet play a sound and starts the can shoot loop
            {
                FireRight();
                FireBullet();
                audioScript.PlayRifleBack();
                canShoot = false;
            }

            if (burst) // if burst is selectd it runs a co-rotine shown later
            {
                BurstRight();
            }
        }
        else if (Input.GetKey(KeyCode.Mouse1) && canShoot == true && reloading == false && Time.timeScale == 1 && auto) //this runs the auto fire stamtment if the key is held down and the other variables return as they should
        {
            autoFire += Time.deltaTime; // creates a time delay between running the same code as the single fire. stop the code from being called every frame
            if (autoFire > 0.2)
            {
                FireRight();
                FireBullet();
                audioScript.PlayRifleBack();
                autoFire = 0;
            }

        }
        else
        {
            flash.gameObject.SetActive(false); // deactivates muzzel flash when button released
        }
    }

    public void FireLeft() // public function that can be called to fire the gun
    {
        ammo -= 1;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // this deffines where the ray cass it spawning from. in this cass the camera. the ViewportPointToRay specifies where in the camera space, at the moment the middle
        RaycastHit hit; // creates an empty object that can be used to reference what we hit

        if (Physics.Raycast(ray, out hit) && Time.timeScale == 1) 
        {
            GameObject chosenBulletHole = bulletHolePrefabs[Random.Range(0, bulletHolePrefabs.Length)]; // uses the array of bullethole prefabs and random range to select a random one to instantiate later

            var tempBullet = Instantiate(chosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal)); //creates the bullet hold as a tempary variable and then assigns it as a child to whatever it hits and instantiated the prefab
            tempBullet.transform.parent = hit.transform;

            if (hit.transform.gameObject.tag == "Enemy") //checks if what we hit has has the enemy tag
            {
                var direction = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, hit.transform.position.z - transform.position.z); //get the direction to apply force by subtracting where we are from what we hit
                hit.rigidbody.AddForceAtPosition(force * Vector3.Normalize(direction), hit.point); //applys the force to the object we hit and normalises it so as not to add the force length of the vector onto our force variable  
                basicEnemy enemymov = hit.collider.GetComponent<basicEnemy>(); // get the script with the loose helth function from the object hit
                enemymov.LooseHealth(); // removes one from the health of the entity hit
                managerScript.hitNumber += 1; //increases the hit counter 
            }
        }
    }

    public void FireRight() // public function that can be called to fire the gun
    {
        ammo -= 2;
        
               Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // this deffines where the ray cass it spawning from. in this cass the camera. the ViewportPointToRay specifies where in the camera space, at the moment the middle
               RaycastHit hit; // creates an empty object that can be used to reference what we hit
        
         if (Physics.Raycast(ray, out hit) && Time.timeScale == 1)
         {
            GameObject chosenBulletHole = bulletHolePrefabs[Random.Range(0, bulletHolePrefabs.Length)]; // uses the array of bullethole prefabs and random range to select a random one to instantiate later

            var tempBullet = Instantiate(chosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal)); //creates the bullet hold as a tempary variable and then assigns it as a child to whatever it hits and instantiated the prefab
            tempBullet.transform.parent = hit.transform;

            if (hit.transform.gameObject.tag == "Enemy") //checks if what we hit has has the enemy tag
            {
                var direction = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, hit.transform.position.z - transform.position.z); //get the direction to apply force by subtracting where we are from what we hit
                hit.rigidbody.AddForceAtPosition(force * Vector3.Normalize(-direction), hit.point); //applys the force to the object we hit and normalises it so as not to add the force length of the vector onto our force variable  
                basicEnemy enemymov = hit.collider.GetComponent<basicEnemy>(); // get the script with the loose helth function from the object hit
                enemymov.LooseHealth(); // removes one from the health of the entity hit
                managerScript.hitNumber += 1; //increases the hit counter 
            }
        }
    }

    public void FireBullet()
    {
        var bulletins = Instantiate(bullet, new Vector3(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y, bulletSpawn.transform.position.z), transform.rotation); //sets the spawn location to the bullet spawner

        bulletins.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward* thrust); //adds force to the bullet ridgid body
    }

    void BurstLeft() //these are seperate methods to call the coroutine outside of the start method
    {
        StartCoroutine(BurstFireLeft());
    }
    void BurstRight()
    {
        StartCoroutine(BurstFireRight());
    }
    IEnumerator BurstFireLeft() // using an IEnumerator so there can be a delay between each round. the code is the same as single shot seperated by a wait period
    {
        FireLeft();
        FireBullet();
        audioScript.PlayRifle();
        yield return new WaitForSeconds(0.1f);
        FireLeft();
        FireBullet();
        audioScript.PlayRifle();
        yield return new WaitForSeconds(0.1f);
        FireLeft();
        FireBullet();
        audioScript.PlayRifle();
        canShoot = false;
    }

    IEnumerator BurstFireRight()
    {
        FireRight();
        FireBullet();
        audioScript.PlayRifleBack();
        yield return new WaitForSeconds(0.1f);
        FireRight();
        FireBullet();
        audioScript.PlayRifleBack();
        yield return new WaitForSeconds(0.1f);
        FireRight();
        FireBullet();
        audioScript.PlayRifleBack();
        canShoot = false;
    }
}
