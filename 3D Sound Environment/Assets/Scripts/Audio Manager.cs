using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public bool isSolo = false;
    public RoomManager RM;
    Controls controls;
    private GameObject playerCamera;

    AudioSourceController[] sources;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera");
    }

    void GetAllAudioSources()
    {
        sources = FindObjectsOfType<AudioSourceController>();
    }

    void SetupControls()
    {
        controls = new Controls();
        controls.Enable();

        controls.Room.EnvironmentControl.performed += ctx => ChangeEnv(ctx);
        controls.Room.PauseToggle.performed += ctx => TogglePauseMusic(ctx);
        controls.Room.Interact.performed += ctx => Interact(ctx);
    }
    void ChangeEnv(InputAction.CallbackContext context)
    {
        RM.CycleEnv();
    }

    void TogglePauseMusic(InputAction.CallbackContext context)
    {
        if(AudioListener.pause)
            AudioListener.pause = false;
        else
            AudioListener.pause = true;
    }

    public void ToggleSoloAudioSource(AudioSource AS)
    {
        GetAllAudioSources();
        float value;
        if(isSolo)
        {
            isSolo = false;
            value = 1;
        }
        else
        {
            isSolo = true;
            value = 0;
        }

        foreach (AudioSourceController source in sources)
        {
            if (source != AS)
            {
                source.ChangeAudio(value);
            }
        }
        
    }

    public void PlayAll()
    {
        GetAllAudioSources();
        print("playing all");
        foreach (AudioSourceController source in sources)
        {
            source.PlayNow();
        }
    }

    void Interact(InputAction.CallbackContext context)
    {
        PlayAll();  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
