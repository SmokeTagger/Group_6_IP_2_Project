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

    // Update is called once per frame
    void Update()
    {
        musicBox = GameObject.FindWithTag("Music");
        mM = musicBox.GetComponent<menuMusic>();
    }

    public void PlayClick() 
    {
        mM.playClick();
    }

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
