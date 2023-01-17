using System.Net.Sockets;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private int Fuel = 60;
    [SerializeField] private float velocity;
    [SerializeField] private AudioClip sfxMainEngine;
    [SerializeField] private AudioClip sfxCrashExplotion;
    [SerializeField] private AudioClip sfxSuccess;
    [SerializeField] private AudioClip sfxFuel;
    [SerializeField] private ParticleSystem fxBooster;
    [SerializeField] private ParticleSystem fxExplotion;
    [SerializeField] private ParticleSystem fxSuccess;

    private Rigidbody rb;
    private AudioSource audioSource;

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
        if (IsFlying())
        {
            state = State.FLY;
        }
        else if (IsStandUpright())
        {
            state = State.LANDED;
        }
        else
        {
            state = State.IDLE;
        }
    }

    private bool IsStandUpright()
    {
        bool isXRotateLanded = (transform.localRotation.eulerAngles.x > 350 || transform.localRotation.eulerAngles.x < 10);
        bool isYRotateLanded = (transform.localRotation.eulerAngles.y > 350 || transform.localRotation.eulerAngles.y < 10);

        return (isXRotateLanded && isYRotateLanded);
    }

    private bool IsFlying()
    {
        velocity = rb.velocity.magnitude;

        return velocity > 0.01;
    }

    public void StartCrashSequence()
    {
        StartSoundCrashExplotion();
        StartEffectExplotion();
        HideRocketMesh();
    }

    private void HideRocketMesh()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void StartSucessSequence()
    {
        StartSoundSuccess();
        StartEffectSuccess();
    }

    #region SFX

    public void StartSoundMainEngine()
    {
        SetVolumn(10f);
        PlaySound(sfxMainEngine);
    }

    private void StartSoundCrashExplotion()
    {
        SetVolumn(100f);
        StopSound();
        PlaySound(sfxCrashExplotion);
    }

    private void StartSoundSuccess()
    {
        SetVolumn(100f);
        StopSound();
        PlaySound(sfxSuccess);
    }

    public void StartSoundFuel()
    {
        SetVolumn(50f);
        StopSound();
        PlaySound(sfxFuel);
    }


    private void PlaySound(AudioClip sfx)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfx);
        }
    }

    private void SetVolumn(float volumn)
    {
        audioSource.volume = volumn;
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    #endregion SFX


    #region FX

    public void StartEffectThrust()
    {
        if (!fxBooster.isPlaying)
        {
            fxBooster.Play();
        }
    }

    public void StopEffectThrust()
    {
        fxBooster.Stop();
    }

    private void StartEffectExplotion()
    {
        PlayEffect(fxExplotion);
    }

    private void StartEffectSuccess()
    {
        PlayEffect(fxSuccess);
    }

    private void PlayEffect(ParticleSystem fx)
    {
        fx.Play();
    }

    #endregion FX
}