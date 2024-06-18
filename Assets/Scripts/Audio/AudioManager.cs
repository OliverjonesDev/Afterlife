using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    //keys for saving mixer data in playPrefs
    public const string MASTER_CHANNEL     = "MasterVolume";
    public const string MUSIC_CHANNEL      = "MusicVolume";
    public const string AMBIENT_CHANNEL    = "AmbientVolume";
    public const string VOICE_CHANNEL      = "VoiceVolume";
    public const string SFX_CHANNEL        = "SfxVolume";
    public const string UI_CHANNEL         = "UiVolume";

    public bool isWalking = false;

#region Private  
    private float musicA, musicB, time;
    [SerializeField] private AudioSource SFXSource, MusicSourceA, MusicSourceB, UISource;
    private bool trackPlaying = true;
    private bool CoroutineRunning = false;
    
    [SerializeField] AudioClip[] typingSounds;
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip startMusic;
    [SerializeField] AudioClip startAmbience;
    [SerializeField] private AudioSource roomTone;
    [SerializeField] private AudioSource subtitleAudio;
    [SerializeField] private AudioSource walkingAudio;

#endregion

#region Unity Functions
    //create singleton
    //don't create more than once instance of this class
    //you can access this class from other scripts using AudioManager.Instance, no need to find the gameobject
    public static AudioManager Instance;
    void Awake(){
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
        //Set Audio Mixer values with data loaded from PlayerPrefs
        LoadData(MASTER_CHANNEL);
        LoadData(MUSIC_CHANNEL);
        LoadData(AMBIENT_CHANNEL);
        LoadData(VOICE_CHANNEL);
        LoadData(SFX_CHANNEL);
        LoadData(UI_CHANNEL);
    }

    void Start()
    {
        musicA = 1.0f;
        musicB = 0.0f;

        //start looping audio
        if(startMusic != null) SetMusic(startMusic);
        if(startAmbience != null) roomTone.clip = startAmbience;
        roomTone.Play();
    }

#endregion

#region PlayerPrefs
//find data saved using key and set the mixer to that value
public void LoadData(string key)
{
    float volume = PlayerPrefs.GetFloat(key, 1.0f);
    volume = Mathf.Log10(volume) * 20;
    mixer.SetFloat(key, volume);
}
#endregion

#region SoundTriggers

    public void PlaySFX(AudioClip audioClip){
        SFXSource.PlayOneShot(audioClip);
    }

    public void PlayUI(AudioClip audioClip){
        UISource.PlayOneShot(audioClip);
    }

    public void PlayRandomSound(AudioClip[] clipsArray, AudioSource source){
        int index = UnityEngine.Random.Range(0,clipsArray.Length);
        source.PlayOneShot(clipsArray[index]);
    }
    public void PlaySelectSound(){
        PlayUI(selectSound);
    }
    
    public void PlaySubtitleAudio(){
        PlayRandomSound(typingSounds, subtitleAudio);
    }

    public void PauseWalking(){
        
        walkingAudio.Stop();
    }

    public void PlayWalking(){
        
        if(walkingAudio.isPlaying) return;
        walkingAudio.Play();
    }

    public void SwitchRoomTone(AudioClip clip){
        if(clip == roomTone.clip) return;
        roomTone.clip = clip;
        roomTone.Play();
    }

#endregion

#region Music Triggers
    public void SetMusic(AudioClip audioClip){
        if(CoroutineRunning) return;
        //set volumes
        musicA = 1.0f;
        musicB = 0.0f;
        MusicSourceA.volume = 1.0f;
        MusicSourceB.volume = 0.0f;

        MusicSourceA.Stop(); 
        MusicSourceB.Stop();

        //Always play on left channel
        MusicSourceA.clip = audioClip;
        MusicSourceA.Play();
        trackPlaying = true;
    }

    public void StopMusic(){
        MusicSourceA.Stop();
        MusicSourceB.Stop();
    }

    public void FadeMusic(AudioClip audioClip, float duration){
        if(CoroutineRunning) return;
        if (trackPlaying) StartCoroutine(MusicFadeRight(duration, audioClip));
        else              StartCoroutine(MusicFadeLeft (duration, audioClip));
        trackPlaying = !trackPlaying;
    }

    IEnumerator MusicFadeLeft(float duration, AudioClip audioClip)
    {
        CoroutineRunning = true;

        float time = 0.0f;
        
        MusicSourceA.clip = audioClip;
        MusicSourceA.Play();
        
        while(time < duration){
            musicA = Mathf.Lerp(0.0f, 1.0f, time / duration);
            musicB = Mathf.Lerp(1.0f, 0.0f, time / duration);

            MusicSourceA.volume = musicA; 
            MusicSourceB.volume = musicB;

            time += Time.deltaTime;
            yield return null;
        }

        musicA = 1.0f;
        musicB = 0.0f;
        MusicSourceB.Stop();
        CoroutineRunning = false;
    }
    
    IEnumerator MusicFadeRight(float duration, AudioClip audioClip)
    {
        CoroutineRunning = true;

        float time = 0.0f;

        MusicSourceB.clip = audioClip;
        MusicSourceB.Play();

        while(time < duration){
            musicA = Mathf.Lerp(1.0f, 0.0f, time / duration);
            musicB = Mathf.Lerp(0.0f, 1.0f, time / duration);

            MusicSourceA.volume = musicA;
            MusicSourceB.volume = musicB;

            time += Time.deltaTime;
            yield return null;
        }

        musicA = 0.0f;
        musicB = 1.0f;
        MusicSourceA.Stop();
        CoroutineRunning = false;
    }

#endregion
}
