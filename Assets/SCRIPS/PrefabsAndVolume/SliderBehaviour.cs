using Interfaces.SoundSystem;
using UnityEngine;
using UnityEngine.UI;
using static Interfaces.SoundSystem.SoundManager;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite OnIcon;
    [SerializeField] private Sprite OffIcon;
    [SerializeField] private Image Icon;
    [SerializeField] private Slider SFXVolumeSlider;

    public void ReproduceSound()
    {
        SoundManager.GetInstance().PlayThis(SoundManager.SoundList.SFX_UI_TAP);
    }

    private void Start()
    {
        SFXVolumeSlider.onValueChanged.AddListener(DelegateValueChanged);
    }

    public void DelegateValueChanged(float newValue)
    {
        if(newValue <= 0 )
            Icon.sprite = OffIcon;
        else
            Icon.sprite = OnIcon;   

    }
}
