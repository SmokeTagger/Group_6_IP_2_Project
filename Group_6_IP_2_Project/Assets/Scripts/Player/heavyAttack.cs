using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyAttack : MonoBehaviour
{
    [SerializeField] float force = -1600;
    public GameObject trigger;
    public GameObject self;

    private void Update()
    {
        if(trigger.activeInHierarchy)
        {
            force = 80f;
        }
        if (!trigger.activeInHierarchy)
        {
            force = -80f;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            col.gameObject.GetComponent<WallDestruction>().LooseHealth();
        }

        if (col.gameObject.tag == "Player" && col.gameObject != self )
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            ph.healthhvy();
            playerEnergy pe = self.GetComponent<playerEnergy>();
            pe.Energyhvy();

        }
    }
}
