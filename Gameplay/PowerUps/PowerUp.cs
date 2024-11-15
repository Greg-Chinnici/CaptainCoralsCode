using System.Collections;
using UnityEngine;

namespace greg
{
    public class PowerUp : MonoBehaviour
    {
        // Base Power up class
        public Color barColor = Color.gray;
        public Sprite powerIcon;
        public float recoverSpeed = 0.025f;
        public float castTime = 4.0f;

        public virtual IEnumerator castPowerUp()
        {
            yield return null;
        }
    }   
}
