using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceParameterSlider : MonoBehaviour
{
    public string SliderType = "null";
    private AudioSourceController ASC;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text valueText;

    private Slider _slider;
    
    // Start is called before the first frame update
    void Awake()
    {
        Transform root = transform.root;
        ASC = root.gameObject.GetComponent<AudioSourceController>();
        text.text = SliderType;
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SliderType != text.text)
        {
            text.text = SliderType;
            _slider.value = ASC.GetDefaultValue(SliderType);
        }

        if (_slider.value != ASC.GetDefaultValue(SliderType))
        {
            _slider.value = ASC.GetDefaultValue(SliderType);
        }
        valueText.text = _slider.value.ToString();
    }

    public void UpdateValue(float value)
    {
        ASC.UpdateDefaultValue(value, SliderType);
    }

    public void ChangeRange(Vector2 range)
    {
        _slider.minValue = range.x;
        _slider.maxValue = range.y;
    }
}
