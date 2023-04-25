using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrowable : MonoBehaviour
{
    public GameObject explotion;
    float radius = 10f;
    float force = 1000f;

    public GameObject musicBox;
    public menuMusic mM;

    // calls the explotion corortine to start the contdown
    void Start()
    {
        StartCoroutine(Explode());
    }

    //update grabs the music bos to play sounds on pickup

    private void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
    }

    //waits for a sertain time and the uses and explotion force to apply the explosives detonation
    //effects of the explotion are different depeneding on the tag to call the coreect scripts 
    public IEnumerator Explode() 
    {
        yield return new WaitForSeconds(3f);
        mM.PlayGrenade();
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
