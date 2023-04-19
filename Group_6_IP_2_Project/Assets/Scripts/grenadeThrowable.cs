using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrowable : MonoBehaviour
{
    public GameObject explotion;
    float radius = 10f;
    float force = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode() // largly the same IEnumerator as above but with out the initial timer so it starts detonating as soon as the funtion is called 
    {
        yield return new WaitForSeconds(3f);
        explotion.gameObject.SetActive(true);
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

            if (hit.gameObject.tag == "Player")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                playerHealth ph = hit.GetComponent<playerHealth>();

                if (rb != null) 
                {
                    rb.AddExplosionForce(force,transform.position, radius);
                }

                if (ph != null) 
                {
                    ph.healthhvy();
                }
            }
        }

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }


}
