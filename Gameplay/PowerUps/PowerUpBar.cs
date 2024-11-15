

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace greg
{
public class PowerUpBar : MonoBehaviour
{
    //[SerializeField] private Image bar;
    private Image powerPic;
    [SerializeField] private Image powerPicBG;
    
    private float recoverSpeed = 0.05f;
    
    [SerializeField] private PowerUp powerUp;

    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderForeground;
    [SerializeField] private Image sliderBackground;

    
    // Flashing if Available
    private Color flashColor1 = new(1f,0.5f,0.5f);
    private Color flashColor2 = new(0,1f,0.25f);
    
    private bool isflashing = false;
    
    private void Start()
    {
        sliderForeground.color = powerUp.barColor;
        sliderBackground.color = new Color(0,0.2f,0.5f); // darker i think
        
        recoverSpeed = powerUp.recoverSpeed;
        
        powerPic.sprite = powerUp.powerIcon; // not working, not inheriting to the picture. still giving error
        
    }

    public void ResetBar()
    {
        slider.value = 0.0f;
    }

    private void FixedUpdate()
    {

        if (slider.value >= 1.0f)
        {
            if (System.DateTime.Now.Millisecond % 2 == 0)
            {
                powerPicBG.color = flashColor1;
            }
            else
            {
                powerPicBG.color = flashColor2;
            }
        }
        else
        {
            powerPicBG.color = Color.white;
        }

        slider.value += recoverSpeed * Time.unscaledDeltaTime;
    }

    public void UsePowerUp()
    {
        if (slider.value != 1.0f){return;} // quits if not ready
        
        slider.value = 0.0f;
        
        StartCoroutine(powerUp.castPowerUp());
    }
    
    
    
}
    
}
