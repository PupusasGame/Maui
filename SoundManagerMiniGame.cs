using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerMiniGame : MonoBehaviour
{
    public AudioClip[] audioClips;
    public int actualClip; 
    // Start is called before the first frame update
    void Start()
    {
        SetAudioClip();
    }

    public void PlayAudioClip()
    {
        GetComponent<AudioSource>().clip = audioClips[actualClip];
        GetComponent<AudioSource>().Play();
    }

    public void SetAudioClip()
    {
        actualClip = GetComponent<MiniGameManager>().actualWord;
    }
}
