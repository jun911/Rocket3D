using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] int Fuel = 60;
    [SerializeField] float velocity;
    [SerializeField] bool isXRotateLanded;
    [SerializeField] bool isYRotateLanded;
    [SerializeField] AudioClip sfxMainEngine;
    [SerializeField] AudioClip sfxCrashExplotion;
    [SerializeField] AudioClip sfxSuccess;
    [SerializeField] ParticleSystem fxBooster;
    [SerializeField] ParticleSystem fxExplotion;
    [SerializeField] ParticleSystem fxSuccess;
    [SerializeField] ParticleSystem fxLeftFire;
    [SerializeField] ParticleSystem fxRightFire;

    Rigidbody rb;
    AudioSource audioSource;

    public State state;

    public enum State
    {
        IDLE,
        FLY,
        LANDED
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        state = State.IDLE;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        velocity = rb.velocity.magnitude;
        isXRotateLanded = (transform.localRotation.eulerAngles.x > 350 || transform.localRotation.eulerAngles.x < 10);
        isYRotateLanded = (transform.localRotation.eulerAngles.y > 350 || transform.localRotation.eulerAngles.y < 10);

        if (velocity > 0.01)
        {
            state = State.FLY;
        }
        else if(isXRotateLanded && isYRotateLanded)
        {
            state = State.LANDED;
        }
        else
        {
            state = State.IDLE;
        }
    }

    public void PlaySoundMainEngine()
    {
        PlaySound(sfxMainEngine);
    }

    public void PlaySoundCrashExpotion()
    {
        SetVolumn(100f);
        StopSound();
        PlaySound(sfxCrashExplotion);
    }

    public void PlaySoundSuccess()
    {
        SetVolumn(100f);
        StopSound();
        PlaySound(sfxSuccess);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void SetVolumn(float volumn)
    {
        audioSource.volume = volumn;
    }

    public void StopSound()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayEffectThrust()
    {
        fxBooster.Play();
    }
    
    public void StopEffectThrust()
    {
        fxBooster.Stop();
    }

    public void PlayEffectExplotion()
    {
        fxExplotion.Play();
    }

    public void PlayEffectSuccess()
    {
        fxSuccess.Play();
    }

    public void PlayEffectLeftFire()
    {
        fxLeftFire.Play();
    }

    public void PlayEffectRightFire()
    {
        fxRightFire.Play();
    }

    public void StopEffectLeftFire()
    {
        fxLeftFire.Stop();
    }

    public void StopEffectRightFire()
    {
        fxRightFire.Stop();
    }
}
