using System.Collections.Generic;
using UnityEngine;
using Interfaces.Utils;
using UnityEngine.SceneManagement;

namespace Interfaces.SoundSystem { 
    public class SoundManager : Singleton<SoundManager>
    {
        public enum SoundList
        {
            SFX_UI_TAP,
            SFX_UI_LOCK,
        }

        private Dictionary<SoundList, AudioClipItem> SoundPool = new Dictionary<SoundList, AudioClipItem>();
        public AudioSource sfxAudioPlayer;
        private float SFXVolume;

        protected override void Awake()
        {
            base.Awake();

            if (!sfxAudioPlayer)
            {
                sfxAudioPlayer = gameObject.AddComponent<AudioSource>();
                sfxAudioPlayer.playOnAwake = false;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SFXVolume = PlayerPrefsManager.GetInstance().GetSFXVolume();
        }

        private string GetSoundLocation(SoundList id)
        {
            switch (id) {
                case SoundList.SFX_UI_TAP:
                case SoundList.SFX_UI_LOCK:
                    return "Sounds/" + id.ToString();
                default: return "Path Not Found";
            }
        }

        private AudioClip GetAudioClip(SoundList id)
        {
            if (!SoundPool.ContainsKey(id)) {
                SoundPool[id] = new AudioClipItem(GetSoundLocation(id));
            }
            return SoundPool[id].GetAudioClip();

        }

        private void Play(AudioClip clip, float volume, float pitch)
        {
            sfxAudioPlayer.pitch = pitch;
            sfxAudioPlayer.volume = volume;
            sfxAudioPlayer.PlayOneShot(clip);
        }

        public void PlayThis(SoundList id)
        {
            Play(GetAudioClip(id), SFXVolume, 1.0f);
        }

        public void UpdateVolume(float volume)
        {
            SFXVolume = volume;
            PlayerPrefsManager.GetInstance().SaveSFXVolume(volume);
            //PlayThis(SoundList.SFX_UI_TAP);
        }

    }
}
