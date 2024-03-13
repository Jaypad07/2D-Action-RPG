using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float playRate;
    private float lastPlayTime;

    private void Update()
    {
        if (rig.velocity.magnitude > 0 && Time.time - lastPlayTime > playRate)
        {
            Play();
        }
    }

    void Play()
    {
        lastPlayTime = Time.time;

        AudioClip clipToPlay = footstepSFX[Random.Range(0, footstepSFX.Length)];
        
        _audioSource.PlayOneShot(clipToPlay);
    }
}
