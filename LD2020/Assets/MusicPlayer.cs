using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer _instance;
    public bool _sfxMuted = false;
    public bool _musicMuted = false;
    public AudioSource _audio;

    public List<AudioSource> PlayerDeathSounds;
    public List<AudioSource> WolfGrowlSounds;
    public List<AudioSource> WolfChaseSounds;
    public List<AudioSource> SheepBounceSounds;
    public List<AudioSource> SheepDeathSounds;
    public List<AudioSource> SheepKickSounds;
    public AudioSource SheepEnterHoleSound;
    public AudioSource SheepExitHoleSound;


    public AudioSource LevelCompleteSound;

    public static MusicPlayer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicPlayer>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("Playing");
        Play();
    }

    public void Play()
    {
        //Play some audio!
        _audio.Play();
    }

    public void PlayPlayerDeathSound(int numb)
    {
        PlaySound(PlayerDeathSounds[numb]);
    }

    public void PlayWolfGrowlSound(int numb)
    {
        PlaySound(WolfGrowlSounds[numb]);
    }

    public void PlayWolfChaseSound(int numb)
    {
        PlaySound(WolfChaseSounds[numb]);
    }

    public void PlaySheepBounceSound(int numb)
    {
        PlaySound(SheepBounceSounds[numb]);
    }

    public void PlaySheepDeathSound(int numb)
    {
        PlaySound(SheepDeathSounds[numb]);
    }

    public void PlaySheepEnterHoleSound()
    {
        PlaySound(SheepEnterHoleSound);
    }

    public void PlaySheepExitHoleSound()
    {
        PlaySound(SheepExitHoleSound);
    }

    public void PlayLevelCompleteSound()
    {
        PlaySound(LevelCompleteSound);
    }

    public void PlaySheepKickSound(int numb)
    {
        PlaySound(SheepKickSounds[numb]);
    }

    public void MuteMusic()
    {
        _audio.mute = true;
        _musicMuted = true;
    }

    public void MuteSfx()
    {
        _sfxMuted = true;
    }

    public void UnMuteMusic()
    {
        _audio.mute = false;
        _musicMuted = false;
    }

    public void UnMuteSfx()
    {
        _sfxMuted = false;
    }

    private void PlaySound(AudioSource source)
    {
        if (!_sfxMuted)
        {
            source.PlayOneShot(source.clip);
        }
    }
}
