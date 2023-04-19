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

    public AudioSource buttonClick;

    public Slider mainmusic;
    public Slider mainsfx;
    public Slider mainBrightness;
    public float musicValue;
    public float sfxValue;
    public float brightnessValue;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void start() 
    {
        playBackground();

    }

    private void Update()
    {
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


}
