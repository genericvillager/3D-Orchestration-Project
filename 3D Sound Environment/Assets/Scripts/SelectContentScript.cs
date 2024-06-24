using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SelectContentScript : MonoBehaviour
{
    public string dirPath;
    public TMP_Text textbox;
    private AudioSourceController ASC;
    private FileManager FM;
    private BoxCollider _boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        ASC = transform.root.GetComponent<AudioSourceController>();
        FM = FindObjectOfType<FileManager>();
        textbox = GetComponent<TMP_Text>();

        _boxCollider = gameObject.AddComponent<BoxCollider>();
        _boxCollider.size = new Vector3(100, 100, 1);
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
        if (Path.GetExtension(dirPath) == ".sav")
        {
            
        }
        else
            StartCoroutine(loadAudioFile());
    }

    void LoadSave()
    {
        FM.LoadSave(dirPath);
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
