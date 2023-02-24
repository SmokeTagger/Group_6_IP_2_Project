using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to provide functionality to the a rocket prefab fired by the launcher, creats an explotion force and effects the objects hit 
/// </summary>
public class rightRocket : MonoBehaviour
{
    float radius = 20f; //variables of the explotion
    float force = -1500f;

    public GameObject explotion; // inactive game object attached to show and explotion when the rocket exploeds

    manager managerScript; //empty refrence to acces manger script

    public AudioSource explode; //audio souce attached to prefab

    void Awake()
    {
        managerScript = GameObject.FindGameObjectWithTag("ManagerTag").GetComponent<manager>(); //sets the managerScript to the script in manager by finding the objecet with the relevent tag 
    }
    void Start() // starts the explode funtion when the rocket is initialised
    {
        StartCoroutine(RocketExplode()); 
    }

    public IEnumerator RocketExplode() // code to detonate the rocket in the air if it dosent hit anything
    {
        yield return new WaitForSeconds(3f); //will wait for 3 seconds before automaticaly exploding
        var rocket = GetComponent<Rigidbody>(); //this grabs the objects RB and stop it 
        rocket.velocity = Vector3.zero;
        rocket.angularVelocity = Vector3.zero;
        explotion.gameObject.SetActive(true); // activates the explotion effect
        explode.Play();
        Collider[] objectsHit = Physics.OverlapSphere(transform.position, radius); //creates and array to store all the objects hit by the explotion
        foreach (Collider hit in objectsHit)    // run through each of the object in that array
        {
            if (hit.gameObject.tag == "Enemy" ) // checks weather the objects hit have the relevent tag
            {

                Rigidbody rb = hit.GetComponent<Rigidbody>(); //aquires the rigid body and basicEnemy class components form those objects
                basicEnemy be = hit.GetComponent<basicEnemy>();
                if (rb != null)                                 // provided the object had a rigid body it applies the explotion force to them and calls a function form the class to decrease thier health
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (be != null)
                {
                    be.LooseHealth();
                }

                managerScript.hitNumber += 1; //increase the number of enimies hit that displace on screen
            }

        }

        yield return new WaitForSeconds(0.5f); //small wait so we can see the explotion effect before the rocket is destroyed 
        Destroy(gameObject);

    }

    void OnCollisionEnter(Collision col) // another method to explolde the rocket buy calling a simmilar coroutine but pased through paramaters to check it it has collided with something
    {
       
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enviroment")
        {
            var rocket = GetComponent<Rigidbody>(); //this grabs the objects RB and stop it 
            rocket.velocity = Vector3.zero;
            rocket.angularVelocity = Vector3.zero;
            StartCoroutine(RocketExplodeHit());

        }

    }

    public IEnumerator RocketExplodeHit() // largly the same IEnumerator as above but with out the initial timer so it starts detonating as soon as the funtion is called 
    {
        explotion.gameObject.SetActive(true);
        explode.Play();
        Collider[] objectsHit = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in objectsHit)
        {
            if (hit.gameObject.tag == "Enemy")
            {

                Rigidbody rb = hit.GetComponent<Rigidbody>();
                basicEnemy be = hit.GetComponent<basicEnemy>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (be != null)
                {
                    be.LooseHealth();
                }

                managerScript.hitNumber += 1; 
            }

        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }
}
