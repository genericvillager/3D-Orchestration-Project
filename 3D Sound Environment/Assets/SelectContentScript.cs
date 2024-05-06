using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using File = OpenCover.Framework.Model.File;

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
                string[] fileNameSplit = dirPath.Split("\\");
                string fileName = fileNameSplit[^1];
                transform.root.GetComponent<AudioSourceController>().SwitchAudioFile(myClip,fileName);
            }
        }
    }
}
