using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncyclopediaMenu : MonoBehaviour
{
   [SerializeField] private string MainMenuSceneName;
   [SerializeField] private CanvasGroup mindwipeConfrim;

   private void Start()
   {
      HideMindwipeConfirm();
   }

   public void ChangeToMainMenu()
   {
      SceneManager.LoadScene(MainMenuSceneName);
   }

   public void ShowMindwipeConfirm()
   {
      mindwipeConfrim.alpha = 1;
      mindwipeConfrim.interactable = true;
      mindwipeConfrim.blocksRaycasts = true;
   }

   public void HideMindwipeConfirm()
   {
      mindwipeConfrim.alpha = 0;
      mindwipeConfrim.interactable = false;
      mindwipeConfrim.blocksRaycasts = false;
   }
   
   
}
