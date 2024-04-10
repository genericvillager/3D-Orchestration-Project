using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSourceName : MonoBehaviour
{
    public TMP_Text audioName;

    private void Start()
    {
        audioName.text = gameObject.GetComponent<AudioSource>().name;
    }
}
