using System;
using System.Collections.Generic;
using UnityEngine;
 
namespace greg
{
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private int maxAmountInPool = 25;

    [SerializeField] private GameObject FishPrefab;
    private List<GameObject> PooledObjects = new List<GameObject>();

    [SerializeField] private Logger l;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < maxAmountInPool; i++)
        {
            GameObject g = makeFish();
            PooledObjects.Add(g);
        }
    }

    public GameObject getFish() // finds first available fish in the list
    {
        
        foreach (GameObject obj in PooledObjects)
        {
            if (obj.activeInHierarchy == false) 
            {
                l.Log("Found Available Fish");
                return obj;
            }
        }

        l.Log("Couldnt find or make space, returning null");
        return null;
    }

    private GameObject makeFish()
    {
        GameObject g = Instantiate(FishPrefab);
        g.SetActive(false);
        g.name = "Albert the Fish Unused";
        g.tag = "Fish";
        return g;
    }

    public void KillAllFish()
    {
        foreach (GameObject obj in PooledObjects)
        {
            if (obj.activeInHierarchy == true) 
            {
               obj.GetComponent<Fish>().killFish();
            }
        }
    }

    public bool isFishOnScreen()
    {
        foreach (GameObject obj in PooledObjects)
        {
            if (obj.activeInHierarchy == true)
            {
                return true;
            }
        }
        return false;
    }

}
    
}
