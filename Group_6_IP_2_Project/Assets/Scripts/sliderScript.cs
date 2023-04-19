using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class sliderScript : MonoBehaviour
{
    [SerializeField] private AudioMixer musicMixer;


    public void MusicVolumeSlide(float sliderValue) 
    {
        musicMixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SFXVolumeSlide(float sliderValue)
    {
        musicMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);

    }
}
