using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{

    [SerializeField] GamepadCursor cursor;
    
    [SerializeField] private GameObject uiCanvas;
    private GraphicRaycaster uiRaycaster;

    private PointerEventData clickInfo;
    private List<RaycastResult> clickResults;
    
    

    private void Start()
    {
        uiRaycaster = uiCanvas.GetComponent<GraphicRaycaster>();
        clickInfo = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
    }
    

    public bool ClickedOnUIElements(Vector2 pos)
    {
        clickInfo.position = pos;
        clickResults.Clear();
        
        uiRaycaster.Raycast(clickInfo , clickResults);
        
        foreach (RaycastResult result in clickResults)
        {
            GameObject uiElement = result.gameObject;
            Button b = uiElement.GetComponent<Button>();
            if (b != null && b.interactable)
            {
                b.onClick.Invoke();
                
                AudioManager.Instance.PlayButtonClickSound();
                
                return true;
            }
        }
        return false;
    }

    public void ChangeTargetCanvas(GameObject c)
    {
        Debug.Log("Changed to: " + c.name);
        uiRaycaster = c.GetComponent<GraphicRaycaster>();
    }
}
