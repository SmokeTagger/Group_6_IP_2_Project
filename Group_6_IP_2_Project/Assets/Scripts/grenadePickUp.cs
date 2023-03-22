using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadePickUp : MonoBehaviour
{
    public GameObject spawner;
    public GameObject pickup;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerMovement pm = col.gameObject.GetComponent<playerMovement>();
            pm.grenade = true;
            pickupSpawner ps = spawner.GetComponent<pickupSpawner>();
            ps.spawns.Remove(pickup);
            Destroy(gameObject);
        }
    }
}
