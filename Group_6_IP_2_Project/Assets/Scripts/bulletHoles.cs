using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHoles : MonoBehaviour
{/// <summary>
/// script to destoyer buller hole prefabs after a set time
/// </summary>
    public float bulletHoleTimer = 0f;
    public float bulletHoleTimermax = 3f; 
    // Update is called once per frame
    void Update()
    {
        bulletHoleTimer += Time.deltaTime; // this runs as a simple time checking how loag the bullet hole has been instantiated

        if(bulletHoleTimer > bulletHoleTimermax)
        {
            Destroy(gameObject); // this destoryes the instance of the bullet hole ater its time excedes the max
        }
    }
}
