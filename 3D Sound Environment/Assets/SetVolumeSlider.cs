using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider; 
    // Start is called before the first frame update
    void Start()
    {
        slider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
