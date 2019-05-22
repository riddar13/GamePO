using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour
{
    public static SoundEffectsHelper Instance;

    public AudioClip explosionSound;
    public AudioClip playerShotSound;
    public AudioClip enemyShotSound;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляров SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }
    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }
    public void MakeEnemyShotSound()
    {
        MakeSound(enemyShotSound);
    }

    //играть данный звук
    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
