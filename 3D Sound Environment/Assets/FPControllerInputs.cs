using System.Collections;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FPControllerInputs : MonoBehaviour
{
    private Controls _controls;

    private Camera mainCamera;

    private FirstPersonLook fpsLook;

    [SerializeField] private GameObject menu;

    [SerializeField] private FPControllerGrabable objectGrabbed = null;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        _controls = new Controls();
        _controls.Enable();
        _controls.Room.Escape.performed += ctx => Quit(ctx);
        _controls.Room.Click.performed += ctx => click(ctx);
        fpsLook = FindObjectOfType<FirstPersonLook>();
        
        Cursor.visible = false;
    }

    void Quit(InputAction.CallbackContext ctx)
    {
        if (menu.activeSelf)
        {
            //exit menu
            fpsLook.sensitivity = 1;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menu.SetActive(false);
            
        }
        else if (!menu.activeSelf)
        {
            //enter menu
            fpsLook.sensitivity = 0;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            menu.SetActive(true);
        }
    }

    void click(InputAction.CallbackContext ctx)
    {
        if (objectGrabbed)
        {
            TogglePickupAudioSource(objectGrabbed);
            return;
        }


        Ray ray = new Ray(mainCamera.transform.position,mainCamera.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin,ray.direction * hit.distance,Color.green, 1f);
            // Check if the hit object is a UI element
            if (hit.collider != null)
            {
                
                //print(hit.collider.gameObject.name);
                TMP_InputField inputField = hit.collider.gameObject.GetComponent<TMP_InputField>();
                Slider slider = hit.collider.gameObject.GetComponent<Slider>();
                Scrollbar scrollbar = hit.collider.gameObject.GetComponent<Scrollbar>();
                Button button = hit.collider.gameObject.GetComponent<Button>();
                if (inputField)
                {
                    InputFieldSelected(inputField);
                }
                else if (slider)
                {
                    SliderSelected(slider, hit.point);
                }
                else if (scrollbar)
                {
                    ScrollBarSelected(scrollbar,hit.point);
                }
                else if (button)
                {
                    click(button.gameObject);
                }
                else if (hit.collider.gameObject.name == "Poke Quad")
                {
                    StartCoroutine(PokeInteractableSelected(hit.collider.gameObject.transform));
                    return;
                }
                else if (hit.collider.gameObject.GetComponent<FPControllerGrabable>())
                {
                    TogglePickupAudioSource(hit.collider.gameObject.GetComponent<FPControllerGrabable>());
                }
            }
        }
    }

    void InputFieldSelected(TMP_InputField inputField)
    {
        //Debug.Log("InputFieldSelected called");
        Time.timeScale = 0;
        click(inputField.gameObject);

        // Select and activate input field
        inputField.Select();
        inputField.ActivateInputField();
        
        // Add some logs to track focus
        //Debug.Log("Is input field focused: " + inputField.isFocused);
    }

    void click(GameObject obj)
    {
        //Debug.Log("Click method called");
        ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    void SliderSelected(Slider slider, Vector3 hitLocation)
    {
        print($"hit slider at {hitLocation}");
        float sliderValue;

        //set up temp OBJ for measuring
        GameObject measuringObj = new GameObject();
        measuringObj.transform.position = hitLocation;
        measuringObj.transform.SetParent(slider.transform);
        measuringObj.transform.localPosition = new Vector3(measuringObj.transform.localPosition.x, 0, 0);
        float distance = Vector3.Distance(measuringObj.gameObject.transform.localPosition, new Vector3(-50f,0f,0f));
        Destroy(measuringObj);
        
        //remap value to allig with click point and set it to slider range
        float value = Remap(distance, 0, slider.minValue, 100, slider.maxValue);
        //round the value to 2 decimals
        sliderValue = Mathf.Round(value * 100.0f) * 0.01f;
        slider.value = sliderValue;
        
        //print("Slider Value: " + sliderValue);
        click(slider.gameObject);
    }

    void ScrollBarSelected(Scrollbar scrollbar, Vector3 hitLocation)
    {
        float sliderValue;

        //set up temp OBJ for measuring
        GameObject measuringObj = new GameObject();
        measuringObj.transform.position = hitLocation;
        measuringObj.transform.SetParent(scrollbar.transform);
        measuringObj.transform.localPosition = new Vector3(measuringObj.transform.localPosition.x, 0, 0);
        float distance = Vector3.Distance(measuringObj.gameObject.transform.localPosition, new Vector3(-135f,0f,0f));
        Destroy(measuringObj);
        
        //remap value to allig with click point and set it to slider range
        
        float value = Remap(distance, 0, 0, 280, 1);
        //round the value to 2 decimals
        sliderValue = Mathf.Round(value * 100.0f) * 0.01f;
        scrollbar.value = sliderValue;
        
        //print("Slider Value: " + sliderValue);
        click(scrollbar.gameObject);
    }

    private static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        //print($"value: {value}\nRange from:{from1}:{from2}\nRange to:{to1}:{to2}");
        float fromAbs = value - from1;
        float fromMaxAbs = from2 - from1;

        float normal = fromAbs / fromMaxAbs;

        float toMaxAbs = to2 - to1;
        float toAbs = toMaxAbs * normal;

        float output = toAbs + to1;
        return output;
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
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
        print("fire");
        if (!objectGrabbed)
            objectGrabbed = fpControllerGrabable;
        else
            objectGrabbed = null;
        
        fpControllerGrabable.ToggleFollow();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(mainCamera.ScreenPointToRay(Input.mousePosition));
    }
}

