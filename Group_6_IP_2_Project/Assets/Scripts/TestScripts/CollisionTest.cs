using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{

    Rigidbody rb;
    float thrustfw = 1000;
    float thrustbk = 500;

    float radius = 20f;
    float force = -1500f;
    public GameObject explotion;

    public float x;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = rb.velocity.x;

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(transform.right * thrustfw);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(-transform.right * thrustbk);
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Wrapper();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (x < -40 && col.gameObject.tag == "TestTag")
        {
            WallTest wt = col.gameObject.GetComponent<WallTest>();

            if (wt != null)
            {
                wt.LooseHealth();
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
            if (hit.gameObject.tag == "TestTag")
            {

                Rigidbody rb = hit.GetComponent<Rigidbody>();
                WallTest wt = hit.GetComponent<WallTest>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (wt != null)
                {
                    wt.LooseHealth();
                }
            }

            
            explotion.gameObject.SetActive(false);
        }
    }
}


