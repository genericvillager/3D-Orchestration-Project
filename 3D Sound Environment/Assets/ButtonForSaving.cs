using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForSaving : MonoBehaviour
{
    private SaveAndLoadSystem _saveAndLoadSystem;
    private Button _button;
    private TMP_InputField _inputField;
    
    private void Start()
    {
        _saveAndLoadSystem = FindObjectOfType<SaveAndLoadSystem>();
        _button = transform.Find("Canvas/Panel/Save").GetComponent<Button>();
        _inputField = transform.Find("Canvas/Panel/InputField").GetComponent<TMP_InputField>();
        
        _button.onClick.AddListener(delegate() {_saveAndLoadSystem.Save(_inputField.text);});
    }
    
    
}
