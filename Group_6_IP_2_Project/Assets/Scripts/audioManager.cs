using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource rifleShot; // varaible to store objects with audio sources 
    public AudioSource rifleBack;
    public AudioSource launcher;
    public AudioSource launcherBack;
    public AudioSource pistol;
    public AudioSource pistolBack;
    public AudioSource pistolReload;
    public AudioSource rifleReload;
    public AudioSource launcherReload;
    public AudioSource pickUp;
    public AudioSource running;
    public AudioSource click;
    public AudioSource blood;
    public AudioSource turret;
    public AudioSource intruder;

    public void PlayRifle() // sereis of functions that can be called from other scripts to player the sounds stored in the audiosources 
    {
        rifleShot.PlayOneShot(rifleShot.clip, 0.5f);
    }

    public void PlayRifleBack()
    {
        rifleBack.PlayOneShot(rifleBack.clip, 0.5f);
    }

    public void PlayLauncher()
    {
        launcher.PlayOneShot(launcher.clip, 1);
    }

    public void PlayLauncherBack()
    {
        launcherBack.PlayOneShot(launcherBack.clip, 1);
    }

    public void PlayPistol()
    {
        pistol.PlayOneShot(pistol.clip, 1);
    }

    public void PlayPistolBack()
    {
        pistolBack.PlayOneShot(pistolBack.clip, 1);
    }

    public void PlayPistolReload()
    {
        pistolReload.PlayOneShot(pistolReload.clip, 1);
    }
    
    public void PlayRifleReload()
    {
        rifleReload.PlayOneShot(rifleReload.clip, 1);
    }

    public void PlayLauncherReload()
    {
        launcherReload.PlayOneShot(launcherReload.clip, 1);
    }

    public void PlayPickUp()
    {
        pickUp.PlayOneShot(pickUp.clip, 1);
    }

    public void PlayClick()
    {
        click.PlayOneShot(click.clip, 1);   
    }

    public void PlayBlood()
    {
        blood.PlayOneShot(blood.clip, 1);
    }

    public void PlayTurret()
    {
        turret.PlayOneShot(turret.clip, 1); 
    }

    public void PlayIntruder()
    {
        intruder.PlayOneShot(intruder.clip, 1);
    }

    public void PlayRunning() // slightly differnt function using play rather than playoneshot as it is constantly looping when.
    {
        running.Play();
    }

    public void StopRunning() // function to stop the running sound 
    {
        running.Stop();
    }

}
