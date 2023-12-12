using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    private void Start()
    {
        sliderMaster.value = SoundManager.Instance.masterVolume;
        sliderBGM.value = SoundManager.Instance.bgmVolume;
        sliderSFX.value = SoundManager.Instance.sfxVolume;
        sliderMaster.onValueChanged.AddListener(Set_MasterVolume);
        sliderBGM.onValueChanged.AddListener(Set_BGMVolume);
        sliderSFX.onValueChanged.AddListener(Set_SFXVolume);
    }

    private void Set_MasterVolume(float sliderVal)
    {
        SoundManager.Instance.masterVolume = sliderVal;
        SoundManager.Instance.bgmSource.volume = sliderVal * sliderBGM.value;
        SoundManager.Instance.sfxSource.volume = sliderVal * sliderSFX.value;
    }

    private void Set_BGMVolume(float sliderVal)
    {
        SoundManager.Instance.bgmVolume = sliderVal;
        SoundManager.Instance.bgmSource.volume = sliderVal * sliderMaster.value;
    }

    private void Set_SFXVolume(float sliderVal)
    {
        SoundManager.Instance.sfxVolume = sliderVal;
        SoundManager.Instance.sfxSource.volume = sliderVal * sliderMaster.value;
    }
}
