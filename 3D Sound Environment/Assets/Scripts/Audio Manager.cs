using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public RoomManager RM;
    Controls controls;
    
    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.Enable();
        
        controls.Room.EnvironmentControl.performed += ctx => ChangeEnv(ctx);
        controls.Room.PauseToggle.performed += ctx => TogglePauseMusic(ctx);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
