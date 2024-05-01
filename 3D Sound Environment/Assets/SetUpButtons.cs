using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpButtons : MonoBehaviour
{
    FileManager fileManager;
    bool audioFileExplorerOpen = false;

    AudioManager audioManager;
    AudioSource audioSource;
    AudioSourceController audioSourceController;
    // Start is called before the first frame update
    void Start()
    {
        fileManager = FindObjectOfType<FileManager>();

        audioSourceController = GetComponent<AudioSourceController>();
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteToggle()
    {
        audioSourceController.ToggleMute();
    }

    public void SoloToggle()
    {
        audioManager.ToggleSoloAudioSource(audioSource);
    }

    public void ToggleOpenAudioFileExplorer()
    {
        if (!audioFileExplorerOpen)
        {
            fileManager.OpenFileExplorer(transform, "AudioFiles");
            audioFileExplorerOpen = true;
        }
        else
        {
            fileManager.CloseFileExplorer();
            audioFileExplorerOpen= false;
        }
    }
}
