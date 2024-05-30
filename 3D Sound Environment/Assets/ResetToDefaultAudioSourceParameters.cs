using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToDefaultAudioSourceParameters : MonoBehaviour
{
    public  AudioSourceController ASC;

    private void Start()
    {
        ASC = transform.root.GetComponent<AudioSourceController>();
    }

    public void Reset()
    {
        ASC.ResetAudioSourceParameters();
    }
}
