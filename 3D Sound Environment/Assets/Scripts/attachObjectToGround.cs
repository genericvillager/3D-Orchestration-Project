using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class attachObjectToGround : MonoBehaviour
{
    public float height;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x,height,pos.z);
        transform.rotation = Quaternion.identity;

    }
}
