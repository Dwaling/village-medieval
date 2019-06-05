using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour
{
    public Slider thisSlider;
    public float musicVolume;
    public float SFXVolume; 
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetSpecificVolume(string whatValue)
    {
        float SliderValue = thisSlider.value;

        if(whatValue == "Music")
        {
            musicVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        }

        if (whatValue == "SFX")
        {
            SFXVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVolume", SFXVolume);
        }
    }
}
