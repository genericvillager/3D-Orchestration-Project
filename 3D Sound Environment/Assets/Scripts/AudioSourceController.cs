using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class AudioSourceController : MonoBehaviour
{
    Transform player;
    
    private LineRenderer laserLine;

    private AudioSource AS;

    private AudioReverbFilter ARF;

    private AudioManager _audioManager;

    private bool isPlaying = false;
    
    public TMP_Text audioName;

    private string dirpath = "";

    public AudioSourceSaveInfo saveInfo;
    
    // Dictionary to map slider types to field names
    private Dictionary<string, string> sliderTypeToProperty = new Dictionary<string, string>
    {
        { "vol", "volDefault" },
        { "pitch", "pitchDefault" },
        { "stereoPan", "stereoPanDefault" },
        { "spatialBlend", "spatialBlendDefault" },
        { "reverbZoneMix", "reverbZoneMixDefault" },
        { "dopplerLevel", "dopplerLevelDefault" },
        { "spread", "spreadDefault" },
        { "dryLevel", "dryLevelDefault" },
        { "room", "roomDefault" },
        { "roomHF", "roomHFDefault" },
        { "roomLF", "roomLFDefault" },
        { "decayTime", "decayTimeDefault" },
        { "decayHFRatio", "decayHFRatioDefault" },
        { "reflectionsLevel", "reflectionsLevelDefault" },
        { "reflectionsDelay", "reflectionsDelayDefault" },
        { "reverbLevel", "reverbLevelDefault" },
        { "reverbDelay", "reverbDelayDefault" },
        { "hfReference", "hfReferenceDefault" },
        { "lfReference", "lfReferenceDefault" },
        { "diffusion", "diffusionDefault" },
        { "density", "densityDefault" }
    };
    
    // Current values
    private float volDefault;
    private float pitchDefault;
    private float stereoPanDefault;
    private float spatialBlendDefault;
    private float reverbZoneMixDefault;
    private float dopplerLevelDefault;
    private float spreadDefault;
    private float dryLevelDefault;
    private float roomDefault;
    private float roomHFDefault;
    private float roomLFDefault;
    private float decayTimeDefault;
    private float decayHFRatioDefault;
    private float reflectionsLevelDefault;
    private float reflectionsDelayDefault;
    private float reverbLevelDefault;
    private float reverbDelayDefault;
    private float hfReferenceDefault;
    private float lfReferenceDefault;
    private float diffusionDefault;
    private float densityDefault;

    
    // Default values
    private readonly float StaticVolDefault = .69f; 
    private readonly float StaticPitchDefault = 1;
    private readonly float StaticStereoPanDefault = 0;
    private readonly float StaticSpatialBlendDefault = 1;
    private readonly float StaticReverbZoneMixDefault = 1;
    private readonly float StaticDopplerLevelDefault = 1;
    private readonly float StaticSpreadDefault = 0;
    private readonly float StaticDryLevelDefault = 0;
    private readonly float StaticRoomDefault = 0;
    private readonly float StaticRoomHFDefault = 0;
    private readonly float StaticRoomLFDefault  = 0;
    private readonly float StaticDecayTimeDefault = 1;
    private readonly float StaticDecayHFRatioDefault = .5f;
    private readonly float StaticReflectionsLevelDefault = -10000;
    private readonly float StaticReflectionsDelayDefault = 0.04f;
    private readonly float StaticReverbLevelDefault = 0;
    private readonly float StaticReverbDelayDefault = 0.04f;
    private readonly float StaticHfReferenceDefault = 5000;
    private readonly float StaticLfReferenceDefault = 250;
    private readonly float StaticDiffusionDefault = 100;
    private readonly float StaticDensityDefault = 100;

    public bool didResetAudioParameters = false;
    
    // Start is called before the first frame update
    void Start()
    { 
        AS = GetComponent<AudioSource>();
        ARF = GetComponent<AudioReverbFilter>();
        player = GameObject.FindWithTag("Player").transform;
        laserLine = GetComponent<LineRenderer>();
        _audioManager = FindObjectOfType<AudioManager>();
        ResetAudioSourceParameters();
        
        SaveInfo();
    }

    public void SaveInfo()
    {
    saveInfo = new AudioSourceSaveInfo(audioName.text,
        transform.position,
        transform.rotation.eulerAngles,
        dirpath,
        volDefault,
        pitchDefault,
        stereoPanDefault,
        spatialBlendDefault,
        reverbZoneMixDefault,
        dopplerLevelDefault,
        spreadDefault,
        dryLevelDefault,
        roomDefault,
        roomHFDefault,
        roomLFDefault,
        decayTimeDefault,
        decayHFRatioDefault,
        reflectionsLevelDefault,
        reflectionsDelayDefault,
        reverbLevelDefault,
        reverbDelayDefault,
        hfReferenceDefault,
        lfReferenceDefault,
        diffusionDefault,
        densityDefault);
    }

    public void ResetAudioSourceParameters()
    {
        volDefault = StaticVolDefault;
        pitchDefault = StaticPitchDefault;
        stereoPanDefault = StaticStereoPanDefault;
        spatialBlendDefault = StaticSpatialBlendDefault;
        reverbZoneMixDefault = StaticReverbZoneMixDefault;
        dopplerLevelDefault = StaticDopplerLevelDefault;
        spreadDefault = StaticSpreadDefault;
        dryLevelDefault = StaticDryLevelDefault;
        roomDefault = StaticRoomDefault;
        roomHFDefault = StaticRoomHFDefault;
        roomLFDefault = StaticRoomLFDefault;
        decayTimeDefault = StaticDecayTimeDefault;
        decayHFRatioDefault = StaticDecayHFRatioDefault;
        reflectionsLevelDefault = StaticReflectionsLevelDefault;
        reflectionsDelayDefault = StaticReflectionsDelayDefault;
        reverbLevelDefault = StaticReverbLevelDefault;
        reverbDelayDefault = StaticReverbDelayDefault;
        hfReferenceDefault = StaticHfReferenceDefault;
        lfReferenceDefault = StaticLfReferenceDefault;
        diffusionDefault = StaticDiffusionDefault;
        densityDefault = StaticDensityDefault;
        didResetAudioParameters = true;
        ChangeAudio();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (isPlaying)
        {
            HitDetect();
        }
    }

    private void HitDetect()
    {
        RaycastHit hit;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, AS.maxDistance))
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            switch (hit.transform.tag)
            {
                case "Player":
                    ChangeAudio();
                    break;

                case "Crowd":
                    ChangeAudio(vol: -.1f, reverbDelay: .07f);
                    break;

                case "Wall":
                    ChangeAudio(vol: .5f, reverbDelay: .09f, diffusion: 50, decayHFRatio: 1);
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.yellow);
        }
        
    }

    public void SwitchAudioFile(AudioClip clip, string dirPath)
    {
        dirpath = dirPath;
        string[] fileNameSplit = dirPath.Split("\\");
        string fileName = fileNameSplit[^1];
        AS.Stop();
        AS.clip = clip;
        audioName.text = fileName;
        if (isPlaying)
        {
            AS.Play();
            SynchroniseWithMain();
        }
        SaveInfo();
    }

    private void ChangeAudio(float vol = 0, float pitch = 0, float stereoPan = 0,
        float spatialBlend = 0, float reverbZoneMix = 0, float dopplerLevel = 0,
        float spread = 0, float dryLevel = 0, float room = 0,
        float roomHF = 0, float roomLF = 0, float decayTime = 0,
        float decayHFRatio = 0, int reflectionsLevel = 0,
        float reflectionsDelay = 0, float reverbLevel = 0,
        float reverbDelay = 0, int hfReference = 0, int lfReference = 0,
        float diffusion = 0, float density = 0)
    {
        AS.volume = volDefault + vol;
        AS.pitch = pitchDefault + pitch;
        AS.panStereo = stereoPanDefault + stereoPan;
        AS.spatialBlend = spatialBlendDefault + spatialBlend;
        AS.reverbZoneMix = reverbZoneMixDefault + reverbZoneMix;
        AS.dopplerLevel = dopplerLevelDefault + dopplerLevel;
        AS.spread = spreadDefault + spread;

        ARF.dryLevel = dryLevelDefault + dryLevel;
        ARF.room = roomDefault + room;
        ARF.roomHF = roomHFDefault + roomHF;
        ARF.roomLF = roomLFDefault + roomLF;
        ARF.decayTime = decayTimeDefault + decayTime;
        ARF.decayHFRatio = decayHFRatioDefault + decayHFRatio;
        ARF.reflectionsLevel = reflectionsLevelDefault + reflectionsLevel;
        ARF.reflectionsDelay = reflectionsDelayDefault + reflectionsDelay;
        ARF.reverbLevel = reverbLevelDefault + reverbLevel;
        ARF.reverbDelay = reverbDelayDefault + reverbDelay;
        ARF.hfReference = hfReferenceDefault + hfReference;
        ARF.lfReference = lfReferenceDefault + lfReference;
        ARF.diffusion = diffusionDefault + diffusion;
        ARF.density = densityDefault + density;
    }
    
    
    // Method to update the value based on slider type
    public void UpdateDefaultValue(float value, string sliderType)
    {
        if (sliderTypeToProperty.TryGetValue(sliderType, out string propertyName))
        {
            var fieldInfo = GetType().GetField(propertyName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(this, value);
                ChangeAudio();
            }
            else
            {
                Debug.LogError("Property not found: " + propertyName);
            }
        }
        else
        {
            Debug.LogError(sliderType + " is not a valid SliderType");
        }
        
        SaveInfo();
    }

    public float GetDefaultValue(string sliderType)
    {
        if (sliderTypeToProperty.TryGetValue(sliderType, out string propertyName))
        {
            var fieldInfo = GetType().GetField(propertyName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                return float.Parse(fieldInfo.GetValue(this).ToString());
            }
            else
            {
                Debug.LogError("Property not found: " + propertyName);
            }
        }
        else
        {
            Debug.LogError(sliderType + " is not a valid SliderType");
        }

        return 0;
    }

    public void ToggleMute(bool forceMute = false)
    {
        if (AS.mute && !forceMute)
        {
            AS.mute = false;
            isPlaying = false;
        }
        else
        {
            AS.mute = true;
            isPlaying = true;
        }
    }

    public void ResetAudioSource()
    {
        AS.Stop();
        AS.mute = false;
        isPlaying = false;
        AS.timeSamples = 0;
        
        SaveInfo();
    }

    public void PlayNow()
    {
        isPlaying = true;
        AS.Play();
        SynchroniseWithMain();
    }

    void SynchroniseWithMain()
    {
        try
        {
            AS.timeSamples = _audioManager.mainAudioSource.timeSamples;
        }
        catch
        {
            print("TimeSamples did not match!");
        }
    }
}
