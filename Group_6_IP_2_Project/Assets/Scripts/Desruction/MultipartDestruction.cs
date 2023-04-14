using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipartDestruction : MonoBehaviour
{
    [SerializeField] int health = 2;


    public Material dmg1;
    public Material dmg2;
    public Material dmg3;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;

    // float duration = 1.0f;

    Renderer rend1;
    Renderer rend2;
    Renderer rend3;
    Rigidbody rb;
    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend1 = part1.GetComponent<Renderer>();
        rend2 = part2.GetComponent<Renderer>();
        rend3 = part3.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //float lerp = Mathf.PingPong(Time.time, duration) / duration;

        if (health == 2) //set the colour of the object at the start
        {
            if(part1 != null)
            {
                rend1.material = dmg1;
            }

            if (part2 != null)
            {
                rend2.material = dmg1;
            }

            if (part3 != null)
            {
                rend3.material = dmg1;
            }
        }

        if (health == 1) //alternates the colour btween geen and yellow when health has droped
        {
            if (part1 != null)
            {
                rend1.material = dmg2;
            }

            if (part2 != null)
            {
                rend2.material = dmg2;
            }

            if (part3 != null)
            {
                rend3.material = dmg2;
            }

        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // For removeal of block while maintaining velocity switch  to trigger rather than collider. add extra inactive colider activated when health drops    
    }

    public void LooseHealth()
    {
        health -= 1;
    }

}

