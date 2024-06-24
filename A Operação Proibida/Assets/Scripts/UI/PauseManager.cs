using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public bool IsPaused { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;

        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void Unpause()
    {
        IsPaused = false;
        Time.timeScale = 1f;

        InputManager.PlayerInput.SwitchCurrentActionMap("Player");
    }
}
