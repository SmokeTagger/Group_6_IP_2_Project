using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAttack : MonoBehaviour
{
    [SerializeField] float force ;
    public GameObject trigger;
    public GameObject self;
    playerMovement move;

    private void Start()
    {
        move = self.GetComponent<playerMovement>();
    }

    private void Update()
    {
        
        if (move.facing)
        {
            force = 40f;
        }
        if (!move.facing)
        {
            force = -40f;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force,0 ,0);
            col.gameObject.GetComponent<WallDestruction>().LooseHealth();
        }

        if (col.gameObject.tag == "Player" && col.gameObject != self)
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            playerHealth ph = col.gameObject.GetComponent<playerHealth>();
            ph.healthlite();
            playerEnergy pe = self.GetComponent<playerEnergy>();
            pe.Energylite();
        }
    }
}
