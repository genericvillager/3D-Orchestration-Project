using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class FileManager : MonoBehaviour
{
    private string dirPath;
    private List<string> filesInDir = new List<string>();
    [SerializeField] private GameObject canvasHolder;
    [SerializeField] private GameObject content;
    private GameObject currentCanvas;

    public void OpenFileExplorer(Transform parent, string folderType)
    {
        currentCanvas = Instantiate(canvasHolder, parent,false);
        currentCanvas.transform.localPosition = new Vector3(0,1,.3f);
        currentCanvas.transform.localRotation= Quaternion.Euler(0,180,0);
        dirPath = Application.dataPath + "/Resources/" + folderType;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        PopulateCanvasHolder(FetchFiles());
    }
    
    public void CloseFileExplorer()
    {
        filesInDir.Clear();
        Destroy(currentCanvas);
    }

    public List<string> FetchFiles()
    {
        DirectoryInfo d = new DirectoryInfo(dirPath);
        List<string> files = new List<string>();
        int amount = 0;
        foreach (var file in d.GetFiles())
        {
            if (!file.ToString().Contains(".meta"))
            {
                filesInDir.Add(file.ToString());
            }
        }
        return filesInDir;
    }

    private void PopulateCanvasHolder(List<string> files)
    {
        foreach (string file in files) 
        {
            string[] fileNameSplit = file.Split("\\");
            string fileName = fileNameSplit[^1];
           
            GameObject con = Instantiate(content, currentCanvas.transform.Find("Canvas/Panel/ScrollArea/Content"), false);
            con.transform.Find("TextBox").GetComponent<TMP_Text>().text = fileName;
            con.GetComponent<SelectContentScript>().dirPath = file;
        }
    }
    
}