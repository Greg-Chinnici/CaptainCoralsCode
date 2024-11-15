using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace greg
{
    public class ScoreAndLeaderboard : MonoBehaviour
    {


        private Dictionary<string, int> completedWords = new Dictionary<string, int>();
        [SerializeField] private TextMeshProUGUI scoreText;

        private int currentScore = 0;

        private int pointMult = 100;

        public static ScoreAndLeaderboard Instance;

        // Start is called before the first frame update
        void Start()
        {
            scoreText.text = currentScore.ToString();

            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }


        public void addCompletedWord(string w)
        {
            int s = increaseScore(w);
            completedWords.Add(w, s);
        }

        private int increaseScore(string w)
        {
            int s = PointCalculator.totalPointsInWord(w);

            s *= pointMult;

            currentScore += s;

            scoreText.text = currentScore.ToString();
            return s;
        }

        public int getScore()
        {
            return currentScore;
        }

        public void resetScore()
        {
            currentScore = 0;
            scoreText.text = currentScore.ToString();
        }


    }
}