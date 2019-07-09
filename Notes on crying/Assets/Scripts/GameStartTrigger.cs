using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{


    public MainManager game;
    public bool readyToSpawn = false;

    public void TriggerSpawns()
    {
        readyToSpawn = true;
        GetComponent<Animator>().enabled = false;
        //game.SetStartTimer();
        StartCoroutine(game.SpawnHazards());
    }
}
