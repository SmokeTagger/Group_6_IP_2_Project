using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{
    [SerializeField] float spawntimer = 15f;
    public GameObject grenade;
    public GameObject health;
    public GameObject thisSpawner;
    public List<GameObject> spawns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update runs a timer and random range so select witch cpick up to spawn and how long to wait after the pickup picked up
    // the pick up object is instatited and then aded to a list to keep track of weather it needs to spawn another
    void Update()
    {
        if (spawns.Count < 1)
        {
            spawntimer += Time.deltaTime;

            if (spawntimer > 20f)
            {
                int i = Random.Range(1, 10);

                if (i < 5)
                {
                    var currentspawn = Instantiate(grenade, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    spawns.Add(currentspawn);

                    grenadePickUp pickup = currentspawn.GetComponent<grenadePickUp>();
                    pickup.spawner = thisSpawner;

                    spawntimer = 0f;
                }
                else 
                {
                    var currentspawn = Instantiate(health, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 20)));
                    spawns.Add(currentspawn);

                    healthPickup pickup = currentspawn.GetComponent<healthPickup>();
                    pickup.spawner = thisSpawner;

                    spawntimer = 0f;
                }
            }
        }
    }
}
