using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCollider : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sadness") || collision.gameObject.CompareTag("nonHazard"))
        {
            Destroy(collision.gameObject);
        }
    }

}
