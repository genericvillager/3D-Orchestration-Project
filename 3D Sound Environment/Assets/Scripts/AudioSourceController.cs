using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    Transform player;
    
    private LineRenderer laserLine;

    private AudioSource AS;

    private AudioReverbFilter ARF;

    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    { 
        AS = GetComponent<AudioSource>();
        ARF = GetComponent<AudioReverbFilter>();
        player = GameObject.FindWithTag("Player").transform;
        laserLine = GetComponent<LineRenderer>();
    }

    void Wall(float value)
    {
        ARF.reverbDelay = value;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isPlaying)
            HitDetect();
    }

    void HitDetect()
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
                    ChangeAudio(vol: .9f, reverbDelay: .07f);
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

    public void SwitchAudioFile(AudioClip clip)
    {
        print(clip.name);
        AS.Stop();
        AS.clip = clip;
        if(isPlaying)
            AS.Play();
    }

    public void ChangeAudio(float vol = 1, float pitch = 1, float stereoPan = 0,
        float spatialBlend = 1, float reverbZoneMix = 1, float dopplerLevel = 1,
        float spread = 0, float dryLevel = 0, float room = 0,
        float roomHF = 0, float roomLF = 0, float decayTime = 1,
        float decayHFRatio = .5f, int reflectionsLevel = -10000,
        float reflectionsDelay = 0.04f, float reverbLevel = 0,
        float reverbDelay = .04f, int hfReference = 5000, int lfReference = 250,
        float diffusion = 100, float density = 100)
    {
        AS.volume = vol;
        AS.pitch = pitch;
        AS.panStereo = stereoPan;
        AS.spatialBlend = spatialBlend;
        AS.reverbZoneMix = reverbZoneMix;
        AS.dopplerLevel = dopplerLevel;
        AS.spread = spread;

        ARF.dryLevel = dryLevel;
        ARF.room = room;
        ARF.roomHF = roomHF;
        ARF.roomLF = roomLF;
        ARF.decayTime = decayTime;
        ARF.decayHFRatio = decayHFRatio;
        ARF.reflectionsLevel = reflectionsLevel;
        ARF.reflectionsDelay = reflectionsDelay;
        ARF.reverbLevel = reverbLevel;
        ARF.reverbDelay = reverbDelay;
        ARF.hfReference = hfReference;
        ARF.lfReference = lfReference;
        ARF.diffusion = diffusion;
        ARF.density = density;
    }

    public void ToggleMute()
    {
        if (AS.mute)
            AS.mute = false;
        else
            AS.mute = true;
    }

    public void PlayNow()
    {
        isPlaying = true;
        AS.Play();
    }
}
