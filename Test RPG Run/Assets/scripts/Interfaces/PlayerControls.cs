//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/scripts/Interfaces/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""6d5724cc-da7f-4430-bae9-ba265a587eea"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0d4fb714-1068-4979-830c-52226483b7e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpellCast"",
                    ""type"": ""Value"",
                    ""id"": ""cc07bb5f-9b4f-4a2d-b37d-e2b9dae04882"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpellCastGamepad"",
                    ""type"": ""Value"",
                    ""id"": ""30c9710f-98d9-4ce5-b8fe-6ae7f7dabbc8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""67f1efe1-5c4e-46e3-917a-03e1e5d0a958"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpellSelectMouseScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f796bd89-a1b8-4e37-b174-da804cfa668a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=1.401298E-45)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpellSelectGamepad"",
                    ""type"": ""Button"",
                    ""id"": ""ebc63f55-f436-4730-bc1e-a218292c2872"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""40ac495c-3e22-46a7-b302-48e96bcdc1d4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""b0e99a00-b5ba-4b14-a983-b05b698d0571"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpellCastGamepad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ceb899e3-0afc-4a5b-b092-a83ef9ea6494"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellCastGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c35fb66d-e25b-49af-87f9-3cc9c188f6c6"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellCastGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ec8f443-e9c3-41e8-adfb-952107da524d"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellCastGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""561f5a64-0172-4741-bcf2-1d05483e59bc"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellCastGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""248e026d-9e48-431f-ad7f-7f7c7d235594"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa3b471c-e5c6-4dc0-9fcf-d1092c7cf1b6"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef998617-b8fa-4e63-9530-5c16db0335fb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""237484c4-0534-4213-ae88-0b83aa2f126d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5f11e3c3-6939-4042-853d-7c15f701d697"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""79f2dafc-de65-4cba-9f4d-9cebee7aa37c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7ce09d22-c488-433c-a74c-7527d30e6014"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9df4a7fc-086f-4b49-b921-91e68e2feac4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""bde9fed9-1461-4065-88ea-3cb30788b46c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""65b41ab3-bdde-4cd6-9d7d-9d6916f0549c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cde41ba6-0b5f-4b95-b3a9-16de9b7c4e46"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""400433f5-6b63-46c5-8c4a-c3a51784b386"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""777ee9f1-3e72-4674-8bef-e5d28de4f9fd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""864209fd-fbaf-4343-b467-246de890c428"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""f4693370-7e99-435b-b77b-735cdc037c14"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""22580b65-c908-4cfd-984f-9aa0cd0e90d0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""410e450d-dbd7-4e0b-a499-2dcbb7b9d38c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""0cc112b5-d08a-49ef-846a-f181ddb4449a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b708ffe5-384e-4d21-8bb5-c36075cff1a8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellCast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abc60daf-32cd-4959-a7be-400eae590866"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellSelectMouseScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99441b51-6880-44e0-a480-276cec7d09d4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellSelectGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88f8354e-0958-479c-a0fb-861f0644bcfd"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SpellSelectGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89cace1d-bacc-49c7-b092-d96715687a25"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Movement = m_PlayerActions.FindAction("Movement", throwIfNotFound: true);
        m_PlayerActions_SpellCast = m_PlayerActions.FindAction("SpellCast", throwIfNotFound: true);
        m_PlayerActions_SpellCastGamepad = m_PlayerActions.FindAction("SpellCastGamepad", throwIfNotFound: true);
        m_PlayerActions_Interact = m_PlayerActions.FindAction("Interact", throwIfNotFound: true);
        m_PlayerActions_SpellSelectMouseScrollWheel = m_PlayerActions.FindAction("SpellSelectMouseScrollWheel", throwIfNotFound: true);
        m_PlayerActions_SpellSelectGamepad = m_PlayerActions.FindAction("SpellSelectGamepad", throwIfNotFound: true);
        m_PlayerActions_MousePosition = m_PlayerActions.FindAction("MousePosition", throwIfNotFound: true);
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

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Movement;
    private readonly InputAction m_PlayerActions_SpellCast;
    private readonly InputAction m_PlayerActions_SpellCastGamepad;
    private readonly InputAction m_PlayerActions_Interact;
    private readonly InputAction m_PlayerActions_SpellSelectMouseScrollWheel;
    private readonly InputAction m_PlayerActions_SpellSelectGamepad;
    private readonly InputAction m_PlayerActions_MousePosition;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerActions_Movement;
        public InputAction @SpellCast => m_Wrapper.m_PlayerActions_SpellCast;
        public InputAction @SpellCastGamepad => m_Wrapper.m_PlayerActions_SpellCastGamepad;
        public InputAction @Interact => m_Wrapper.m_PlayerActions_Interact;
        public InputAction @SpellSelectMouseScrollWheel => m_Wrapper.m_PlayerActions_SpellSelectMouseScrollWheel;
        public InputAction @SpellSelectGamepad => m_Wrapper.m_PlayerActions_SpellSelectGamepad;
        public InputAction @MousePosition => m_Wrapper.m_PlayerActions_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @SpellCast.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCast;
                @SpellCast.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCast;
                @SpellCast.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCast;
                @SpellCastGamepad.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCastGamepad;
                @SpellCastGamepad.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCastGamepad;
                @SpellCastGamepad.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellCastGamepad;
                @Interact.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @SpellSelectMouseScrollWheel.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectMouseScrollWheel;
                @SpellSelectMouseScrollWheel.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectMouseScrollWheel;
                @SpellSelectMouseScrollWheel.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectMouseScrollWheel;
                @SpellSelectGamepad.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectGamepad;
                @SpellSelectGamepad.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectGamepad;
                @SpellSelectGamepad.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpellSelectGamepad;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @SpellCast.started += instance.OnSpellCast;
                @SpellCast.performed += instance.OnSpellCast;
                @SpellCast.canceled += instance.OnSpellCast;
                @SpellCastGamepad.started += instance.OnSpellCastGamepad;
                @SpellCastGamepad.performed += instance.OnSpellCastGamepad;
                @SpellCastGamepad.canceled += instance.OnSpellCastGamepad;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @SpellSelectMouseScrollWheel.started += instance.OnSpellSelectMouseScrollWheel;
                @SpellSelectMouseScrollWheel.performed += instance.OnSpellSelectMouseScrollWheel;
                @SpellSelectMouseScrollWheel.canceled += instance.OnSpellSelectMouseScrollWheel;
                @SpellSelectGamepad.started += instance.OnSpellSelectGamepad;
                @SpellSelectGamepad.performed += instance.OnSpellSelectGamepad;
                @SpellSelectGamepad.canceled += instance.OnSpellSelectGamepad;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSpellCast(InputAction.CallbackContext context);
        void OnSpellCastGamepad(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSpellSelectMouseScrollWheel(InputAction.CallbackContext context);
        void OnSpellSelectGamepad(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
