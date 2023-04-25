using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class heavyAttack : MonoBehaviour
{
    [SerializeField] float force; // assgines a force varibale
    public GameObject trigger; // hit box trigger to deal damage
    public GameObject self; // the player character who started the attack
    playerMovement move;


    private void Start() 
    { 
        move = self.GetComponent<playerMovement>();
    }

    private void Update()
    {
        if(move.facing)
        {
            force = 80f; // this allows the attack to apply a amount of force and what direction the force sends the hit player
        }
        if (!move.facing)
        {
            force = -80f;
        }


    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable") // this will activate upon colliding with a object tagged as distructable
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            col.gameObject.GetComponent<WallDestruction>().LooseHealth(); // makes the object lose a health state 
        }

        if (col.gameObject.tag == "Player" && col.gameObject != self) // if the attack hits a tagged object with the player tag and its not hitting itself it will apply the following affects 
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0); // adds force to hit character
            playerHealth ph = col.gameObject.GetComponent<playerHealth>(); // grabs the objects health script
            ph.healthhvy(); // deals the damage held within the attack
            playerEnergy pe = self.GetComponent<playerEnergy>();
            pe.Energyhvy(); // also gives amount of energy assigned to the attack

        }
    }
}
