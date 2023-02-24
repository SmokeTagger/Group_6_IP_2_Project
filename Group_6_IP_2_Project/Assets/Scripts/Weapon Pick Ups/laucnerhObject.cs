using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to provide functionality the launcher object placed in the enviroment allwoing the player to "pick them up"
/// </summary>
public class laucnerhObject : MonoBehaviour
{
    manager managerScript; //references to acces class's from other scripts
    audioManager audioScript;

    public GameObject rifleObj; //refernecs to the charachter weapons
    public GameObject pistolObj;
    public GameObject launcherObj;

    void Awake() // get the class's form other scripts to play sounds and acces variables in the manager
    {
        managerScript = GameObject.FindGameObjectWithTag("ManagerTag").GetComponent<manager>();
        audioScript = GameObject.FindGameObjectWithTag("AudioManagerTag").GetComponent<audioManager>();
    }

    void Update() // set the object spinning in place 
    {
        transform.Rotate(0, 0.5f, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") // runs the code if the player collides with the gun
        {
            audioScript.PlayPickUp(); //plays a pick upsound
            managerScript.launcherOn = true; //sets a bool in the manager script so we can switch between weapons
            rifleObj.gameObject.SetActive(false); //these activate the players launcher and deactivate thier other weapons so that when the player picks up the gun it presents the rifle and puts awawy aything else the might be holding
            pistolObj.gameObject.SetActive(false);
            launcherObj.gameObject.SetActive(true);
            Destroy(gameObject); //destroys the pickup

        }
    }
}
