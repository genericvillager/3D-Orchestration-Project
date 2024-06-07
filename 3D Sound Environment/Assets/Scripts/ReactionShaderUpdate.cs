using System;
using System.Linq;
using UnityEngine;

public class ReactionShaderUpdate : MonoBehaviour
{
    private Renderer mat;
    public float val = 0;
    private AudioSpectrum _audioSpectrum;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>();
        _audioSpectrum = FindObjectOfType<AudioSpectrum>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(_audioSpectrum.MeanLevels.Average() - val) > 0.001)
        {
            val = _audioSpectrum.MeanLevels.Average();
            mat.material.SetFloat("_Value", val);
        }
    }
}
