using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Encyclopedia : MonoBehaviour
{

    [Header("Data Display")]
    private FishData currentCard;

    [SerializeField] private TextMeshProUGUI timesCaught;
    [SerializeField] private TextMeshProUGUI displayFishName;
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private Image cardBgColor;
    
    [Header("All Seen Fish")]
    private FishSeen fishSeen;
    private Dictionary<string , FishData> FishCards = new Dictionary<string, FishData>(); //sprite name to card

    [SerializeField] private List<FishData> fd = new List<FishData>(); // All Fish Data Scriptable Objects
    [SerializeField] private FishData QuestionMarkFish; // Placeholder for unknown fish
        
    private Dictionary<string, int> CaughtCount;

    [Header("Button Spawning")]
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject parentContainer;

    public static Encyclopedia Instance = null;
    
    
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
        fishSeen = FindObjectOfType<FishSeen>();
        CaughtCount = fishSeen.GetFishSeen();
        
        // spawn in all required buttons (length of dictionary)
        if (parentContainer == null) return;
        
        foreach (var VARIABLE in fd)
        {
            GameObject but = Instantiate(button, parentContainer.transform);
            EnclopediaButton e =  but.GetComponent<EnclopediaButton>();
            e.setSprite(VARIABLE.PixelPic);
            
            FishCards.Add( VARIABLE.PixelPic.name ,VARIABLE);
        }
    }

    public void OnEncyclopediaButtonClick(string spriteName)
    {
        if (FishCards.ContainsKey(spriteName) == false)
        {
            return;
        }
        swapCards(FishCards[spriteName] , spriteName);
        
    }
    
    private void swapCards(FishData newCard, string spriteName)
    {
        currentCard = newCard; // Set new card

        if (HasBeenSeen(spriteName) == false)
        {
            currentCard = QuestionMarkFish;
        }
        
        timesCaught.text = CaughtCount[spriteName].ToString();
        
        displayFishName.text = currentCard.DisplayName;
        description.text = currentCard.Description;
        displayImage.sprite = currentCard.RealWorldPicure;
        
        // Change the Background to be a light brown range
        cardBgColor.color = new Color( (230 + (Random.Range(-1f, 1f) * 10) ) / 255,
            (210 + (Random.Range(-1f, 1f) * 15) ) / 255,
            (120 + (Random.Range(-1f, 1f) * 40)) / 255 , 1);
    }
    
    public bool HasBeenSeen(string sn)
    {
        return CaughtCount[sn] > 0; 
    }

    public void Mindwipe()
    {
        fishSeen.ClearCaughtMemories();
    }
    
    
}

