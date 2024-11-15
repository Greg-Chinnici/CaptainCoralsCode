using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FishSeen : MonoBehaviour
{
    [SerializeField] GameObject SaveAndLoadOBJ;

    private Dictionary<string, int> CaughtNumbers;
    
    public static FishSeen Instance;
    private void Awake()
    {
       if (Instance == null)
       {
          Instance = this;
       }
       else if (Instance != this)
       {
          Destroy(gameObject);
       }
    }
   private void Start()
   {
      Load();
      
      DontDestroyOnLoad(SaveAndLoadOBJ);
   }
   
   
   public void SawFish(string spriteName)
   {
      if (CaughtNumbers.ContainsKey(spriteName))
      {
         CaughtNumbers[spriteName] += 1;  
      }
      else
      {
         CaughtNumbers.Add(spriteName , 1);  
      }
       
   }

   public void Save()
   {
      PlayerSave.SavePlayerPrefs(CaughtNumbers);
   }

   public void Load()
   {
      CaughtNumbers = PlayerSave.LoadPlayerPrefs();
   }

   public void ClearCaughtMemories()
   {
      PlayerSave.ResetPlayerPrefs();
      Load();
   }
   
   public Dictionary<string, int> GetFishSeen()
   {
      return CaughtNumbers;
   }
   

}

