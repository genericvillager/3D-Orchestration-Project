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

    private bool on;

    public AudioSource mainAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        SetupControls();
        mainAudioSource = GetComponent<AudioSource>();
        mainAudioSource.mute = true;

        StartCoroutine(SyncSources());
    }

    private void Update()
    {
        if(AudioListener.pause)
            AudioListener.pause = false;
        
        else if(!AudioListener.pause)
            AudioListener.pause = true;
    }

    void GetAllAudioSources()
    {
        _sources = FindObjectsOfType<AudioSourceController>();

        musicSyncTimeSamples = mainAudioSource.timeSamples;
        if(mainAudioSource.clip != _sources[0].GetComponent<AudioSource>().clip)
            mainAudioSource.clip = _sources[0].GetComponent<AudioSource>().clip;

        mainAudioSource.timeSamples = musicSyncTimeSamples;
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
        
        GetAllAudioSources();
        foreach (AudioSourceController source in _sources)
        {
            source.ToggleMute(true);
        }
    }

    public void ToggleSoloAudioSource(AudioSource audioSource)
    {
        if(!on)
            return;
        
        GetAllAudioSources();
        foreach (AudioSourceController source in _sources)
        {
            if (source.gameObject != audioSource.gameObject)
            {
                source.ToggleMute(true);
            }
        }
        
    }

    public void PlayAllFromStart()
    {
        if(!on)
            return;
        mainAudioSource.Stop();
        mainAudioSource.timeSamples = 0;
        mainAudioSource.Play();
        musicSyncTimeSamples = mainAudioSource.timeSamples;
        GetAllAudioSources();
        foreach (AudioSourceController source in _sources)
        {
            source.PlayNow();
        }
    }

    void Interact(InputAction.CallbackContext context)
    {
        PlayAllFromStart();  
    }

    public void ToggleOnOff()
    {
        if (on)
        {
            on = false;
        }
        else
        {
            on = true;
        }
    }
    
    private IEnumerator SyncSources()
    {
        while (true)
        {
            if (mainAudioSource.timeSamples != 0)
            {
                foreach (AudioSourceController audioSourceController in _sources)
                {
                    audioSourceController.SynchroniseWithMain();
                    yield return null;
                }
            }
        }    
    }
}
