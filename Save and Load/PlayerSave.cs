 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerSave
{

    private static readonly List<string> fishSpriteNames  = new List<string>()
    {
        "AlligatorGar" , "BlueGill" , "BlueTang" , "Clownfish" , "FourEyeButterfish",
        "FrenchGrunt" , "GrayAngelfish" , "GrayTriggerfish" , "HarlequinFilefish",
        "KingMackeral" , "LaneSnapper" , "Lionfish" , "MidnightParrotfish",
        "Parrotfish" , "RedPorgy" , "RoyalGramma" , "ScissortailSergeant" , "Wahoo"
    };
    

    public static Dictionary<string, int>  LoadPlayerPrefs()
    {
        Dictionary<string, int> catchNumbers = new Dictionary<string, int>();
        
        foreach (var sn in fishSpriteNames)
        {
            int i = PlayerPrefs.GetInt(sn , 0);
            catchNumbers.Add(sn , i);
        }

        return catchNumbers;
    }

    public static void SavePlayerPrefs( Dictionary<string, int> newData)
    {
        foreach (var sn in fishSpriteNames)
        {
            PlayerPrefs.SetInt(sn , newData[sn] );
        }
        PlayerPrefs.Save();
    }
    
    public static void ResetPlayerPrefs()
    {
        foreach (var sn in fishSpriteNames)
        {
            PlayerPrefs.SetInt(sn , 0);
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("Encyclopedia_GChinnici");
    }
}
