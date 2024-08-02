using UnityEngine;

public class RealisticRolloff : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Ensure the audio source is set to Custom Rolloff
        audioSource.rolloffMode = AudioRolloffMode.Custom;

        // Create the custom rolloff curve
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 1f);        // Full volume at 0 distance
        curve.AddKey(0.1f, 0.8f);    // Slight drop-off
        curve.AddKey(0.3f, 0.5f);    // More significant drop-off
        curve.AddKey(0.6f, 0.2f);    // Decreasing volume
        curve.AddKey(1f, 0f);        // No volume at max distance
        

        // Apply the custom rolloff curve to the audio source
        audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, curve);

        // Set min and max distances
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 100f;
    }
}