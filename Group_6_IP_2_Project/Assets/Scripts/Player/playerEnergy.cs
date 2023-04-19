using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerEnergy : MonoBehaviour
{
    [SerializeField] int energy = 0;
    [SerializeField] int energyMax = 100;
    public TextMeshProUGUI energyMeter;
    public GameObject player;
    public bool activeSuper;

    // Start is called before the first frame update
    void Start()
    {
        activeSuper = false;
        energyMeter.text = " Energy : " + energy;
        energy = 0;
        energyMax = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (energy >= energyMax)
        {
            energy = 100;
            activeSuper = true;
            energyMeter.text = " Energy : " + energy;
        }
    }

    public void Energylite()
    {
        energy += 10;
        energyMeter.text = " Energy : " + energy;
    }

    public void Energyhvy()
    {
        energy += 25;
        energyMeter.text = " Energy : " + energy;
    }

    public void Energydown()
    {
        energy += 15;
        energyMeter.text = " Energy : " + energy;
    }

    public void EnergyReset()
    {
        energy = 0;
        activeSuper = false;
        energyMeter.text = " Energy : " + energy;
    }
}
