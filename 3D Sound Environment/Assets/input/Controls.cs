//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/input/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Room"",
            ""id"": ""6080fbf2-68e1-49e7-88b3-07334c1391fc"",
            ""actions"": [
                {
                    ""name"": ""EnvironmentControl"",
                    ""type"": ""Button"",
                    ""id"": ""e55a2c6a-3964-4ed6-a5e7-07014e379b5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""f2a7e9f4-74ec-490f-86f9-1c24460f3713"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""2645cf52-58d9-4561-89db-eee55fdc4a6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""e42034cb-b2f8-458e-a6dd-84093af62178"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""44fbc95b-e358-4f9a-8832-0658df0605bc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnvironmentControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""439a0e49-1959-4e79-bbe5-bf473028c79f"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f73efa78-8581-443c-ad62-fc503083608c"",
                    ""path"": ""<XRController>{RightHand}/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b95acc15-43f2-4206-827f-279ed271429d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b84f50f1-0b50-40fa-961e-cef624590af0"",
                    ""path"": ""<XRController>{RightHand}/{SecondaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef65488a-ff44-43ed-80d5-0430a0cbecd4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Room
        m_Room = asset.FindActionMap("Room", throwIfNotFound: true);
        m_Room_EnvironmentControl = m_Room.FindAction("EnvironmentControl", throwIfNotFound: true);
        m_Room_PauseToggle = m_Room.FindAction("Pause Toggle", throwIfNotFound: true);
        m_Room_Interact = m_Room.FindAction("Interact", throwIfNotFound: true);
        m_Room_Click = m_Room.FindAction("Click", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Room
    private readonly InputActionMap m_Room;
    private List<IRoomActions> m_RoomActionsCallbackInterfaces = new List<IRoomActions>();
    private readonly InputAction m_Room_EnvironmentControl;
    private readonly InputAction m_Room_PauseToggle;
    private readonly InputAction m_Room_Interact;
    private readonly InputAction m_Room_Click;
    public struct RoomActions
    {
        private @Controls m_Wrapper;
        public RoomActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @EnvironmentControl => m_Wrapper.m_Room_EnvironmentControl;
        public InputAction @PauseToggle => m_Wrapper.m_Room_PauseToggle;
        public InputAction @Interact => m_Wrapper.m_Room_Interact;
        public InputAction @Click => m_Wrapper.m_Room_Click;
        public InputActionMap Get() { return m_Wrapper.m_Room; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RoomActions set) { return set.Get(); }
        public void AddCallbacks(IRoomActions instance)
        {
            if (instance == null || m_Wrapper.m_RoomActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RoomActionsCallbackInterfaces.Add(instance);
            @EnvironmentControl.started += instance.OnEnvironmentControl;
            @EnvironmentControl.performed += instance.OnEnvironmentControl;
            @EnvironmentControl.canceled += instance.OnEnvironmentControl;
            @PauseToggle.started += instance.OnPauseToggle;
            @PauseToggle.performed += instance.OnPauseToggle;
            @PauseToggle.canceled += instance.OnPauseToggle;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(IRoomActions instance)
        {
            @EnvironmentControl.started -= instance.OnEnvironmentControl;
            @EnvironmentControl.performed -= instance.OnEnvironmentControl;
            @EnvironmentControl.canceled -= instance.OnEnvironmentControl;
            @PauseToggle.started -= instance.OnPauseToggle;
            @PauseToggle.performed -= instance.OnPauseToggle;
            @PauseToggle.canceled -= instance.OnPauseToggle;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(IRoomActions instance)
        {
            if (m_Wrapper.m_RoomActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRoomActions instance)
        {
            foreach (var item in m_Wrapper.m_RoomActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RoomActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RoomActions @Room => new RoomActions(this);
    public interface IRoomActions
    {
        void OnEnvironmentControl(InputAction.CallbackContext context);
        void OnPauseToggle(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
}
