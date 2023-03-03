using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [SerializeField] int health = 3;
    Color colour1 = Color.green;
    Color colour2 = Color.yellow;
    Color colour3 = Color.red;
    float duration = 1.0f;
    Renderer rend;
    Rigidbody rb;
    public GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;

        if (health == 3) //set the colour of the object at the start
        {
            rend.material.color = colour1;
        }

        if (health == 2) //alternates the colour btween geen and yellow when health has droped
        {
            rend.material.color = Color.Lerp(colour1, colour2, lerp);
        }

        if (health == 1) //alternates the colour betweeen yellow and red when on last health point
        {
            rend.material.color = Color.Lerp(colour2, colour3, lerp);
            trigger.SetActive(true);
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
