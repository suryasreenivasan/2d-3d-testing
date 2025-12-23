using UnityEngine;

public class moveobsatcle : MonoBehaviour
{
    public float speed = 2f; // Speed of the movement
    public float magnitude = 1f; // Magnitude of the movement
    private Vector3 startPosition; // Initial position of the object

    void Start()
    {
        startPosition = transform.position; // Store the initial position of the object
    }

    void Update()
    {
        // Calculate the vertical offset using a sinusoidal function based on time
        float yOffset = Mathf.Sin(Time.time * speed) * magnitude;

        // Update the position of the object
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        transform.rotation = Quaternion.identity;
    }
}