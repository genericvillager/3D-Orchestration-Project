using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AudioManager : MonoBehaviour
{
    public bool isSolo = false; 
    public RoomManager roomManager;
    Controls _controls;

    public List<AudioSourceController> _sources;
    
    public int musicSyncTimeSamples = 0;

    public bool on = false;

    public AudioSource mainAudioSource;
    private AudioListener _audioListener;
    public float musicReactionValue;
    
    [SerializeField] private GameObject canvasHolder;
    [SerializeField] private GameObject content;
    private GameObject currentCanvas;

    private Dictionary<string, Vector2> audioSourceControllerParametersDefault = new Dictionary<string, Vector2>
    {
        { "vol", new Vector2(0,1)},
        { "pitch", new Vector2(-3,3) },
        { "stereoPan", new Vector2(-1,1) },
        { "spatialBlend", new Vector2(0,1) },
        { "reverbZoneMix", new Vector2(0,1.1f) },
        { "dopplerLevel", new Vector2(0,5) },
        { "spread", new Vector2(0,360) },
        { "dryLevel", new Vector2(-10000,0) },
        { "room", new Vector2(-10000,0) },
        { "roomHF", new Vector2(-10000,0) },
        { "roomLF", new Vector2(-10000,0) },
        { "decayTime", new Vector2(0.01f,20) },
        { "decayHFRatio", new Vector2(0.01f,2) },
        { "reflectionsLevel", new Vector2(-10000,1000) },
        { "reflectionsDelay", new Vector2(0,.3f) },
        { "reverbLevel", new Vector2(-10000,2000) },
        { "reverbDelay", new Vector2(0,.1f) },
        { "hfReference", new Vector2(1000,20000) },
        { "lfReference", new Vector2(20,1000) },
        { "diffusion", new Vector2(0,100) },
        { "density", new Vector2(0,100) }
    };

    private OVRManager _ovrManager;
        
    // Start is called before the first frame update
    void Start()
    {
        SetupControls();
        mainAudioSource = GetComponent<AudioSource>();
        mainAudioSource.mute = true;
        _audioListener = FindObjectOfType<AudioListener>();
        ResetAll();
        
        _ovrManager = OVRManager.instance;

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

    public void GetMainAudioSource()
    {
        
        mainAudioSource.Stop();
        if (_sources.Count == 0)
            return;

        mainAudioSource.clip = _sources[0].GetComponent<AudioSource>().clip;

        musicSyncTimeSamples = mainAudioSource.timeSamples;

        mainAudioSource.Play();
        mainAudioSource.loop = true;
    }

    void SetupControls()
    {
        _controls = new Controls();
        _controls.Enable();

        /*
        _controls.Room.EnvironmentControl.performed += ctx => ChangeEnv(ctx);
        _controls.Room.PauseToggle.performed += ctx => TogglePauseMusic();
        _controls.Room.Interact.performed += ctx => Interact(ctx);
        */
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
            GetMainAudioSource();
            on = true;
            mainAudioSource.Play();
        }
    }

    public void InitAudioSourceParameters(Transform parent)
    {
        currentCanvas = Instantiate(canvasHolder, parent,false);
        currentCanvas.transform.localPosition = new Vector3(0,1,.3f);
        currentCanvas.transform.localRotation= Quaternion.Euler(0,180,0);
        
        PopulateAudioSourceParameters();
    }
    
    public void CloseAudioSourceParameters()
    {
        Destroy(currentCanvas);
    }

    private void PopulateAudioSourceParameters()
    {
        foreach (KeyValuePair<string,Vector2> parameter in audioSourceControllerParametersDefault)
        {
            AudioSourceParameterSlider con = Instantiate(content, currentCanvas.transform.Find("Canvas/Panel/ScrollArea/Content"),
                false).GetComponent<AudioSourceParameterSlider>();
            con.SliderType = parameter.Key;
            con.ChangeRange(parameter.Value);
        }
    }
    
    
}
