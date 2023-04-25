using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public GameObject spawner;
    public GameObject pickup;

    public GameObject musicBox;
    public menuMusic mM;

    //update grabs the music bos to play sounds on pickup
    private void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
    }

    // adds health to the player when it the activate the pick up, then removes it from the pickup list so another can spawn and destroys the pickup object
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            if (ph.health < 120)
            {
                mM.PlayHealthUp();
                ph.health += 20;
            }

            pickupSpawner ps = spawner.GetComponent<pickupSpawner>();
            ps.spawns.Remove(pickup);
            Destroy(gameObject);
        }
    }
}
