using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace greg
{

    public class SpawnExtraFish : PowerUp
    {
        [SerializeField] private FishSpawner spawner;
        [SerializeField] private int spawnCount = 5;
    
        // can i do a Setup method that grabs the Fish spawner from the hierarchy

        private void Start()
        {
            if (spawner == null)
                spawner = FindObjectOfType<FishSpawner>();
        }

        public override IEnumerator castPowerUp() // Calls the spawn random fish in the Fish Spawner
        {
            StartCoroutine(spawner.SpawnABunchOfFish(spawnCount));
        
            yield return null;
        }
    }
    
}

