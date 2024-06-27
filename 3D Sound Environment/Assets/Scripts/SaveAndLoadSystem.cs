using System.Collections.Generic;
using System.IO;
using Leguar.TotalJSON;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class SaveAndLoadSystem : MonoBehaviour
{

    private string dirPath;
    private string FileName;
    private AudioManager AM;
    [SerializeField]private GameObject AudioSourcePrefab;
    
    [SerializeField] private GameObject SaveUI;
    private GameObject saveUI;
    [SerializeField] private bool showingSaveUI = false;
    
    
    //sample save
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        AM = GetComponent<AudioManager>();
        dirPath = Application.dataPath + "/Resources/Saves";
        print(dirPath);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        //SetupSample();
        //print(dirPath);
        
    }

    private JSON AStoDic()
    {
        var dictionary = new JSON();
        dictionary.Clear();
        int index = 0;
        foreach (AudioSourceController audioSourceController in AM._sources)
        {
            audioSourceController.SaveInfo();
            
            index++;
            Dictionary<string, object> result = new Dictionary<string, object>();

            Type type = typeof(AudioSourceSaveInfo);
            // Get all public fields
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                result.Add(field.Name, field.GetValue(audioSourceController.saveInfo));
            }

            // Get all public properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead)
                {
                    result.Add(property.Name, property.GetValue(audioSourceController.saveInfo));
                }
            }
            dictionary.Add($"audioSource{index}", result);
        }

        return dictionary;
    }

    public void Save(String name)
    {
        FileName = "/" + name;
        JSON jsonObject = AStoDic();
        var jsonAsString = jsonObject.CreatePrettyString();
        var writer = new StreamWriter(dirPath+FileName+".sav");
        print("saving at: " + dirPath+FileName+".sav");
        writer.WriteLine(jsonAsString);
        writer.Close();
    }
    
    private JSON loadTextFileToJsonObject(string path)
    {
        var reader = new StreamReader(path);
        var jsonAsString = reader.ReadToEnd();
        reader.Close();
        var jsonObject = JSON.ParseString(jsonAsString);
        return jsonObject;
    }

    public void Load(string filePath)
    {
        //print("LOADING...");
        JSON saveFile = loadTextFileToJsonObject(filePath);
        
        //Remove and load audioSources
        RemoveAllCurrentAudioSources();
        LoadAllAudioSources(saveFile);
        
        //print("LOADING COMPLETE");
    }
    private Vector3 StringToVector3(string input)
    {
        if (input != null)
        {
            input = input.Replace("(", "");
            input = input.Replace(")", "");
            var vals = input.Split(',').Select(s => s.Trim()).ToArray();
            if (vals.Length == 3)
            {
                Single v1, v2, v3;
                
                if (Single.TryParse(vals[0], out v1) &&
                    Single.TryParse(vals[1], out v2) &&
                    Single.TryParse(vals[2], out v3))
                    return new Vector3(v1, v2, v3);
                else
                {
                    print(vals[0] + vals[1] + vals[2]);
                    throw new ArgumentException();
                }
            }
            else
                throw new ArgumentException();
        }
        else
            throw new ArgumentException();
    }
    
    private async Task<AudioClip> LoadAudioFileAsync(string dirPath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + dirPath, AudioType.UNKNOWN))
        {
            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
                return null;
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                return myClip;
            }
        }
    }

    public void ToggleShowSaveUI()
    {
        if (!showingSaveUI)
        {
            saveUI = Instantiate(SaveUI);
            showingSaveUI = true;
            return;
        }
        else
        {
            Destroy(saveUI);
            showingSaveUI = false;
            return;
        }
    }

    private void RemoveAllCurrentAudioSources()
    {
        AudioSourceController[] allAudioSources = FindObjectsOfType<AudioSourceController>();

        foreach (AudioSourceController source in allAudioSources)
        {
            //print(source.name);
            Destroy(source.gameObject);
        }

        allAudioSources = null;
    }
    private async void LoadAllAudioSources(JSON saveFile)
    {
        int index = 0;
        int stage = 0;
        while (true)
        {
            index++;
            if (!saveFile.ContainsKey($"audioSource{index}"))
            {
                break;
            }
            else
            {
                
                var values = saveFile.GetJSON($"audioSource{index}");
                
                
                GameObject AS = Instantiate(AudioSourcePrefab);
                stage++;
                
                AudioSourceController ASC = AS.GetComponent<AudioSourceController>();
                
                AS.transform.position = StringToVector3(values.GetString("Position")) ;
                stage++;
                
                AS.transform.rotation = Quaternion.Euler(StringToVector3(values.GetString("Rotation")));
                stage++;
                
                string path = values.GetString("File");
                ASC.SwitchAudioFile( await LoadAudioFileAsync(path),path);
                stage++;
                
                int s = 0;
                foreach (KeyValuePair<string,object> pair in values.AsDictionary())
                {
                    if (s == stage)
                    {
                        float result;
                        float.TryParse(pair.Value.ToString(), out result);
                        ASC.UpdateDefaultValue(result, char.ToLower(pair.Key[0]) + pair.Key.Substring(1));
                        //print(pair.ToString());
                        stage++;
                    }
                    s++;
                }
                
            }
        }
    }

    private void SetupSample()
    {
        Save("Sample");
        RemoveAllCurrentAudioSources();
    }
}
