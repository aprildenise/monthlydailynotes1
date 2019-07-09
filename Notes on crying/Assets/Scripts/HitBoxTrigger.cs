using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{


    public PlayerController player;
    private bool hasAlreadySuffered;


    /// <summary>
    /// Check if the player's heart (heart object) has collided with something
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has encountered the urge to cry
        //Debug.Log("hit here");
        if (collision.gameObject.CompareTag("sadness") && !hasAlreadySuffered)
        {
            hasAlreadySuffered = true;
            player.PlayParticle();
            Destroy(collision.gameObject);
            player.LoseLife();
            //Debug.Log("player lost a life");
            StartCoroutine(StartIFrames());
        }
    }


    /// <summary>
    /// Play invisibility frames
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartIFrames()
    {
        // Play I frames so the player gets a break from suffering
        player.animator.SetBool("got hit", true);
        yield return new WaitForSeconds(3);
        player.animator.SetBool("got hit", false);
        //Debug.Log("done getting hit...");
        hasAlreadySuffered = false;
        //Debug.Log("player has healed...");
    }

}
