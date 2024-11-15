using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashlightMover : MonoBehaviour
{
    [SerializeField] private GamepadCursor gamepadCursor;
    [SerializeField] private RectTransform moveTo;
    
  
    private void Update()
    {
        moveTo.anchoredPosition = gamepadCursor.getScreenPos();
    }
}
