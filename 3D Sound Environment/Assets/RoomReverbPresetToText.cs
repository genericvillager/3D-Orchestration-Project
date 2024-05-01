using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomReverbPresetToText : MonoBehaviour
{
    private TMP_Text textBox;

    private AudioReverbZone audioReverbZone;
    // Start is called before the first frame update
    void Start()
    {
        textBox = transform.Find("Room").GetComponent<TMP_Text>();
        audioReverbZone = transform.parent.parent.GetComponent<AudioReverbZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioReverbZone.reverbPreset.ToString() != textBox.text)
        {
            textBox.text = audioReverbZone.reverbPreset.ToString();
        }
    }
}
