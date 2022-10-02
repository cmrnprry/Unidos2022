using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;

    public static AudioController instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetAudio(int index)
    {
        source.DOFade(0, 0.5f);
        source.clip = clips[index];
        source.DOFade(1, 0.5f);
    }
}
