using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class brightness : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightnessprofile;
    public PostProcessLayer layer;

    AutoExposure exposure;
    // gets the exposure value of the brightness post proces and assigns it the slider value on start
    void Start()
    {
        brightnessprofile.TryGetSettings(out exposure);
        AdjustBrghtness(brightnessSlider.value);
    }

    //used to assing the exposure value of the brightness prost proces is set to the brightenes slider value and ensure that if the slider value goes below 0.5 that the screen brightness value stays at 0.5

    public void AdjustBrghtness(float value)
    {

        if (value > 0.5) 
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.5f;
        }
    }
}
