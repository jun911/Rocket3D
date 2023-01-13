using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField][Range(0, 1)] private float movementFactor;
    [SerializeField] private float period = 15f;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;
        float rowSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rowSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}