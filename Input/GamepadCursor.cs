
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using MouseButton = UnityEngine.InputSystem.LowLevel.MouseButton;

namespace greg
{
    public class GamepadCursor : MonoBehaviour
    {
        [Header("Input Module")] [SerializeField]
        private PlayerInput playerInput;

        private const string gamepadScheme = "Gamepad";
        private const string mouseScheme = "Keyboard&Mouse";
        private string PreviousControlScheme = "";
        private PlayerInput_V2 playerInputV2;


        [SerializeField] private UIInteraction uiInteraction;


        [Header("Cursor Things")] [SerializeField]
        private Texture2D cursorIcon;

        [SerializeField] private RectTransform cursorTransform;
        [SerializeField] private Canvas cursorCanvas;
        [SerializeField] private float cursorSpeedBase = 1000f;
        [SerializeField] private float cursorPadding = 0f;

        private Mouse virtualMouse;
        private Mouse currentMouse;

        private bool previousMouseState;
        private Camera mainCamera;

        private Vector2 controllerOffset = new Vector2(0, -25); // in Pixels

        private enum InputType
        {
            Mouse,
            Controller
        }

        private InputType currentInput;

        private void Awake()
        {
            playerInputV2 = new PlayerInput_V2();
        }

        private void OnEnable()
        {
            playerInputV2.Player.Enable();
            playerInputV2.UI.Enable();

            mainCamera = Camera.main;
            currentMouse = Mouse.current;

            if (virtualMouse == null)
            {
                virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            }
            else if (virtualMouse.added == false)
            {
                InputSystem.AddDevice(virtualMouse);
            }

            InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

            if (cursorTransform == null)
            {
                Vector2 pos = cursorTransform.anchoredPosition;
                InputState.Change(virtualMouse.position, pos);
            }

            InputSystem.onAfterUpdate += updateMotion;

            //playerInput.onControlsChanged += onControlsChanged;
            currentInput = InputType.Mouse;
            //currentInput = InputType.Controller;
            onControlsChanged(currentInput);
        }

        private void OnDisable()
        {
            playerInputV2.Player.Disable();
            playerInputV2.UI.Disable();

            if (virtualMouse != null) InputSystem.RemoveDevice(virtualMouse);
            InputSystem.onAfterUpdate -= updateMotion;

        }

        private void updateMotion()
        {
            if (virtualMouse == null && Gamepad.current == null)
            {
                return;
            }

            Vector2 newPos;
            if (Gamepad.current == null)
            {

                Vector2 delta = virtualMouse.delta.value;
                delta *= cursorSpeedBase * Time.unscaledDeltaTime;

                Vector2 currentPos = virtualMouse.position.ReadValue();
                newPos = currentPos + delta;

                newPos.x = Mathf.Clamp(newPos.x, cursorPadding, 1920 - cursorPadding);
                newPos.y = Mathf.Clamp(newPos.y, cursorPadding, 1080 - cursorPadding);

                InputState.Change(virtualMouse.position, newPos);
                InputState.Change(virtualMouse.delta, delta);
            }
            else
            {
                Vector2 delta = Gamepad.current.leftStick.ReadValue();
                delta *= cursorSpeedBase * Time.unscaledDeltaTime;

                Vector2 currentPos = virtualMouse.position.ReadValue();
                newPos = currentPos + delta;

                newPos.x = Mathf.Clamp(newPos.x, cursorPadding, Screen.width - cursorPadding);
                newPos.y = Mathf.Clamp(newPos.y, cursorPadding, Screen.height - cursorPadding);

                InputState.Change(virtualMouse.position, newPos);
                InputState.Change(virtualMouse.delta, delta);
            }

            anchorCursor(newPos);

            if (playerInputV2.UI.Click.WasReleasedThisFrame() || playerInputV2.Player.CatchFish.WasReleasedThisFrame())
            {
                genericClick(getScreenPos());
            }
        }

        private void anchorCursor(Vector2 p)
        {
            Vector2 anchoredPos = p;
            cursorTransform.anchoredPosition = anchoredPos;
        }

        private void onControlsChanged(InputType it)
        {
            if (it == InputType.Mouse)
            {
                Debug.Log("Changing Input to Mouse");

                Cursor.visible = true;
                Cursor.SetCursor(cursorIcon, Vector2.one / 2, CursorMode.Auto);

                cursorTransform.gameObject.SetActive(false);
                currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
                PreviousControlScheme = mouseScheme;
            }
            else if (it == InputType.Controller)
            {
                Debug.Log("Changing Input to Gamepad");

                Cursor.visible = false;
                cursorTransform.gameObject.SetActive(true);
                InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
                anchorCursor(currentMouse.position.ReadValue());
                PreviousControlScheme = gamepadScheme;
            }

        }

        public void SwapToController()
        {
            currentInput = InputType.Controller;
            onControlsChanged(currentInput);
        }

        public void SwapToMouse()
        {
            currentInput = InputType.Mouse;
            onControlsChanged(currentInput);
        }

        public Vector2 getScreenPos()
        {
            if (currentInput == InputType.Mouse)
            {

                //return Mouse version position
                return Mouse.current.position.ReadValue() - (Vector2.one * 10);
            }
            else
            {
                // return gamepad version position
                return cursorTransform.anchoredPosition + controllerOffset;
            }
        }

        private void genericClick(Vector2 p)
        {
            bool hitButton = uiInteraction.ClickedOnUIElements(p);

            Debug.Log("Looking at: " + p);
            Debug.Log("Current Input: " + currentInput.ToString());

            if (GameplayManager.Instance != null && hitButton == false)
            {
                if (currentInput == InputType.Mouse)
                {
                    // Mouse version position
                    GameplayManager.Instance.click(Mouse.current.position.ReadValue() - (Vector2.one * 10));
                }
                else
                {
                    //  gamepad version position
                    GameplayManager.Instance.click(p);
                }
            }
        }
    }
}