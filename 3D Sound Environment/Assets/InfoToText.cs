using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InfoToText : MonoBehaviour
{
    [SerializeField,System.Serializable] class OnEvent : UnityEvent { }

    [SerializeField] private TMP_Text textBox;
    
    [SerializeField] OnEvent myEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        textBox.text = myEvent.ToString();
    }
}
