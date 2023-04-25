using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadePickUp : MonoBehaviour
{
    public GameObject spawner;
    public GameObject pickup;
    public GameObject musicBox;
    menuMusic mM;

    //update grabs the music bos to play sounds on pickup

    private void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
    }
    //acces the player moement script to set the ability to throw grenades, removes the pick up from the list so another can spawn and destorys the pickup
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            mM.PlayPickUp();
            playerMovement pm = col.gameObject.GetComponent<playerMovement>();
            pm.grenade = true;
            pickupSpawner ps = spawner.GetComponent<pickupSpawner>();
            ps.spawns.Remove(pickup);
            Destroy(gameObject);
        }
    }
}
