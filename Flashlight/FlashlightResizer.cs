using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightResizer : MonoBehaviour
{
   [SerializeField] private RectTransform imageTransform;
   [SerializeField] private int width;
   [SerializeField] private int height;

   private Vector2 dimensions;
   private void Start()
   {
      dimensions = new Vector2(width, height);
      imageTransform.sizeDelta = dimensions;
   }

   public void ChangeCircleSize(float x, float y)
   {
      dimensions = new Vector2(x, y);
      imageTransform.sizeDelta = dimensions;
   }

   public Vector2 CurrentSize()
   {
      return dimensions;
   }
}
