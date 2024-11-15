using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace greg
{
    
    public class IncreaseVisibility : PowerUp
    {
        [SerializeField] private FlashlightResizer resizer;
        [SerializeField] private int increasePixelSizeBy = 200;
        public override IEnumerator castPowerUp() // uses flashlight resizer to increase the radius for a few seconds
        {
            Vector2 initSize = resizer.CurrentSize();
            
            resizer.ChangeCircleSize(initSize.x + increasePixelSizeBy , initSize.y + increasePixelSizeBy);

            yield return new WaitForSecondsRealtime(castTime);
            
            resizer.ChangeCircleSize(initSize.x , initSize.y );

        }
    }
}