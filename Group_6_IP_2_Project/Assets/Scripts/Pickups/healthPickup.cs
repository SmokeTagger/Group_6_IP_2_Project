using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public GameObject spawner;
    public GameObject pickup;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            if (ph.health < 120)
            { 
                ph.health += 20;
            }

            pickupSpawner ps = spawner.GetComponent<pickupSpawner>();
            ps.spawns.Remove(pickup);
            Destroy(gameObject);
        }
    }
}
