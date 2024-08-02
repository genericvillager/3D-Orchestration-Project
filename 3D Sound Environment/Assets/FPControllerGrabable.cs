using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPControllerGrabable : MonoBehaviour
{
    private bool following = false;
    private bool ToggleRotation;

    private Transform oldParent;

    private BoxCollider _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            Vector3 rotationSpeed = new Vector3 (Keyboard.current.xKey.isPressed.CompareTo(false), 
                Keyboard.current.yKey.isPressed.CompareTo(false), Keyboard.current.zKey.isPressed.CompareTo(false));
            transform.Rotate (rotationSpeed);

            float distance = Mouse.current.scroll.value[1];
            if (distance != 0)
            {
                Vector3 pos = transform.position;
                pos = Vector3.MoveTowards(pos, transform.parent.position, -distance/ 10 * Time.deltaTime);
                transform.position = pos;
            }
        }
    }
    
    

    public void ToggleFollow()
    {
        if (!following)
        {
            //print("toggled grab on");
            following = true;
            if (transform.parent != null) oldParent = transform.parent;
            if (Camera.main != null) transform.parent = Camera.main.transform;
            gameObject.layer = LayerMask.NameToLayer("MovingObjects");
            SetLayerAllChildren(transform, LayerMask.NameToLayer("MovingObjects"));
            
        }

        else if (following)
        {
            Vector3 pos = transform.position;
            //print("toggled grab off");
            following = false;
            transform.parent = oldParent;
            _boxCollider.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Default");
            SetLayerAllChildren(transform, LayerMask.NameToLayer("Default"));
            transform.position = pos;
        }
    }
    
    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
//            Debug.Log(child.name);
            child.gameObject.layer = layer;
        }
    }
}
