using UnityEngine;
using System.Collections;

public class scr_soundManager : MonoBehaviour {
    //MusicSource
    public AudioSource musicSource;
    //SFXSource
    public AudioSource SFXSource;
    //ThisGameObject
    public static scr_soundManager instance = null;
    //MainMenuMusic
    public AudioClip snd_mainMenuMusic;
    //GameMusic
    public AudioClip snd_gameMusic;
    //ButtonClick
    public AudioClip snd_buttonClick;

    // Use this for initialization
    void Awake () {
        //SetInstanceToThisGameObjectIsIstanceIsNull
        if(instance == null)
        {
            instance = this;
        }
        //IfInstanceIsNotThisObjectDestroyObject
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyGameObjectWhenLoadingScenes
        DontDestroyOnLoad(gameObject);
	}
	
    //PlayAudioFileOnce
    public void PlayOnce(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    //PlayButtonClick
    public void playButtonClick()
    {
        //PlaySoundEffct
        PlayOnce(snd_buttonClick);
    }

    //PlayMusic
    public void playMusic(AudioClip clip){
        //SetMusicClip
        musicSource.clip = clip;
        //PlayMusicFile
        musicSource.Play();
    }

    //StopMusic
    public void stopMusic()
    {
        musicSource.Stop();
    }

    //PlayMenuMusic
    public void playMenuMusic()
    {
        playMusic(snd_mainMenuMusic);
    }

    //PlayGameMusic
    public void playGameMusic()
    {
        playMusic(snd_gameMusic);
    }
}
