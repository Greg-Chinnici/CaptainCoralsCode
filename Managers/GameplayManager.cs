using System;
using System.Collections;
using UnityEngine;
 
namespace greg
{
public class GameplayManager : MonoBehaviour
{
    
    [SerializeField] private LetterStack letterStack;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private FishSpawner fishSpawner;
    [SerializeField] private PowerUpBar powerUpBar;
    [SerializeField] private FishSeen fishSeen;
    
    [SerializeField] private Logger l;

    private Coroutine endGame;

    public static GameplayManager Instance;
    
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;

        fishSeen = FindObjectOfType<FishSeen>();
    }

    public void click(Vector3 screenPos)
    {
        if (isGamePaused()) // cancels catch if paused
        {
            return;
        }

        // Camera is origin of cast
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit[] allHits;
        allHits = Physics.RaycastAll(ray, 20f);
        foreach (RaycastHit info in allHits) // Had to do this because it is colliding with the color volume
        {

            l.Log("Hit: " + info.collider.tag);

            if (info.collider.CompareTag("Fish")) 
            {

                if (letterStack.canAddLetter())
                {
                    CatchFish(info.collider);
                }
                else
                {
                    //give a status message, it will do a sound still, but maybe some kind of popup
                    unableToCatchFish();
                }

                return;
            }

        }
        
        // no hit
        
       
    }
    

    private void CatchFish(Collider fishHit) // if a fish is found, catch it and use the letter
    {
        AudioManager.Instance.PlayFishCaughtSound();
        
        fishSeen.SawFish(fishHit.GetComponent<SpriteRenderer>().sprite.name);
        
        Fish f = fishHit.GetComponent<Fish>();
        letterStack.AddLetter(f.GetLetter());
        f.Catch();
    }

    private void unableToCatchFish() // if stack is full
    {
        l.Log("Letter Stack is Full, not adding Fish");
        // play a sound or something
    }

    public void restartLevel()
    {
        Timer.Instance.resetTimer();

        ScoreAndLeaderboard.Instance.resetScore();
        
        letterStack.resetLetterStack();
        ObjectPool.instance.KillAllFish();
        fishSpawner.StartGame();
        powerUpBar.ResetBar();
        
    }

    public void TryPowerUp() // uses powerup bar, cancels if paused. (Keep Progress)
    {
        if (pauseMenu.isGamePaused()){return;}
        powerUpBar.UsePowerUp();
    }

    public void ClearStack()
    {
        letterStack.ClearWord();
    }
    
    public void PauseMenuClick() // Toggle Pause Menu
    {
        pauseMenu.PauseClick();
    }

    public bool isGamePaused() //returns if the game is paused
    {
        return pauseMenu.isGamePaused();
    }

    public void SaveFishCount() // When the game Quits, save progress to the Player Prefs
    {
        fishSeen.Save();
    }

    public void endtheGame() // When the timer runs out, wait a bit before returning to main menu
    {
        if (endGame == null)
        {
            print("Game stopping");
            endGame = StartCoroutine(endWithTimer());
        }
    }

    private IEnumerator endWithTimer() // Waits for 4 seconds
    {
        yield return new WaitForSecondsRealtime(4);
        pauseMenu.QuitGame();
    }
}
    
}
