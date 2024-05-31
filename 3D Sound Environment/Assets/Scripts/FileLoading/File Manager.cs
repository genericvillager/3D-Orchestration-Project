using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class FileManager : MonoBehaviour
{
    private string dirPath;
    private List<string> filesInDir = new List<string>();
    [SerializeField] private GameObject canvasHolder;
    [SerializeField] private GameObject content;
    private GameObject currentCanvas;

    private List<GameObject> contents = new List<GameObject>();

    public void OpenFileExplorer(Transform parent, string folderType)
    {
        currentCanvas = Instantiate(canvasHolder, parent,false);
        currentCanvas.transform.localPosition = new Vector3(0,1,.3f);
        currentCanvas.transform.localRotation= Quaternion.Euler(0,180,0);
        dirPath = Application.dataPath + "/Resources/" + folderType;
        dirPath =  dirPath.Replace("/", "\\");
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        OpenFolder(dirPath);
    }
    
    public void CloseFileExplorer()
    {
        filesInDir.Clear();
        contents.Clear();
        Destroy(currentCanvas);
    }

    public List<string> FetchFiles(string path)
    {
        filesInDir.Clear();
        DirectoryInfo d = new DirectoryInfo(path);
        foreach (var folder in d.GetDirectories())
        {
            filesInDir.Add(folder.ToString());
        }
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
            SelectContentScript conScript = con.GetComponent<SelectContentScript>();
            conScript.textbox.text = fileName;
            conScript.dirPath = file;
            contents.Add(con);
        }
    }

    public void OpenFolder(string path)
    {
        foreach (GameObject con in contents)
        {
            Destroy(con);
        }
        
        contents.Clear();
        
        PopulateCanvasHolder(FetchFiles(path));

        print(path);
        print(dirPath);
        
        if (path != dirPath)
        {
            DirectoryInfo parentDir = Directory.GetParent(path);
            GameObject back = Instantiate(content, currentCanvas.transform.Find("Canvas/Panel/ScrollArea/Content"), false);
            SelectContentScript conScript = back.GetComponent<SelectContentScript>();
            conScript.textbox.text = "Back";
            conScript.dirPath = parentDir.ToString();
            contents.Add(back);
        }

        
    }
    
}