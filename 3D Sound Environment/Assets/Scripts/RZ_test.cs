using System;
using UnityEngine;

public class RZ_test : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioReverbZone reverbZone;
    public Transform listener;


    private void Start()
    {

    }

    void Update()
    {
        if (audioSource == null || reverbZone == null || listener == null) return;

        float distanceToListener = Vector3.Distance(audioSource.transform.position, listener.position);
        float distanceToMin = reverbZone.minDistance;
        float distanceToMax = reverbZone.maxDistance;

        Debug.Log($"Distance to Listener: {distanceToListener}");
        Debug.Log($"Reverb Min Distance: {distanceToMin}");
        Debug.Log($"Reverb Max Distance: {distanceToMax}");

        if (distanceToListener < distanceToMin)
        {
            Debug.Log("Full reverb effect applied.");
        }
        else if (distanceToListener < distanceToMax)
        {
            Debug.Log("Partial reverb effect applied.");
        }
        else
        {
            Debug.Log("No reverb effect applied.");
        }
        
        AudioReverbZone[] arz = FindObjectsOfType<AudioReverbZone>();
        foreach (AudioReverbZone audioReverbZone in arz)
        {
            print(audioReverbZone.gameObject.name);
        }
    }
}