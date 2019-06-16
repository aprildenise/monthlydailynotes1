using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{


    // References
    public UIManager uimanager;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public GameStartTrigger gameStartTrigger;
    public GameObject dropPrefab;
    public GameObject nonhazardDrop;

    private int spawnProbability;
    private int hazardTotal;
    private float spawnInterval;
    private bool alreadyStarted;

    




    // Start is called before the first frame update
    void Awake()
    {
        uimanager.TurnOffGame();
        uimanager.TurnOnStart();
        spawnProbability = 3;
        hazardTotal = 50;
        spawnInterval = 2f;
    }


    public void StartGame()
    {
        if (!alreadyStarted)
        {
            StartCoroutine(uimanager.FadeOutStartMenu());
            alreadyStarted = true;
        }
        
    }


    public IEnumerator GameOver()
    {
        int hazardCount = 0;
        
        spawnInterval = .5f;
        while (hazardCount <= 100)
        {
            // Choose if we want to spawn a hazard
            int possibility = Random.Range(0, 11);
            if (possibility <= spawnProbability)
            {
                // chosen to spawn the hazard
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.y);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
                hazardCount++;

                yield return new WaitForSeconds(spawnInterval);
            }

        }



    }


    public void SetStartTimer()
    {
        uimanager.timestarts = true;
    }


    public IEnumerator SpawnHazards()
    {
        int hazardCount = 0;
        while (hazardCount <= hazardTotal)
        {
            // Choose if we want to spawn a hazard
            int possibility = Random.Range(0, 11);
            if (possibility <= spawnProbability)
            {
                // chosen to spawn the hazard
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.y);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
                hazardCount++;

                yield return new WaitForSeconds(spawnInterval);
            }

            // Change the spawn interval based on how many hazards were already spawned
            if (hazardCount == 10)
            {
                spawnInterval = 1.5f;
            }


            if (hazardCount == 30)
            {
                spawnInterval = 1f;
            }

        }

        
    }
}
