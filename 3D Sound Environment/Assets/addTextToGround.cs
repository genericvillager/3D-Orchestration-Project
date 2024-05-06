using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class addTextToGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        if(transform.position.z != 0)
            text.text =  Mathf.RoundToInt(transform.position.z).ToString();
        else
            text.text = Mathf.RoundToInt(transform.position.x).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
