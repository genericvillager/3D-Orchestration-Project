using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomManager : MonoBehaviour
{
    private AudioReverbZone ARZ;
    
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
