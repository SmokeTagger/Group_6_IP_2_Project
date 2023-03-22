using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{
    [SerializeField] float spawntimer = 15f;
    public GameObject grenade;
    public GameObject thisSpawner;
    public List<GameObject> spawns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawns.Count < 1)
        {
            spawntimer += Time.deltaTime;

            if (spawntimer > 20f)
            {
                var currentgrenade = Instantiate(grenade, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                spawns.Add(currentgrenade);

                grenadePickUp pickup = currentgrenade.GetComponent<grenadePickUp>();
                pickup.spawner = thisSpawner;

                spawntimer = 0f;
            }
        }
    }
}
