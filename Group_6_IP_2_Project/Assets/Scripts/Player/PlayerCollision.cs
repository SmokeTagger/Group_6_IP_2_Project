using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    Rigidbody rb; // assigns the Rigidbody to a variable 

    [SerializeField] float x;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = rb.velocity.x; // sets the float x to the rigidbodys x velocity 
    }

    private void OnCollisionEnter(Collision col) // when the object collides with another object it will run this code
    {
        if (x < -20 && col.gameObject.tag == "Destructable" || x > 20 && col.gameObject.tag == "Destructable") // if the player hits an object tagged destructable but is going greater than 20 it will damage the object
        {
            WallDestruction wd = col.gameObject.GetComponent<WallDestruction>(); // 

            if (wd != null)
            {
                wd.LooseHealth(); // this causes the object to lose a health state damaging it 
            }

            MultipartDestruction md = col.gameObject.GetComponent<MultipartDestruction>();
            if (md != null)
            {
                md.LooseHealth();
            }
        }
    }

    private void OnTriggerEnter(Collider col) // this function actiaves when a trigger collides with this object such as a player character
    {
        if (x < -20 && col.gameObject.tag == "Destructable" || x > 20 && col.gameObject.tag == "Destructable")
        {
            WallDestruction wd = col.gameObject.transform.parent.gameObject.GetComponent<WallDestruction>(); 
            if (wd != null)
            {
                wd.LooseHealth();
            }

            MultipartDestruction md = col.gameObject.transform.parent.gameObject.GetComponent<MultipartDestruction>();
            if(md != null)
            {
                md.LooseHealth();
            }

        }
    }
}
