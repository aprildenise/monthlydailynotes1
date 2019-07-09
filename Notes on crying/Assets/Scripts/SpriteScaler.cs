using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScaler : MonoBehaviour
{


    public int defaultWidth = 1024;
    public int defaultHeight = 720;

    private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(defaultWidth / Screen.width, defaultHeight / Screen.height, 1f);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.transform.localScale = Vector3.Scale(sprite.transform.localScale, scale);
        sprite.transform.position = Vector3.Scale(sprite.transform.position, scale); //not sure that you need this
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
