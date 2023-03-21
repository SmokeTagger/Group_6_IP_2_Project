using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadePickUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerMovement pm = col.gameObject.GetComponent<playerMovement>();
            pm.grenade = true;
            Destroy(gameObject);
        }
    }
}
