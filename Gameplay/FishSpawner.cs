using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace greg
{
public class FishSpawner : MonoBehaviour
{
    [SerializeField] private int FishPerWave = 6;
    [SerializeField] private float timeBewteenWaves = 10.0f;
    [SerializeField] private float timeBewteenWavesDelta = 5.0f;

    [SerializeField] private Logger l;
    
    [SerializeField] private FishStats[] allFishTypes;
    [SerializeField] private Transform[] spawnBoundsTransforms = new Transform[2];
    private readonly Vector3[] spawnBounds = new Vector3[2];
    
    private void Start()
    {
        spawnBounds[0] = spawnBoundsTransforms[0].position;
        spawnBounds[1] = spawnBoundsTransforms[1].position;

        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(startWave());
    }
    
    // should keep running until the timer is done 
    private IEnumerator startWave()
    {
        
            for (int i = 0; i < FishPerWave; i++)
            {
                spawnFish(whereToSpawn());
                yield return new WaitForSeconds(Random.value * 1.0f);
            }

            yield return new WaitForSeconds(timeBewteenWaves + (Random.value * timeBewteenWavesDelta));

            if (Timer.Instance.isTimerDone() == false)
            {
                StartCoroutine(startWave());
            }
            else
            {
                l.Log("Timer Done, no more fish");
            }
        
    }
    
    public IEnumerator SpawnABunchOfFish(int fishCount) //spawns in the center of the screen
    {
        Vector3 initPoint = new Vector3(0,0,Random.Range(spawnBounds[0].z, spawnBounds[1].z));
        
        for (int i = 0; i < fishCount; i++)
        {
            spawnFish(initPoint + ( Random.onUnitSphere * 2.0f)); // change this to increase the range of spawn
            yield return new WaitForSeconds(0.5f + (Random.value * 0.25f));
        }
    }
    
    private void spawnFish(Vector3 spawnPoint) // assigns fish with all the Fish Data it needs
    {
        GameObject g = ObjectPool.instance.getFish();
        if (g.IsUnityNull()) // faster than just null comparison
        {
            l.Log("No More Fish");
            return;
        }

        FishStats theseStats = selectFish();
        Debug.Log(spawnPoint);
        g.transform.position = spawnPoint;
        g.GetComponent<Fish>().setStats(theseStats);
        g.name = "Albert the Fish " + (Convert.ToInt16(Random.value * 1000));
        g.tag = "Fish";

        g.SetActive(true);
        g.transform.rotation = Quaternion.identity;
        
    }

    private FishStats selectFish()  // picks a fish to spawn, from the weighted sum
    {
        float rng = Random.value;
        rng *= 100;
        
        // sum the percentages of the fish stats until it reaches the rng
        for (int i = 0; i < allFishTypes.Length ; i++)
        {
            float sum = 0.00f;
            for (int j = 0; j <= i; j++) // won't add the 0th chance 
            {
                sum += allFishTypes[j].chance;
                if (rng < sum)
                {
                    return allFishTypes[i];
                }
            }
        }

        return allFishTypes[0]; // worst case return the most popular

    }

    private Vector3 whereToSpawn() // picks what side and height to spawn at
    {
        float rng = Random.value; // what side to spawn
        print(rng);
        Vector3 spawnVec = new Vector3(0, Random.Range(spawnBounds[0].y, spawnBounds[1].y),
            Random.Range(spawnBounds[0].z, spawnBounds[1].z));
        if (rng < 0.5)
        {
            spawnVec.x = spawnBounds[0].x;
        }
        else
        {
            spawnVec.x = spawnBounds[1].x;
        }
        return spawnVec;
        
    }
}
    
}
