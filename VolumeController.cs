using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider VolumeSlider;

    public void SetMasterVolume()
    {
        float volume = VolumeSlider.value;
        myMixer.SetFloat("Volume",Mathf.Log10(volume)*20);
    }
}
