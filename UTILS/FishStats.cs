using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish", order = 0)]
public class FishStats : ScriptableObject
{
    public string Letters = ""; //just list them all 
    public Sprite[] Sprites = new Sprite[2];

    public int Points = 0;
    public int rarity = 1;
    public float chance;

    private void Awake()
    {
        Letters = String.Concat(Letters.Where(c => !Char.IsWhiteSpace(c))); // just in case
    }
}