using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GamepadCursor gamepadCursor;
    [SerializeField] private GameObject manager;
    
    enum InputType
    {
    MOUSE,
    GAMEPAD,
    TOUCH
    }
    private InputType currentInputType = InputType.GAMEPAD;

    private void Start()
    {
        DontDestroyOnLoad(manager);
        SceneManager.sceneLoaded += getObjects;
    }

    private void getObjects(Scene s , LoadSceneMode lsm)
    {
        gamepadCursor = FindObjectOfType<GamepadCursor>();
    }

    private void OnInputChange()
    {
        switch (currentInputType)
        {
            case InputType.GAMEPAD:
                break;
            case InputType.MOUSE:
                break;
            case InputType.TOUCH:
                break;
        }
    }

    public void ChangeToMouse()
    {
        if (currentInputType == InputType.MOUSE){return;}
        if (Application.platform == RuntimePlatform.IPhonePlayer) { return; }
        currentInputType = InputType.MOUSE;
        OnInputChange();
    }

    public void ChangeToGamepad()
    {
        if (currentInputType == InputType.GAMEPAD){return;}
        if (Application.platform == RuntimePlatform.IPhonePlayer) { return; }
        if (Gamepad.current == null || Gamepad.all.Count == 0)
        {
            Debug.Log("No Controller");
            return;
        }
        
        currentInputType = InputType.GAMEPAD;
        OnInputChange();
    }

    public void ChangeToTouch()
    {
        if (currentInputType == InputType.TOUCH){return;}

        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            Debug.Log("Not on a phone");
            return;
        }
        currentInputType = InputType.TOUCH;
        OnInputChange();
    }
}
