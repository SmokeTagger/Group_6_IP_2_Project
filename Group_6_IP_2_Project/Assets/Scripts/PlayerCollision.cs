using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField] float x;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = rb.velocity.x;

    }

    private void OnCollisionEnter(Collision col)
    {
        if (x < -20 && col.gameObject.tag == "Destructable" || x > 20 && col.gameObject.tag == "Destructable")
        {
            WallDestruction wd = col.gameObject.GetComponent<WallDestruction>();

            if (wd != null)
            {
                wd.LooseHealth();
            }

            MultipartDestruction md = col.gameObject.GetComponent<MultipartDestruction>();
            if (md != null)
            {
                md.LooseHealth();
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (x < -20 && col.gameObject.tag == "Destructable" || x > 20 && col.gameObject.tag == "Destructable")
        {
            WallDestruction wd = col.gameObject.transform.parent.gameObject.GetComponent<WallDestruction>(); // TIDY THIS SHIT UP
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
