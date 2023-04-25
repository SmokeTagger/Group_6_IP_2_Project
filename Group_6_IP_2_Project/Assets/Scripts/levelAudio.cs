using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class levelAudio : MonoBehaviour
{
    public GameObject musicBox;
    public menuMusic mM;

    public Slider levelMusic;
    public Slider levelSFX;
    public Slider LevelBrightness;

    // grabs the music box when carries over from the frist scene
    void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
    }

    // method to be attached to buttons to playt the sound from the music box
    public void PlayClick() 
    {
        mM.playClick();
    }

    // method to be attached to the start game buton to switch out the background music to the battle music selceted by a random number
    public void Music()
    {


        if (musicBox != null)
        {
            mM.stopBackground();


            int rand = Random.Range(1, 3);

            switch (rand)
            {
                case 1:
                    mM.playBattle1();
                    break;

                case 2:
                    mM.playBattle2();
                    break;

                case 3:
                    mM.playBattle3();
                    break;
            }

            levelMusic.value = mM.musicValue;
            levelSFX.value = mM.sfxValue;
            LevelBrightness.value = mM.brightnessValue;
        }
    }

}
