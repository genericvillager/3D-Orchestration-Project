
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
 
        /* Toggle cursor lock state
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
        */
    }

    void click(InputAction.CallbackContext ctx)
    {
        
        Ray ray = new Ray(mainCamera.transform.position,mainCamera.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin,ray.direction * hit.distance,Color.green, 1f);
            // Check if the hit object is a UI element
            if (hit.collider != null)
            {
                print(hit.collider.gameObject.name);
                
                if (hit.collider.gameObject.GetComponent<TMP_InputField>())
                {
                    InputFieldSelected();
                }

                if (hit.collider.gameObject.name == "Poke Quad")
                {
                    StartCoroutine(PokeInteractableSelected(hit.collider.gameObject.transform));
                    return;
                }

                if (hit.collider.gameObject.GetComponent<FPControllerGrabable>())
                {
                    TogglePickupAudioSource(hit.collider.gameObject.GetComponent<FPControllerGrabable>());
                }
                //print("execute UI element");
                // If you want to trigger UI events manually, you can use ExecuteEvents
                ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }

    void InputFieldSelected()
    {

        Time.timeScale = 0;

    }

    IEnumerator PokeInteractableSelected(Transform pokeQuad)
    {
        //print("Poke Interactable Exectuting");
        
        UnityEvent select = pokeQuad.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().WhenSelect;
        select.Invoke();

        yield return 1;

        if (pokeQuad)
        {
            UnityEvent unSelect = pokeQuad.parent.gameObject.GetComponent<InteractableUnityEventWrapper>().WhenUnselect;
            unSelect.Invoke();
        }

    }

    void TogglePickupAudioSource(FPControllerGrabable fpControllerGrabable)
    {
        fpControllerGrabable.ToggleFollow();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(mainCamera.ScreenPointToRay(Input.mousePosition));
    }
}

