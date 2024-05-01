using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSourceName : MonoBehaviour
{
    public TMP_Text audioName;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioName.text = _audioSource.clip.name;
    }
}
