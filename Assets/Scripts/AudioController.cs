using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class AudioController : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource audioSource;//ff
    private AudioSource[] sources;//ff
    public AudioMixer mixer;
    private float bgm = 1;

    public static AudioController instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        sources = GameObject.Find("MusicSources").GetComponentsInChildren<AudioSource>();//ff
    }

    public void SetSFXVolume(float vol)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(vol) * 20);
    }

    public void SetBGMVolume(float vol)
    {
        bgm = vol;
        mixer.SetFloat("BGMVolume", Mathf.Log10(vol) * 20);
    }

    public void SetAudio(int index)//ff
    {
        //make this transisition smoother
        StartCoroutine(SmoothAudio(index));

    }//ff

    private IEnumerator SmoothAudio(int index)
    {
        foreach (AudioSource source in sources)
        {
            if (source.isPlaying)
            {
                source.DOFade(0, 0.5f);
                yield return new WaitForSeconds(1);
                source.Pause();
            }
            if (source.clip == musicClips[index])
            {
                audioSource = source;
            }
        }
        audioSource.Play();
        audioSource.DOFade(bgm, 1);
        yield return null;        
    }


    public void PlayUIAccept()
    {//ff
        GameObject.Find("UIAudio").GetComponent<AudioSource>().Play();//ff
    }

    public void PlayCameraPic()
    {//ff
        GameObject.Find("CameraSFX").GetComponent<AudioSource>().Play();//ff
    }

    public void PlaySizzle()
    {//ff
        GameObject.Find("SizzleSFX").GetComponent<AudioSource>().Play();//ff
    }
}
