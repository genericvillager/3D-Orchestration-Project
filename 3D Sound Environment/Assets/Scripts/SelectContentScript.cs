using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class SelectContentScript : MonoBehaviour
{
    public string dirPath;
    public TMP_Text textbox;
    private AudioSourceController ASC;
    private FileManager FM;
    
    // Start is called before the first frame update
    void Start()
    {
        ASC = transform.root.GetComponent<AudioSourceController>();
        FM = FindObjectOfType<FileManager>();
        textbox = GetComponent<TMP_Text>();
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
        // get the file attributes for file or directory
        FileAttributes attr = File.GetAttributes(dirPath);

//detect whether its a directory or file
        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            FM.OpenFolder(dirPath);
        else
            StartCoroutine(loadAudioFile());
    }

    IEnumerator loadAudioFile()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + dirPath, AudioType.UNKNOWN))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                ASC.SwitchAudioFile(myClip,dirPath);
            }
        }
    }
}
