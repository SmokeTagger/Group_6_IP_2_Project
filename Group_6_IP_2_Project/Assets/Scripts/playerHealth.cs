using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    [SerializeField] int health = 200;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);    
        }
    } 

    public void healthlite()
    {
        health -= 10;
    } 

    public void healthhvy()

    {
        health -= 30;
    }

    public void healthdown()
    {
        health -= 20;
    }
}
