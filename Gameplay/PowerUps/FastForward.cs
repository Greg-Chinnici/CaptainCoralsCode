using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace greg
{
    public class FastForward : PowerUp
    {
        public override IEnumerator castPowerUp() // Fast forwards time and sets back to normal timescale
        {

            Time.timeScale = GlobalSettings.FastForwardTimescale;
      
            yield return new WaitForSecondsRealtime(castTime);

            yield return new WaitUntil(() => GameplayManager.Instance.isGamePaused() == false);
      
            Time.timeScale = 1.0f;
        }
    }   
}

