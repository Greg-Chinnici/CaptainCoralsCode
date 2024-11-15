using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace greg
{
public class WordChecker : MonoBehaviour
{
    [SerializeField] private LetterStack _letterStack;
    [SerializeField] private Logger l; 
    
    public void CheckWord(string word = "hello")
    {
        _letterStack.isCheckingWord = true;
        StartCoroutine(profanityFilter(word));
    }

    
    private IEnumerator profanityFilter(string word)
    {
        var profanityFilter = "https://www.purgomalum.com/service/containsprofanity?text=" + word;
        using var p = UnityWebRequest.Get(profanityFilter);
        var filter = p.SendWebRequest();

        while (filter.isDone == false)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        if (p.result == UnityWebRequest.Result.Success)
        {
            l.Log($" {p.downloadHandler.text}");
            if (p.downloadHandler.text == "true")
            {
                l.Log("BAD WORD");
                _letterStack.isCheckingWord = false;
                _letterStack.LevelNotPassed();
            }
            else
            {
                // if no profanity, launch the coroutine to check spelling
                StartCoroutine(isWordValid(word));
            }
        }else{
            l.Log("That is not a word");
            l.Log($"Failed {p.error}");
            _letterStack.isCheckingWord = false;
            _letterStack.LevelNotPassed();
        }

    }

    private IEnumerator isWordValid(string word)
    {
        //dictionary text
        var url = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word;
        using var www = UnityWebRequest.Get(url);
        var operation = www.SendWebRequest();

        while (operation.isDone == false)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (www.result == UnityWebRequest.Result.Success){
            l.Log("That is a Word");
            l.Log($" {www.downloadHandler.text}");
           
            // maybe break the json down and display the definition
            _letterStack.ShowWordStats(www.downloadHandler.text);
            
            _letterStack.isCheckingWord = false;
            _letterStack.LevelPassed();
        }else{
            l.Log("That is not a word");
            l.Log($"Failed {www.error}");
            _letterStack.isCheckingWord = false;
            _letterStack.LevelNotPassed();
        }
        
    }


}
    
}
