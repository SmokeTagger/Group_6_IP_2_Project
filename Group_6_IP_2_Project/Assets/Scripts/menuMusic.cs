using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuMusic : MonoBehaviour
{

    public AudioSource background;

    public AudioSource battle1;
    public AudioSource battle2;
    public AudioSource battle3;
    public AudioSource fighterHeavy;
    public AudioSource fighterLight;
    public AudioSource fighterJump;
    public AudioSource fighterSwing;
    public AudioSource wizardHeavy;
    public AudioSource wizardLight;
    public AudioSource wizardJump;
    public AudioSource wizardSwing;
    public AudioSource super;
    public AudioSource grenade;
    public AudioSource healthUp;
    public AudioSource pickUp;

    public AudioSource buttonClick;

    GameObject mainMusicSlider;
    Slider mainmusic;
    GameObject mainSFXSlider;
    Slider mainsfx;
    GameObject mainBrightenssSlider;
    Slider mainBrightness;

    public float musicValue;
    public float sfxValue;
    public float brightnessValue;

    void Start() 
    {
        playBackground();

    }

    private void Update()
    {
        mainMusicSlider = GameObject.Find("Music Volume");
        mainmusic = mainMusicSlider.GetComponent<Slider>();
        mainSFXSlider = GameObject.Find("SFX Volume");
        mainsfx = mainSFXSlider.GetComponent<Slider>();
        mainBrightenssSlider = GameObject.Find("Brightenes");
        mainBrightness = mainBrightenssSlider.GetComponent<Slider>();


        sfxValue = mainsfx.value;
        musicValue = mainmusic.value;
        brightnessValue = mainBrightness.value;
    }

    public void playBackground() { background.Play(); }

    public void stopBackground() { background.Stop(); }

    public void playBattle1() { battle1.Play(); }

    public void playBattle2() { battle2.Play(); }

    public void playBattle3() { battle3.Play(); }

    public void stopBattle() { battle1.Stop(); battle2.Stop(); battle3.Stop(); }

    public void playClick() { buttonClick.Play(); }

    public void PlayFighterLight() { fighterLight.Play(); }

    public void PlayFighterHeavy() { fighterHeavy.Play(); }

    public void PlayFighterJump() { fighterJump.Play(); }

    public void PlayFighterSwing() { fighterSwing.Play(); }

    public void PlayWizardLight() { wizardLight.Play(); }

    public void PlayWizardHeavy() { wizardHeavy.Play(); }

    public void PlayWizardJump() { wizardJump.Play(); }

    public void PlayWizardSwing() { wizardSwing.Play(); }

    public void PlaySuper() { super.Play(); }

    public void PlayGrenade() { grenade.Play(); }

    public void PlayHealthUp() { healthUp.Play(); }

    public void PlayPickUp() { pickUp.Play(); }
}
