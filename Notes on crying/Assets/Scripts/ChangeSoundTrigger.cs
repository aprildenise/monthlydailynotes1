using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundTrigger : MonoBehaviour
{

    public AudioManager audio;


    public void SetSoundChange()
    {
        StartCoroutine(audio.ChangeToDoubleTime());
    }


}
