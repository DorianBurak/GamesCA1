using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    public AudioClip backGround;
    public AudioClip death;
    public AudioClip finish;
    public AudioClip gold;
    public AudioClip heart;
    public AudioClip hit;
    public AudioClip jump;
    void Start()
    {
        music.clip = backGround;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

}
