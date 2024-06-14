
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class FPControllerInputs : MonoBehaviour
{
    private Controls _controls;

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        _controls = new Controls();
        _controls.Enable();
        _controls.Room.Interact.performed += ctx => Interact(ctx);
        _controls.Room.Click.performed += ctx => click(ctx);
    }

    void Interact(InputAction.CallbackContext ctx)
    {
 
        // Toggle cursor lock state
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    void click(InputAction.CallbackContext ctx)
    {
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin,ray.direction * hit.distance,Color.green, 1f);
            print(hit.transform.gameObject.name);
            // Check if the hit object is a UI element
            if (hit.collider != null)
            {
                // If you want to trigger UI events manually, you can use ExecuteEvents
                ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(mainCamera.ScreenPointToRay(Input.mousePosition));
    }
}

