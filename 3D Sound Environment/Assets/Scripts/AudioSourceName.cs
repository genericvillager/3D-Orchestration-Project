using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSourceName : MonoBehaviour
{
    public TMP_Text audioName;

    public Transform audioSource, textTransform;

    private void Start()
    {
        audioName.text = gameObject.GetComponent<AudioSource>().name;
    }

    private void Update()
    {
        textTransform.position = audioSource.position;
        textTransform.rotation = audioSource.rotation;
    }
}
