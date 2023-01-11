using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 20f;

    Rigidbody rigidbody;
    AudioSource audioSource;

    Rocket rocket;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = rigidbody.GetComponent<AudioSource>();
    }

    private void Start()
    {
        rocket = GameManager.instance.rocket;
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        //rocket.StopEffectLeftFire();
        //rocket.StopEffectRightFire();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rocket.PlayEffectRightFire();
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rocket.PlayEffectLeftFire();
            ApplyRotation(-Vector3.forward);
        }
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocket.PlayEffectThrust();
            rocket.PlaySoundMainEngine();
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            rocket.StopEffectThrust();
            rocket.StopSound();
        }
    }

    private void ApplyRotation(Vector3 vec)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(vec * rotateThrust * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}