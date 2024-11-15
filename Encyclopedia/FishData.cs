using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(order = 1, menuName = "Fish Card", fileName = "blank card")]
public class FishData : ScriptableObject
{
    public string DisplayName = String.Empty;
    public string Description = String.Empty;
    public Sprite RealWorldPicure;
    public Sprite PixelPic;
    public string FishType = String.Empty;
}
