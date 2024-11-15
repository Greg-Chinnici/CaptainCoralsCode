 
using UnityEngine;
using UnityEngine.SceneManagement;

namespace greg
{

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup titleScreen;
        [SerializeField] private CanvasGroup levelSelect;
        [SerializeField] private CanvasGroup optionsScreen;
        [SerializeField] private CanvasGroup tutorialScreen;

        private CanvasGroup currentActiveCanvaGroup;
        
        public string CoralSceneName;
        public string DeepOceanSceneName;
        public string EncyclopediaSceneName;


        private void Start()
        {
            hideCanvasGroup(levelSelect);
            hideCanvasGroup(tutorialScreen);
            hideCanvasGroup(optionsScreen);
            ShowMainMenu();
            
            AudioManager.Instance.PlayBackgroundAmbiance();
            AudioManager.Instance.PauseMenuSoundsEnter();
        }

        public void ShowMainMenu()
        {
            showCanvasGroup(titleScreen);
            if (currentActiveCanvaGroup != null)
                hideCanvasGroup(currentActiveCanvaGroup);
            currentActiveCanvaGroup = titleScreen;
        }

        public void ShowLevelSelect()
        {
            showCanvasGroup(levelSelect);
            hideCanvasGroup(currentActiveCanvaGroup);
            currentActiveCanvaGroup = levelSelect;
        }
        public void ShowOptions()
        {
            showCanvasGroup(optionsScreen);
            hideCanvasGroup(currentActiveCanvaGroup);
            currentActiveCanvaGroup = optionsScreen;
        }
        public void showTutorialScreen()
        {
            showCanvasGroup(tutorialScreen);
            hideCanvasGroup(currentActiveCanvaGroup);
            currentActiveCanvaGroup = tutorialScreen;
        }

        public void ChangeToCoralScene()
        {
            SceneManager.LoadScene(CoralSceneName);
        }

        public void ChangeToNightScene()
        {
            SceneManager.LoadScene(DeepOceanSceneName);
        }

        public void ChangeToEncyclopediaScene()
        {
            SceneManager.LoadScene(EncyclopediaSceneName);
        }

        private void showCanvasGroup(CanvasGroup c)
        {
            c.alpha = 1;
            c.blocksRaycasts = true;
            c.interactable = true;
        }

        private void hideCanvasGroup(CanvasGroup c)
        {
            c.alpha = 0;
            c.blocksRaycasts = false;
            c.interactable = false;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}


