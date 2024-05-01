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
    
}
