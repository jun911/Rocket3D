using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 20f;

    Rigidbody rigidbody;
    Rocket rocket;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rocket = GameManager.instance.rocket;
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRotateRight();
        }
    }

    private void StartRotateRight()
    {
        DoRotation(-Vector3.forward);
    }

    private void StartRotateLeft()
    {
        DoRotation(Vector3.forward);
    }

    private void StopThrust()
    {
        rocket.StopEffectThrust();
        rocket.StopSound();
    }

    private void StartThrust()
    {
        rocket.StartEffectThrust();
        rocket.StartSoundMainEngine();
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    private void DoRotation(Vector3 vec)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(vec * rotateThrust * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}