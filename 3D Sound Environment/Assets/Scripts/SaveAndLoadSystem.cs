using System.Collections.Generic;
using System.IO;
using Leguar.TotalJSON;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;

public class SaveAndLoadSystem : MonoBehaviour
{

    private string dirPath;
    private string FileName;
    private AudioManager AM;
    [SerializeField]private GameObject AudioSourcePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        AM = GetComponent<AudioManager>();
        dirPath = Application.dataPath + "/Resources/Saves";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        print(dirPath);
        
    }

    private JSON AStoDic()
    {
        var dictionary = new JSON();
        dictionary.Clear();
        int index = 0;
        foreach (AudioSourceController audioSourceController in AM._sources)
        {
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            JSON test = AStoDic();
            saveJsonObjectToTextFile(test);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            JSON test = AStoDic();
            LoadAudioSources();
        }
    }

    private void saveJsonObjectToTextFile(JSON jsonObject)
    {
        var jsonAsString = jsonObject.CreatePrettyString();
        var writer = new StreamWriter(dirPath+"/testFile");
        writer.WriteLine(jsonAsString);
        writer.Close();
    }
    
    private JSON loadTextFileToJsonObject()
    {
        var reader = new StreamReader(dirPath+"/testFile");
        var jsonAsString = reader.ReadToEnd();
        reader.Close();
        var jsonObject = JSON.ParseString(jsonAsString);
        return jsonObject;
    }

    public void LoadAudioSources()
    {
        JSON saveFile = loadTextFileToJsonObject();

        int index = 0;
        while (true)
        {
            index++;
            if (!saveFile.ContainsKey($"audioSource{index}"))
            {
                break;
            }
            else
            {
                JSON values = saveFile.GetJSON($"audioSource{index}");
                GameObject AS = Instantiate(AudioSourcePrefab);
                AudioSourceController ASC = AS.GetComponent<AudioSourceController>();
                
                AS.transform.position =ConvertFromString(values.GetString("Position")) ;
                AS.transform.rotation = Quaternion.Euler(ConvertFromString(values.GetString("Rotation")));
            }
        }
    }
    Vector3 ConvertFromString(string input)
    {
        print(input);
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
}
