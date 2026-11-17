using Interfaces.Utils;
using UnityEngine;

public class PlayerPrefsManager : Singleton<PlayerPrefsManager>
{

    enum playerPrefsNames
    {
        SFXVolume,
    }
    private static float SFXVolume;
    protected override void Awake()
    {
        base.Awake();
        SFXVolume = PlayerPrefs.GetFloat(playerPrefsNames.SFXVolume.ToString(), 1);
        
        DontDestroyOnLoad(gameObject);
    }

    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public float SaveSFXVolume(float newVolume)
    {
        SFXVolume = newVolume;
        PlayerPrefs.SetFloat(playerPrefsNames.SFXVolume.ToString(), newVolume);
        return SFXVolume;
    }

    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("All PlayerPrefs have been reset!");
    }
}
