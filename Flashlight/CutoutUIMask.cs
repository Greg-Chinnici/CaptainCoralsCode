using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;


public class CutoutUIMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material baseMat =  new Material(base.materialForRendering);
            baseMat.SetInt("_StencilComp" , (int)CompareFunction.NotEqual);
            return baseMat;
        }
        
    }
}
