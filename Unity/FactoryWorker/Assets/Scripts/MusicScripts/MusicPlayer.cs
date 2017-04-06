using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    //Singleton
    static bool AudioPlaying = false;

    private static AudioSource audio;

    private AudioClip pipeGameMusic, archeryGameMusic, snowGameMusic, painterGameMusic, levelHubMusic;

    void Awake()
    {
        //load audio clips
        pipeGameMusic = Resources.Load<AudioClip>("snowGameMusic");
        snowGameMusic = Resources.Load<AudioClip>("snowGameMusic");
        painterGameMusic = Resources.Load<AudioClip>("painterGameMusic");
        archeryGameMusic = Resources.Load<AudioClip>("levelHubMusic");
        levelHubMusic = Resources.Load<AudioClip>("levelHubMusic");

        //Get the audio source component
        audio = GetComponent<AudioSource>();

        //Dont destroy this object
        DontDestroyOnLoad(gameObject);

    }

    public static void StartMusic(AudioClip audioClip)
    {
        if (!AudioPlaying)
        {
            if (audio)
            {
                //Load the specified clip
                audio.clip = audioClip;
                //Play the audio
                audio.Play();

                //Set audio playing to true
                AudioPlaying = true;
            }
        }
    }

    public static void StopMusic() {

        //Stop the audio
        audio.Stop();

        //Set audio playing to false;
        AudioPlaying = false;
    }
}
