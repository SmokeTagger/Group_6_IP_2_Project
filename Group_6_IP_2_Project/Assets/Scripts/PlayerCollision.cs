using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    Rigidbody rb;
    //float thrustfw = 1000;
    //float thrustbk = 500;

    // explosion values 
    float radius = 5f;
    float force = -1500f;
    public GameObject explotion;

    [SerializeField] float x;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = rb.velocity.x;

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    rb.AddForce(transform.right * thrustfw);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    rb.AddForce(-transform.right * thrustbk);
        //}

       if (Input.GetKeyDown(KeyCode.E))
       {
           Wrapper();
       }
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

    public void Wrapper()
    {
        StartCoroutine(RocketExplodeHit());

    }

    public IEnumerator RocketExplodeHit() // largly the same IEnumerator as above but with out the initial timer so it starts detonating as soon as the funtion is called 
    {
        explotion.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Collider[] objectsHit = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in objectsHit)
        {
            if (hit.gameObject.tag == "Destructable")
            {

                Rigidbody rb = hit.GetComponent<Rigidbody>();
                WallDestruction wd = hit.GetComponent<WallDestruction>();
                MultipartDestruction md = hit.GetComponent<MultipartDestruction>();

                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (wd != null)
                {
                    wd.LooseHealth();
                }

                if (md != null)
                {
                    md.LooseHealth();
                }
            }


            explotion.gameObject.SetActive(false);
        }
    }
}
