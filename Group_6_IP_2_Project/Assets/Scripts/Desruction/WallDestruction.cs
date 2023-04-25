using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [SerializeField] int health = 3;

    float time = 0;

    public Material dmg1;
    public Material dmg2;
    public Material dmg3;

    public GameObject effect;
    public GameObject musicBox;
    menuMusic mM;

    // float duration = 1.0f;

    Renderer rend;
    public GameObject trigger;

    // grabs the renderer to change the textures
    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    // keeping track of the health int to determine texture and destruction
    void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();

        if (health == 3) 
        {
            rend.material = dmg1;
        }

        if (health == 2) 
        {
            rend.material = dmg2;

        }

        if (health == 1) 
        {
            rend.material = dmg3;
            trigger.SetActive(true);
        }

        if (health <= 0)
        {
            effect.SetActive(true);

            time += Time.deltaTime;

            if(time > 0.2) 
            {
                mM.PlayBreakUp();
                effect.SetActive(false);
                Destroy(gameObject);
            }
        }

            
    }

    // method to decrease health, called from another script on collision
    public void LooseHealth()
    {
        health -= 1;
    }

    

   
}
