using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downAttack : MonoBehaviour
{
    [SerializeField] float force = -65;
    public GameObject trigger;
    public GameObject self;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0); // Adds a force to the object 
            col.gameObject.GetComponent<WallDestruction>().LooseHealth(); // reduces the amout of health the object by attack damage 
        }

        if (col.gameObject.tag == "Player" && col.gameObject != self)
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            ph.healthdown();
            playerEnergy pe = self.GetComponent<playerEnergy>();
            pe.Energydown();
        }
    }
}
