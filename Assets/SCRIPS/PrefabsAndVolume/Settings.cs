using Interfaces.SoundSystem;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider SFXVolumeSlider;

    private void Start()
    {
        SFXVolumeSlider.value = PlayerPrefsManager.GetInstance().GetSFXVolume();
        SFXVolumeSlider.onValueChanged.AddListener(DelegateVolumeUpdated);
    }

    public void DelegateVolumeUpdated(float newVolume)
    {
        SoundManager.GetInstance().UpdateVolume(newVolume);
    }
}
