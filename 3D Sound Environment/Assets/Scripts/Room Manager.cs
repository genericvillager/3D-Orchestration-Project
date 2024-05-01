using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomManager : MonoBehaviour
{
    private AudioReverbZone ARZ;

    public GameObject audioSourceGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        ARZ = GetComponent<AudioReverbZone>();
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
    }
}
