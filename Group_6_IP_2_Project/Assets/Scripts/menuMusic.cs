using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour
{

    public AudioSource background;

    public AudioSource battle1;
    public AudioSource battle2;
    public AudioSource battle3;

    public AudioSource buttonClick;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void start() 
    {
        playBackground();
    }

    public void playBackground() { background.Play(); }

    public void stopBackground() { background.Stop(); }

    public void playBattle1() { battle1.Play(); }

    public void playBattle2() { battle2.Play(); }

    public void playBattle3() { battle3.Play(); }

    public void stopBattle() { battle1.Stop(); battle2.Stop(); battle3.Stop(); }

    public void playClick() { buttonClick.Play(); }


}
