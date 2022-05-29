using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

   [SerializeField] private AudioSource _adgangSource, _begrænsetSource, _ingenSource;

    
   void Awake() 
   {
       if (Instance == null) 
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
       else
       {
           Destroy(gameObject);
       }
    }
    public void PlayAdgangSound()
    {
        _adgangSource.Play();
    }
     public void PlayBegrænsetSound()
    {
        _begrænsetSource.Play();
    }
     public void PlayIngenSound()
    {
        _ingenSource.Play();
    }
 //CAN BE USED LATER

    // public void ChangeMasterVolume(float value)
    // {
    //     AudioListener.volume = value;
    // }

    // public void ToggleAdgang()
    // {
    //     _adgangSource.mute = !_adgangSource.mute;
    // }

    // public void ToggleBegrænset()
    // {
    //     _begrænsetSource.mute = !_begrænsetSource.mute;
    // }

    // public void ToggleIngen()
    // {
    //     _ingenSource.mute = !_ingenSource.mute;
    // }
}
