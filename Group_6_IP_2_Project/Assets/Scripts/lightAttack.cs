using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAttack : MonoBehaviour
{
    [SerializeField] float force;
    public GameObject player;

    private void Update()
    {
        if (player.transform.eulerAngles.y == 0)
        {
            force = -40f;
        }
        else if (player.transform.eulerAngles.y == -180)
        {
            force = 40f;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Destructable")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
            col.gameObject.GetComponent<WallDestruction>().LooseHealth();
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(20 * force, 0, 0);
        }
    }
}
