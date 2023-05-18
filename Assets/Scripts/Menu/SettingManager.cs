
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace ErfanDeveloper
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private Slider audioSettingSlider;
        
        public void ChangeVolume(float sliderValue)
        {
            mixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
        }
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex + 1);
        }
        
    }
}