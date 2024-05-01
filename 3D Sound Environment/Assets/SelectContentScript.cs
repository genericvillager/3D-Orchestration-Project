using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class SelectContentScript : MonoBehaviour
{
    public string dirPath;

    [SerializeField] private AudioSourceController audioSourceController;
    string[] extensionList = { ".wav", ".mp3", ".m4a", ".flac", ".mp4"};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFile()
    {
        System.Diagnostics.Process.Start(dirPath);
    }

    public void SwitchAudioFile()
    {
        string dirPathFormatted = dirPath;
        foreach(string extension in extensionList) 
        {
            if (dirPath.Contains(extension))
            {
                dirPathFormatted = dirPath.Replace(extension, "");
                break;
            }
        }
        AudioClip AC = Resources.Load<AudioClip>(dirPathFormatted);
        AudioClip clip = (AudioClip)Resources.Load(dirPath, typeof(AudioClip));
        transform.root.GetComponent<AudioSourceController>().SwitchAudioFile(clip);
    }

    public void SwitchAudioFileIE()
    {
        StartCoroutine(loadFile());
    }

    IEnumerator loadFile()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + dirPath, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                transform.root.GetComponent<AudioSourceController>().SwitchAudioFile(myClip);
            }
        }
    }
}
