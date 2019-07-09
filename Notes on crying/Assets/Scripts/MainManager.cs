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
    public AudioManager audio;

    public GameObject anxiety;
    public GameObject burnout;
    public GameObject depression;
    public GameObject expectations;
    public GameObject loneliness;
    public GameObject unemployment;
    

    public GameObject nonhazardDrop;
    public PlayerController playerController;

    private int spawnProbability;
    private int hazardTotal;
    private float spawnInterval;
    private bool alreadyStarted;
    private bool alreadyQuitted;
    private bool gameOver;
    private bool interactingWithPhone;
    private bool finishedPhone;

    public bool onPhoneEnd;

    




    // Start is called before the first frame update
    void Awake()
    {
        uimanager.TurnOffGame();
        uimanager.TurnOnStart();
        spawnProbability = 3;
        hazardTotal = 100;
        spawnInterval = 1f;
        gameOver = false;
        interactingWithPhone = false;
    }


    public void StartGame()
    {
        if (!alreadyStarted)
        {
            StartCoroutine(uimanager.FadeOutStartMenu());
            alreadyStarted = true;
        }
        
    }


    public void Quit()
    {
        if (!alreadyQuitted)
        {
            StartCoroutine(uimanager.PlayQuitScreen());
            alreadyQuitted = true;
        }
    }


    public IEnumerator GameOver()
    {

        int hazardCount = 0;
        gameOver = true;
        TurnOffPhone();

        while (hazardCount <= 100)
        {
            // chosen to spawn the hazard
            // choose where to spawn
            float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
            Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

            // spawn it!
            Instantiate(nonhazardDrop, spawnPosition, Quaternion.identity);
            hazardCount++;

            yield return new WaitForSeconds(.1f);
        }

        StartCoroutine(audio.ChangeToSingleTime());

        StartCoroutine(uimanager.PlayCongratsScreen(true));


    }


    //public void SetStartTimer()
    //{
    //    uimanager.timestarts = true;
    //}


    public IEnumerator SpawnHazards()
    {
        int hazardCount = 0;
        while (hazardCount <= hazardTotal)
        {

            yield return new WaitUntil(() => interactingWithPhone == false);

            if (gameOver)
            {
                TurnOffPhone();
                yield break;
            }

            // Choose if we want to spawn a hazard
            int possibility = Random.Range(0, 11);
            if (possibility <= spawnProbability)
            {
                // chosen to spawn the hazard
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
                hazardCount++;

                yield return new WaitForSeconds(spawnInterval);
            }


            if (hazardCount == 10)
            {
                spawnInterval = .5f;
            }

            if (hazardCount == 20)
            {
                spawnProbability = 5;
                uimanager.PlayTextPrompt();
                playerController.canText = true;
            }

            if (hazardCount == 30)
            {
                spawnProbability = 10;
                spawnInterval = .1f;
            }

            if (hazardCount == 50)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(loneliness, spawnPosition, loneliness.transform.rotation);
            }

            if (hazardCount == 60)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(unemployment, spawnPosition, unemployment.transform.rotation);
            }

            if (hazardCount == 65)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(burnout, spawnPosition, burnout.transform.rotation);
            }

            if (hazardCount == 70)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(expectations, spawnPosition, expectations.transform.rotation);
            }

            if (hazardCount == 90)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(anxiety, spawnPosition, anxiety.transform.rotation);
            }

            if (hazardCount == 95)
            {
                // choose where to spawn
                float temp = Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
                Vector2 spawnPosition = new Vector2(temp, leftSpawnPoint.position.y);

                // spawn it!
                Instantiate(depression, spawnPosition, depression.transform.rotation);
            }

        }

        yield return new WaitForSeconds(3);

        if (gameOver)
        {
            TurnOffPhone();
            yield break;
        }

        TurnOffPhone();
        StartCoroutine(audio.ChangeToSingleTime());
        uimanager.HideTextPrompt();
        StartCoroutine(uimanager.PlayCongratsScreen(false));

    }


    public void InteractWithPhone(bool status)
    {
        interactingWithPhone = status;
    }

    public void SetCanText(bool status)
    {
        playerController.canText = status;
    }


    public void TurnOffPhone()
    {
        playerController.canText = false;
        uimanager.textPrompt.SetBool("showTextPrompt", false);
        uimanager.phonePrompt.SetBool("displayPhone", false);
        audio.Stop("phone");
    }


    public void PlayPhoneEnding()
    {
        
        onPhoneEnd = true;
        uimanager.phonePrompt.SetBool("displayPhone", false);
        audio.Stop("phone");
        //uimanager.HidePhone();
        StartCoroutine(uimanager.PlayCongratsScreen(false));
        StartCoroutine(audio.ChangeToSingleTime());

    }
}
