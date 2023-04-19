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

    void Start()
    {
        brightnessprofile.TryGetSettings(out exposure);
        AdjustBrghtness(brightnessSlider.value);
    }

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
