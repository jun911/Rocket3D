using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 15f;

    Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;
        float rowSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rowSinWave + 1f) / 2f;
        
        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
