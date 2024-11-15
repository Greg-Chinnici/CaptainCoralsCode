using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverColor : MonoBehaviour
{
    [SerializeField] private Color hoveredColor;

    [SerializeField] private Button button;
    private ColorBlock buttonColorBlock;
    

    // Start is called before the first frame update
    void Start()
    {
        button.image.color = buttonColorBlock.normalColor;
    }

    public void OnHover()
    {
        button.image.color = hoveredColor;
    }

    public void OnHoverOff()
    {
        button.image.color = buttonColorBlock.normalColor;
    }
}
