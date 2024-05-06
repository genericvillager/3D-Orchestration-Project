using System;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public bool isSolo = false; 
    public RoomManager roomManager;
    Controls _controls;

    AudioSourceController[] _sources;
    
    public int musicSyncTimeSamples = 0;

    public bool on = false;

    public AudioSource mainAudioSource;
    private AudioListener _audioListener;
    public float musicReactionValue;
    // Start is called before the first frame update
    void Start()
    {
        SetupControls();
        mainAudioSource = GetComponent<AudioSource>();
        mainAudioSource.mute = true;
        _audioListener = FindObjectOfType<AudioListener>();
        GetAllAudioSources();
        ResetAll();
    }

    private void Update()
    {
        if(on)
            UpdateMusicReaction();
    }

    void UpdateMusicReaction()
    {
        float rms;
        float dbv;
        const int QSAMPLES = 128;
        const float REFVAL = 0.1f;  // RMS for 0 dB
 
        float[] samples = new float[QSAMPLES];
 
        AudioListener.GetOutputData(samples, 0);
 
        float sqrSum = 0.0f;
 
        int i = QSAMPLES; while (i --> 0) {
 
            sqrSum += samples[i] * samples[i];
        }
 
        rms = Mathf.Sqrt(sqrSum/QSAMPLES); // rms value 0-1
        dbv = 20.0f*Mathf.Log10(rms/REFVAL); // dB value
        musicReactionValue = dbv;
    }

    public void GetAllAudioSources()
    {
        _sources = FindObjectsOfType<AudioSourceController>();

        mainAudioSource.Stop();
        musicSyncTimeSamples = mainAudioSource.timeSamples;
        if (_sources.Length == 0)
            return;
        if(mainAudioSource.clip != _sources[0].GetComponent<AudioSource>().clip)
            mainAudioSource.clip = _sources[0].GetComponent<AudioSource>().clip;

        mainAudioSource.timeSamples = musicSyncTimeSamples;
        mainAudioSource.Play();
        mainAudioSource.loop = true;
    }

    void SetupControls()
    {
        _controls = new Controls();
        _controls.Enable();

        _controls.Room.EnvironmentControl.performed += ctx => ChangeEnv(ctx);
        _controls.Room.PauseToggle.performed += ctx => TogglePauseMusic();
        _controls.Room.Interact.performed += ctx => Interact(ctx);
    }
    void ChangeEnv(InputAction.CallbackContext context)
    {
        roomManager.CycleEnv();
    }

    public void TogglePauseMusic()
    {
        if(!on)
            return;
        
        if(AudioListener.pause)
            AudioListener.pause = false;
        else
            AudioListener.pause = true;
    }

    public void ToggleSoloAudioSource(AudioSource audioSource)
    {
        if(!on)
            return;
        
        foreach (AudioSourceController source in _sources)
        {
            if (source.gameObject != audioSource.gameObject)
            {
                source.ToggleMute();
            }
        }
        
    }

    public void PlayAllFromStart()
    {
        if(!on)
            return;
        
        mainAudioSource.timeSamples = 0;
        musicSyncTimeSamples = mainAudioSource.timeSamples;
        foreach (AudioSourceController source in _sources)
        {
            source.PlayNow();
        }
    }

    void Interact(InputAction.CallbackContext context)
    {
        PlayAllFromStart();  
    }

    private void ResetAll()
    {
        foreach (AudioSourceController source in _sources)
        {
            source.ResetAudioSource();
        }
        mainAudioSource.Stop();
        mainAudioSource.timeSamples = 0;
        AudioListener.pause = false;
        musicSyncTimeSamples = 0;
    }

    public void ToggleOnOff()
    {
        if (on)
        {
            on = false;
            ResetAll();
        }
        else if(!on)
        {
            on = true;
            GetAllAudioSources();
            mainAudioSource.Play();
        }
    }
}
