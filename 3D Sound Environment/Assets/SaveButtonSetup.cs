using TMPro;
using UnityEngine;

public class SaveButtonSetup : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    private SaveAndLoadSystem _saveAndLoadSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _saveAndLoadSystem = FindObjectOfType<SaveAndLoadSystem>();
    }


    public void Save()
    {
        _saveAndLoadSystem.Save(_inputField.text);
    }
}
