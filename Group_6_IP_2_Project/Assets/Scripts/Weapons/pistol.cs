using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script to provide functionality to the pistol allowing it fire, produce sounds and interact with objects in the world
/// </summary>
public class pistol : MonoBehaviour
{
    public Camera cam;

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
        ammoMax = 5;
        ammo = ammoMax;
        reloading = false;
        ammoTimer = 0f;
        ammoTimerMax = 1f;

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
            reloading = true;// changes the relading bool to return true if we hit the relaod button (R) or if were out of ammo and sets 
        }

        if (reloading == true)
        {
            ammoTimer += Time.deltaTime; // while were reloading we will add the time pased to the ammo timer each frame

            if (ammoTimer > ammoTimerMax) // checks if our reload timer has exxeded the max time limit
            {
                audioScript.PlayPistolReload();
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true && reloading == false && Time.timeScale == 1) //calls the fireleft and firebullet function if the left mouse key is pressed if we are able to shoot and we're not relaoding and the game is running
        {
            FireLeft();
            FireBullet();
            audioScript.PlayPistol();
            flash.gameObject.SetActive(true); //activates muzzel flash
            canShoot = false;
        }
        else
        {
            flash.gameObject.SetActive(false); // deactivates muzzel flash when button released
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot == true && reloading == false && Time.timeScale == 1) //calls the fireright and firebullet function if the right mouse key is pressed if we are able to shoot and we're not relaoding and the game is runnig
        {
            FireRight();
            FireBullet();
            audioScript.PlayPistolBack();
            flash.gameObject.SetActive(true); //activates muzzel flash
            canShoot = false;
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

        if (Physics.Raycast(ray, out hit) && Time.timeScale == 1) //checks if the ray hit anything and if the game is running
        {
            GameObject chosenBulletHole = bulletHolePrefabs[Random.Range(0, bulletHolePrefabs.Length)]; // uses the array of bullethole prefabs and random range to select a random one to instantiate later
            GameObject objectHit;

            var tempBullet = Instantiate(chosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal)); //creates the bullet hole as a tempary variable and then assigns it as a child to whatever it hits and instantiates the prefab 
            tempBullet.transform.parent = hit.transform;

            if (hit.transform.gameObject.tag == "Enemy") //checks if what we hit has has the enemy tag
            {

                objectHit = hit.transform.gameObject; // initalised the object hit variable as the object that was hit by the ray
                objectHit.gameObject.transform.localScale += new Vector3(2, 2, 2); // transfroms that stored object with the new vector3 increasing teh size
                objectHit.gameObject.transform.localPosition += new Vector3(0, 2, 0); // icrease the height of the object hit so it dosen hit the kill box 
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
            GameObject objectHit;

            var tempBullet = Instantiate(chosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal)); //creates the bullet hole as a tempary variable and then assigns it as a child to whatever it hits and instantiates the prefab 
            tempBullet.transform.parent = hit.transform;

            if (hit.transform.gameObject.tag == "Enemy") //checks if what we hit has has the enemy tag
            {

                objectHit = hit.transform.gameObject; // initalised the object hit variable as the object that was hit by the ray
                objectHit.gameObject.transform.localScale += new Vector3(-2, -2, -2); // transfroms that stored object with the new vector3 in this case negative to decrease the size
                basicEnemy enemymov = hit.collider.GetComponent<basicEnemy>(); // get the script with the loose helth function from the object hit
                enemymov.LooseHealth(); // removes one from the health of the entity hit
                managerScript.hitNumber += 1; //increases the hit counter
            }
        }
    }

    public void FireBullet()
    {
        var bulletins = Instantiate(bullet, new Vector3(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y, bulletSpawn.transform.position.z), transform.rotation); //sets the spawn location to the bullet spawner

        bulletins.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrust); //adds force to the bullet ridgid body
    }
}
