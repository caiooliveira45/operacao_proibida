using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static PlayerInput PlayerInput;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool DashPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool PausePressed { get; private set; }
    public bool UnpauseInput { get; private set; }

    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;
    private InputAction _dashInputAction;
    private InputAction _interactInputAction;
    private InputAction _pauseInputAction;

    private InputAction _unpauseInputAction;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        PlayerInput = GetComponent<PlayerInput>();

        _moveInputAction = PlayerInput.actions["Move"];
        _jumpInputAction= PlayerInput.actions["Jump"];
        _dashInputAction = PlayerInput.actions["Dash"];
        _interactInputAction = PlayerInput.actions["Interact"];
        _pauseInputAction = PlayerInput.actions["Pause"];

        _unpauseInputAction = PlayerInput.actions["Unpause"];
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = _moveInputAction.ReadValue<Vector2>();
        JumpPressed = _jumpInputAction.WasPressedThisFrame();
        DashPressed = _dashInputAction.WasPressedThisFrame();
        InteractPressed = _interactInputAction.WasPressedThisFrame();
        PausePressed = _pauseInputAction.WasPressedThisFrame();

        UnpauseInput = _pauseInputAction.WasPressedThisFrame();
    }
}
