using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

using System.Collections;

namespace greg
{
public class PauseMenu : MonoBehaviour
{

   [SerializeField] private UIInteraction uiInteraction;
   
   [SerializeField] private CanvasGroup pauseCanvasGroup;
   [SerializeField] private CanvasGroup settingsCanvasGroup;
   
   [SerializeField] private UnityEngine.UI.Slider controllerSens;
   [SerializeField] private UnityEngine.UI.Slider mouseSens;

   private PlayerInputV2 piv2;

   [Header("Canvas with UI")] 
   [SerializeField] private GameObject gameplayUICanvas;
   [SerializeField] private GameObject pauseCanvas;
   [SerializeField] private GameObject settingsCanvas;
   
   private enum MenuStates
   {
      Gameplay,
      Pause,
      Settings
   }

   private MenuStates currentState;
   
   private void Start()
   {
      piv2 = FindObjectOfType<PlayerInputV2>();
      currentState = MenuStates.Gameplay;
      
      CanvasGroupHide(settingsCanvasGroup);
      CanvasGroupHide(pauseCanvasGroup);
      
   }
   
   public void PauseClick() // If "ESCAPE" or any pause alias is pressed
   {
      Debug.Log("Pause Clicked");
      switch (currentState)
      {
         case MenuStates.Gameplay:
            Pause();
            break;
         case MenuStates.Pause:
            UnPause();
            break;
         case MenuStates.Settings:
            ExitSettingMenu();
            break;
      }
   }
   
   public void Pause() // Stops time and shows pause screen
   {

      uiInteraction.ChangeTargetCanvas(pauseCanvas);
      currentState = MenuStates.Pause;
      
      Time.timeScale = 0;

      AudioManager.Instance.PauseMenuSoundsEnter();
      
      CanvasGroupShow(pauseCanvasGroup);
   }

   public void UnPause() // resumes time, and hides pause screen
   {
      uiInteraction.ChangeTargetCanvas(gameplayUICanvas);

      currentState = MenuStates.Gameplay;
      
      Time.timeScale = 1;
      
      AudioManager.Instance.PauseMenuSoundsExit();
      
      piv2.SwitchToPlayerMap(); // BAd fix but whatever
      
      CanvasGroupHide(settingsCanvasGroup);
      CanvasGroupHide(pauseCanvasGroup);
   }
   
   public void ShowSettingMenu() // opens pause menu
   {
      Debug.Log("Showing settings");
      
      uiInteraction.ChangeTargetCanvas(settingsCanvas);
      
      currentState = MenuStates.Settings;
      
      CanvasGroupShow(settingsCanvasGroup);
   }

   public void ExitSettingMenu() // hides pause menu
   {
      uiInteraction.ChangeTargetCanvas(pauseCanvas);

      currentState = MenuStates.Pause;
      
      CanvasGroupHide(settingsCanvasGroup);
   }

   public void QuitGame() // quits the level, either on "QUIT" button or timer over
   {
      Time.timeScale = 1.0f;
      
      GameplayManager.Instance.SaveFishCount();
      
      SceneManager.LoadScene("MainMenu_GChinnici");
   }

   public void RestartButton() // calls reset in gameplay manager
   {
      GameplayManager.Instance.restartLevel();
      UnPause();
   }
   public bool isGamePaused() // if the player is not able to play, it is paused
   {
      return currentState != MenuStates.Gameplay;
   }
   
   private void CanvasGroupHide(CanvasGroup cg) // util to hide canvas
   {
      cg.alpha = 0.0f;
      cg.enabled = false;
      cg.blocksRaycasts = false;
      cg.interactable = false;
      cg.GetComponentInParent<Canvas>().enabled = false;
   }
   
   private void CanvasGroupShow(CanvasGroup cg) // util to show canvas
   {
      cg.alpha = 1.0f;
      cg.enabled = true;
      cg.blocksRaycasts = true;
      cg.interactable = true;
      cg.GetComponentInParent<Canvas>().enabled = true;

   }
   
}
   
}
