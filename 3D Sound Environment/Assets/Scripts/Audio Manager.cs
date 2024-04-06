using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public RoomManager RM;
    Controls controls;
    private GameObject playerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera");
        controls = new Controls();
        controls.Enable();
        
        controls.Room.EnvironmentControl.performed += ctx => ChangeEnv(ctx);
        controls.Room.PauseToggle.performed += ctx => TogglePauseMusic(ctx);
        controls.Room.Interact.performed += ctx => Interact(ctx);
    }

    void ChangeEnv(InputAction.CallbackContext context)
    {
        RM.CycleEnv();
    }

    void TogglePauseMusic(InputAction.CallbackContext context)
    {
        if(AudioListener.pause)
            AudioListener.pause = false;
        else
            AudioListener.pause = true;
    }

    void Interact(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            switch (hit.transform.tag)
            {
                case "AudioSource":
                    hit.transform.gameObject.GetComponent<AudioSourceController>().ToggleMute();
                    break;
                
                default:
                    break;
            }
        }
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
