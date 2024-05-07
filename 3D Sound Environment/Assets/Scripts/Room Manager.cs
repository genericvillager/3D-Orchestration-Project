using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomManager : MonoBehaviour
{
    private AudioReverbZone ARZ;

    public GameObject audioSourceGameObject;
    public GameObject crowdGameObject;
    [SerializeField] private GameObject screenGameObject;
    private AudioManager _audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        ARZ = GetComponent<AudioReverbZone>();
        _audioManager = GetComponent<AudioManager>();
    }

    public void CycleEnv()
    {
        ARZ.reverbPreset += 1;
        if (ARZ.reverbPreset == (AudioReverbPreset)28)
        {
            ARZ.reverbPreset = 0;
        }
            
        print(ARZ.reverbPreset);
    }

    public void InstantiateAudioSourceGameObject()
    {
        Instantiate(audioSourceGameObject, transform.position + new Vector3(0,0,3),Quaternion.identity);
        _audioManager.GetAllAudioSources();
    }
    public void InstantiateCrowdGameObject()
    {
        Instantiate(crowdGameObject, transform.position + new Vector3(0,0,3),Quaternion.identity);
    }
    
    public void InstantiateScreenGameObject()
    {
        Instantiate(screenGameObject, transform.position + new Vector3(0,0,3),Quaternion.identity);
    }
}
