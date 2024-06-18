using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;
using UnityEngine.Assertions;

public class AudioInterface : MonoBehaviour
{
    [Serializable]
    public struct ButtonGroup {
        public Button master,
        music,
        ambient,
        voice,
        SFX,
        UI;
    }

    [Serializable]
    public struct SliderGroup {
        public Slider master,
        music,
        ambient,
        voice,
        SFX,
        UI;
    }
    
    [SerializeField] public ButtonGroup buttons;
    [SerializeField] public SliderGroup sliders;

    public AudioMixer mixer;

#region Unity Functions
    void Awake() {
        SetSliderListeners();
        LoadData();
    }
	void OnDisable () {
        //save settings to PlayerPrefs
        SaveData();
	}

#endregion

#region PlayerPrefs
    
    private void SetSliderListeners(){
        sliders.master.onValueChanged.AddListener   (delegate {OnSliderChanged(sliders.master.value,  AudioManager.MASTER_CHANNEL);});
        sliders.music.onValueChanged.AddListener    (delegate {OnSliderChanged(sliders.music.value,   AudioManager.MUSIC_CHANNEL);});
        sliders.ambient.onValueChanged.AddListener  (delegate {OnSliderChanged(sliders.ambient.value, AudioManager.AMBIENT_CHANNEL);});
        sliders.voice.onValueChanged.AddListener    (delegate {OnSliderChanged(sliders.voice.value,   AudioManager.VOICE_CHANNEL);});
        sliders.SFX.onValueChanged.AddListener      (delegate {OnSliderChanged(sliders.SFX.value,     AudioManager.SFX_CHANNEL);});
        sliders.UI.onValueChanged.AddListener       (delegate {OnSliderChanged(sliders.UI.value,      AudioManager.UI_CHANNEL);});
    }

    //Save data to PlayerPrefs
    private void SaveData(){
        PlayerPrefs.SetFloat(AudioManager.MASTER_CHANNEL , sliders.master.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_CHANNEL  , sliders.music.value);
        PlayerPrefs.SetFloat(AudioManager.AMBIENT_CHANNEL, sliders.ambient.value);
        PlayerPrefs.SetFloat(AudioManager.VOICE_CHANNEL  , sliders.voice.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_CHANNEL    , sliders.SFX.value);
        PlayerPrefs.SetFloat(AudioManager.UI_CHANNEL     , sliders.UI.value);
    }

    //Load data from PlayerPrefs, if no save data is found the default value to 1.0f
    private void LoadData(){
         sliders.master.value  = PlayerPrefs.GetFloat(AudioManager.MASTER_CHANNEL,  1.0f);
         sliders.music.value   = PlayerPrefs.GetFloat(AudioManager.MUSIC_CHANNEL,   1.0f);
         sliders.ambient.value = PlayerPrefs.GetFloat(AudioManager.AMBIENT_CHANNEL, 1.0f);
         sliders.voice.value   = PlayerPrefs.GetFloat(AudioManager.VOICE_CHANNEL,   1.0f);
         sliders.SFX.value     = PlayerPrefs.GetFloat(AudioManager.SFX_CHANNEL,     1.0f);
         sliders.UI.value      = PlayerPrefs.GetFloat(AudioManager.UI_CHANNEL,      1.0f);
    }
#endregion

    /*
    * Callback function, when a slider value is changed, set the selected mixer channel value.
    * @argument <float> value - linear scaling to be converted to logarithmic value for audio
    * @argument <string> channel - the selected mixer group
    */
    private void OnSliderChanged(float value, string channel){
        //convert linear value into logarithmic to be used with decibel units
        float scaledValue = Mathf.Log10(value) * 20;
        if(value <= 0.0001f) scaledValue = -80.0f;

        //make sure a channel is selected
        Assert.IsNotNull(channel);
        mixer.SetFloat(channel, scaledValue);
    }
}


    
