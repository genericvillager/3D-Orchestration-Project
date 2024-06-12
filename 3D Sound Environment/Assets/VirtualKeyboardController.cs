using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VirtualKeyboardController : MonoBehaviour
{
    private TMP_InputField _inputField;
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    // Start is called before the first frame update
    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            print("not windows");
            if (overlayKeyboard != null)
                inputText = overlayKeyboard.text;

            if (_inputField.text != inputText)
                _inputField.text = inputText;
        }
    }

    public void OnSelect()
    {
        
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //System.Diagnostics.Process.Start("osk.exe");
            _inputField.ActivateInputField();
        }
        else
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }

    public void DeSelect()
    {
        overlayKeyboard = null;
    }
}
