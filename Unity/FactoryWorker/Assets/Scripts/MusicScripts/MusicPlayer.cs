using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    //Singleton
    static bool AudioPlaying = false;

    private static AudioSource audioSource;

    private AudioClip pipeGameMusic, archeryGameMusic, snowGameMusic, painterGameMusic, levelHubMusic;

    void Awake()
    {
        //Get the audio source component
        audioSource = GetComponent<AudioSource>();

        //Dont destroy this object
        DontDestroyOnLoad(gameObject);

    }

    public static void StartMusic(AudioClip audioClip)
    {
        if (!AudioPlaying)
        {
            if (audioSource)
            {
                //Load the specified clip
                audioSource.clip = audioClip;

                //Set the volume
                audioSource.volume = 0.05f;

                //Play the audio
                audioSource.Play();

                //Set audio playing to true
                AudioPlaying = true;
            }
        }
    }

    public static void StopMusic() {
        if (audioSource)
        {
            //Stop the audio
            audioSource.Stop();

            //Set audio playing to false;
            AudioPlaying = false;
        }
    }
}
