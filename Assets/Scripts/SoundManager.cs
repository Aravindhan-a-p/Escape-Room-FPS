using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingSound;
    public AudioSource RelaodSound;
    public AudioSource EmptyMagazineSound;

    public AudioSource playerChannel;
    public AudioSource playerHurt;
    public AudioSource playerDeath;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
