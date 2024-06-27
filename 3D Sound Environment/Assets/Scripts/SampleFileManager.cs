using System.Collections;
using UnityEngine;
using System.IO;

public class SampleFileManager : MonoBehaviour
{
    private void Start()
    {
//#if UNITY_WEBGL_API
        CopySampleFilesToPersistentPath();
//#endif
        
    }

    private void CopySampleFilesToPersistentPath()
    {
        string dataPath = Application.dataPath;
        string streamingAssetsPath = Application.streamingAssetsPath;
        
        string[] sampleFiles = { "/Resources/Saves/Sample.sav", "/Resources/AudioFiles/Samples/BASS.wav", 
            "/Resources/AudioFiles/Samples/DRUMS.wav","/Resources/AudioFiles/Samples/GTR 70.wav" };
        
        if (!Directory.Exists(dataPath + "/Resources"))
            Directory.CreateDirectory(dataPath + "/Resources");

        if (!Directory.Exists(dataPath + "/Resources/Saves"))
            Directory.CreateDirectory(dataPath + "/Resources/Save");
        
        if (!Directory.Exists(dataPath + "/Resources/AudioFiles"))
            Directory.CreateDirectory(dataPath + "/Resources/AudioFiles");
        
        if (!Directory.Exists(dataPath + "/Resources/AudioFiles/Samples"))
            Directory.CreateDirectory(dataPath + "/Resources/AudioFiles/Samples");
        

        foreach (string fileName in sampleFiles)
        {
            string filePath = streamingAssetsPath + fileName;
            string destinationPath = dataPath + fileName;
#if UNITY_EDITOR
            LoadStreamingAssets(filePath,destinationPath);
#elif PLATFORM_WEBGL
            filePath = System.IO.Path.Combine(streamingAssetsPath, fileName);
            StartCoroutine(loadStreamingAssetWebGL(filePath,destinationPath));
#else
            LoadStreamingAssets(filePath,destinationPath);
#endif
        }
    }

    private void LoadStreamingAssets(string filePath, string destinationPath)
    {
        if(File.Exists(destinationPath))
            return;
        File.Copy(filePath, destinationPath);
    } 
    
    IEnumerator loadStreamingAssetWebGL(string filePath, string destinationPath)
    {
        
        print(destinationPath);

        WWW www = new WWW(filePath);
        print(www.url);
        File.Copy(www.url, destinationPath, true);
        yield return www;
        

    }
}