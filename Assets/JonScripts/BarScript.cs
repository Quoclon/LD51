using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    public Slider slider;

    public void SetSliderValueMax(float sliderValueMax)
    {
        
        slider.maxValue = sliderValueMax;
        slider.value = sliderValueMax;

    }

    public void SetSliderValue(float sliderValue)
    {
        slider.value = sliderValue;
    
    }

    
}
