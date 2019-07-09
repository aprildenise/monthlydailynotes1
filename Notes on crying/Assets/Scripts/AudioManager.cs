using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;


    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("music");   
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                Debug.Log("playing");
            }
        }
    }


    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                Debug.Log("playing");
            }
        }
    }



    public void PlayRandPitched(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                float newPitch = Random.Range(.1f, 3f);
                s.source.pitch = newPitch;
                s.source.Play();
                Debug.Log("playing");
            }
        }
    }


    public IEnumerator ChangeToSingleTime()
    {
        Sound sound = sounds[0];
        float pitch = sound.source.pitch;
        //sound.source.pitch = 1f;
        //while (pitch > 1)
        //{
        //    pitch -= .01f;
        //    sound.source.pitch = pitch;
        //    yield return new WaitForSeconds(0.01f);
        //}

        yield break;
    }


    public IEnumerator ChangeToDoubleTime()
    {
        Sound sound = sounds[0];
        float pitch = sound.source.pitch;
        //sound.source.pitch = 1.5f;
        //while (pitch < 1.5f)
        //{
        //    pitch += .01f;
        //    sound.source.pitch = pitch;
        //    yield return new WaitForSeconds(0.01f);
        //}

        yield break;

    }
}
