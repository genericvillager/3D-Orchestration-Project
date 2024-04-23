using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    public Scrollbar slide;

    public Transform panel;

    public GridLayoutGroup panelGrid;

    public int amountOfFiles;

    private int scrollAmounts;

    public float padMin = 0;

    public float padMax = 200; 
    // Start is called before the first frame update
    void Start()
    {
        amountOfFiles = panel.childCount;
        padMax = Mathf.RoundToInt(amountOfFiles-1 / 2)*500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
    public void Scroll(float value)
    {
        //int val = Mathf.RoundToInt(Remap(value,0,padMin,1,padMax))+200;
        //print(value);
        //print(val);
        //panelGrid.padding.left = val;
    }
}
