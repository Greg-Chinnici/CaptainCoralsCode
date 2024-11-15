using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace greg
{
public class PlayerInputV2 : MonoBehaviour
{
    [SerializeField] private Logger l;
    
    [SerializeField] private PlayerInput playerInput;
    private PlayerInput_V2 playerInputV2;

    [SerializeField] private GamepadCursor gamepadCursor;

    [SerializeField] private Canvas gameplayCanvas;
    [SerializeField] private Canvas pauseCanvas;
    

    // https://learn.unity.com/tutorial/taking-advantage-of-the-input-system-scripting-api?uv=2020.1&projectId=5fc93d81edbc2a137af402b7#5fcad3efedbc2a0020f781e1
    private void Awake()
    {
         
        playerInputV2 = new PlayerInput_V2();
        
        playerInputV2.Player.ClearWord.performed += OnClearStackClick;
        playerInputV2.Player.PauseGame.performed += OnPauseMenuButton;
        playerInputV2.Player.PowerUp.performed += OnPowerUpClick;
        
    }
    
    private void OnEnable()
    {
        playerInputV2.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputV2.Player.ClearWord.performed -= OnClearStackClick;
        playerInputV2.Player.PauseGame.performed -= OnPauseMenuButton;
        playerInputV2.Player.PowerUp.performed -= OnPowerUpClick;
        
        if (playerInputV2 != null) playerInputV2.Player.Disable();
    }

    public void OnPauseMenuButton(InputAction.CallbackContext c)
    {
        GameplayManager.Instance.PauseMenuClick();
        if (GameplayManager.Instance.isGamePaused())
        {
            playerInput.SwitchCurrentActionMap("UI"); 
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Player"); 
        }
           
    }

    public void SwitchToPlayerMap()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
 
    public void OnPowerUpClick(InputAction.CallbackContext c)
    {
        GameplayManager.Instance.TryPowerUp();   
    }

    public void OnClearStackClick(InputAction.CallbackContext c)
    {
        GameplayManager.Instance.ClearStack();
        if (c.started == false)
        {
            return;
        }
      //  GameplayManager.Instance.ClearStack();
    }
}

}
