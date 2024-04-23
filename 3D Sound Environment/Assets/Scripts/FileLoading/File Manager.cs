using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
 
public class FileManager : MonoBehaviour
{
    public string FolderName;

    private string dirPath;

    public TMP_Text textBox;

    private List<string> filesInDir = new List<string>();
    private void Start()
    {
        dirPath = Application.dataPath + "/" +FolderName;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath); ;
        }
        print(dirPath);
        FetchFiles();
        textBox.text = filesInDir[0];
        print(textBox.text);
        //EditorUtility.OpenFilePanel("Overwrite with png", dirPath, "png");
    }

    public string FetchFiles()
    {
        DirectoryInfo d = new DirectoryInfo(dirPath);
        string files = "";
        int amount = 0;
        foreach (var file in d.GetFiles())
        {
            if (!file.ToString().Contains(".meta"))
            {
                files += file + "\n";
                amount++;
                filesInDir.Add(file.ToString());
            }
        }
        return files;
    }

    public void SaveFile()
    {
        
    }
    
}