
using TMPro;
using UnityEngine;

namespace greg
{
public class LetterStack : MonoBehaviour
{
   [SerializeField] private WordChecker _wordChecker;
   [SerializeField] private ScoreAndLeaderboard _scoreAndLeaderboard;
   [SerializeField] private int wordLength = GlobalSettings.MinimumWordLength;

   [SerializeField] private TextMeshPro _textMeshPro;
   private int currentLetters = 0;
   
   private string displayWord = "";
   [HideInInspector] public bool isCheckingWord = false;

   [SerializeField] private Logger l;
   


   private void Start()
   {
      if (_wordChecker == null)
         _wordChecker = new WordChecker();
      
      if (_scoreAndLeaderboard == null)
         _scoreAndLeaderboard = new ScoreAndLeaderboard();
      
      displayWord = new string ('_', wordLength);

      _textMeshPro = GetComponent<TextMeshPro>();

   }
   
   private void Update()
   {
      if (currentLetters == wordLength && isCheckingWord == false) // Auto calls the Validator
      {
         waitToCheckWord();
      }

      _textMeshPro.text = displayWord;
   }

   private void waitToCheckWord()
   {
      _wordChecker.CheckWord(displayWord); // Sends the full string to the Validator
   }

   public void LevelPassed() // Increases word length and Wraps around to 3 letter from 6
   {
      l.Log(isCheckingWord.ToString());
      
      // Pass the word onto the scoreboard here
      
      wordLength += 1;
      if (wordLength > GlobalSettings.MaxWordLength) // resets the length to min value
         wordLength = GlobalSettings.MinimumWordLength;
      
      AudioManager.Instance.PlayValidWordSound();
      _scoreAndLeaderboard.addCompletedWord(displayWord);
      
      ClearWord();
   }

   
   public void LevelNotPassed() // Clears word , same length
   {
      l.Log(isCheckingWord.ToString());
      
      AudioManager.Instance.PlayInvalidWordSound();
      ClearWord();
   }

   public void ClearWord() // resets progress
   {
      currentLetters = 0;
      displayWord = new string ('_', wordLength);
      _textMeshPro.text = displayWord;
   }

   public void AddLetter(string letter) // replaces farthest underscore with new letter, cursed C# happens here
   {
      // C# string are immutable so here's thing awful thing, fix this later
      char[] charArr = displayWord.ToCharArray();
      charArr[currentLetters] = letter.ToCharArray()[0]; //lmao 
      displayWord = new string(charArr);
      
      currentLetters += 1;
   }

   public bool canAddLetter() // if word is full, dont add
   {
      if (currentLetters == wordLength)
         return false;
      return true;
   }

   public void ShowWordStats(string json) // Has definitions and dictionary information
   {
      //parse the json here
      l.Log(json);
   }

   public void resetLetterStack() // Full reset
   {
      wordLength = GlobalSettings.MinimumWordLength;
      ClearWord();
   }

}
   
}
