using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superAttack : MonoBehaviour
{
    [SerializeField] float force = -1600;
    public GameObject trigger;
    public GameObject self;

    private void Update()
    {
        if (trigger.activeInHierarchy)
        {
            force = 120f;
        }
        if (!trigger.activeInHierarchy)
        {
            force = -120f;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(40 * force, 0, 0);
            col.gameObject.GetComponent<WallDestruction>().LooseHealth();
        }

        if (col.gameObject.tag == "Player" && col.gameObject != self)
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(40 * force, 0, 0);
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            ph.healthSuper();
        }
    }
}
