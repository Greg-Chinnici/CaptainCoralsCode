using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using greg;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
   public int TotalSeconds;
   [SerializeField] private TextMeshProUGUI timerText;
   private int secondsLeft;

   public UnityEvent GameTimerEnd = new UnityEvent();

   private float sec = 0.00f;

   public static Timer Instance;
   
   
   private string formatTime(int s) // Format to MM:SS
   {
      string minutes = (s / 60).ToString() + "m ";
      string seconds = (s % 60).ToString() + "s";

      return minutes + seconds;
   }

   private void Start()
   {
      secondsLeft = TotalSeconds;
      
      if (Instance == null)
      {
         Instance = this;
      }
      else if (Instance != this)
      {
         Destroy(gameObject);
      }
      
   }

   private void Update() // adds using deltatime
   {
      if (secondsLeft <= 0)
      {
         GameTimerEnd.Invoke();
         return;
      }
      
      sec += Time.deltaTime;
      if (sec > 1)
      {
         sec = 0.00f;
         secondsLeft -= 1;
         timerText.text = formatTime(secondsLeft);
      }
   }

   public bool isTimerDone()
   {
      return secondsLeft <= 0;
   }

   public void resetTimer()
   {
      secondsLeft = TotalSeconds;
   }
}
