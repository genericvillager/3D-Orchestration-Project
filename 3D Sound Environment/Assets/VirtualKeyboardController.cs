using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
#if UNITY_ANDROID
        print("not windows");
        if (overlayKeyboard != null)
            inputText = overlayKeyboard.text;

        if (_inputField.text != inputText)
            _inputField.text = inputText;
#endif
    }

    public void OnSelect()
    {
        
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //System.Diagnostics.Process.Start("osk.exe");
            //_inputField.ActivateInputField();
        }
        else
        {
#if UNITY_ANDROID
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
#endif
            
        }
    }

    public void DeSelect()
    {
        print("Deselect");
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        _inputField.DeactivateInputField();
        overlayKeyboard = null;
    }
}
