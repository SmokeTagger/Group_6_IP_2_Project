using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerEnergy : MonoBehaviour
{
    // Energy Variables 
    [SerializeField] int energy;
    [SerializeField] int energyMax;
    public TextMeshProUGUI energyMeter;
    public GameObject player; // grabs the player character 
    public bool activeSuper; // this controls if the super can be activated or not 

    // Start is called before the first frame update
    void Start()
    {
        activeSuper = false; // starts the game with supers deactivated
        energyMeter.text = " Energy : " + energy;
        energy = 0; // minimum enrergy the player can have
        energyMax = 80; // max energy that can be gained 
    }

    // Update is called once per frame
    void Update()
    {
        if (energy == energyMax) // if the energy ever reaches the maximum energy it will allow the player to use their super attacks
        {
            energy = energyMax;
            activeSuper = true;
            energyMeter.text = " Energy : " + energy;
        }
    }

    // these code segments show how much energy is gained from each attack 
    public void Energylite()
    {
        energy += 20;
        energyMeter.text = " Energy : " + energy;
    }

    public void Energyhvy()
    {
        energy += 40;
        energyMeter.text = " Energy : " + energy;
    }

    public void Energydown()
    {
        energy += 20;
        energyMeter.text = " Energy : " + energy;
    }

    public void EnergyReset() // if the super is used this function resets the current energy to 0 and disables the use of supers 
    {
        energy = 0;
        activeSuper = false;
        energyMeter.text = " Energy : " + energy;
    }
}
