using UnityEngine;

public class SetUpButtons : MonoBehaviour
{
    FileManager fileManager;
    bool audioFileExplorerOpen = false;

    bool audioSourceParametersOpen = false;

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

    public void Delete()
    {
        if(!transform.CompareTag("Crowd"))
            audioManager._sources.Remove(audioSourceController);
        Destroy(gameObject);
    }
    
    public void ToggleOpenAudioSourceParameters()
    {
        if (!audioSourceParametersOpen)
        {
            audioManager.InitAudioSourceParameters(transform);
            audioSourceParametersOpen = true;
        }
        else
        {
            audioManager.CloseAudioSourceParameters();
            audioSourceParametersOpen= false;
        }
    }
}
