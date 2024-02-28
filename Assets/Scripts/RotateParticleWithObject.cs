using UnityEngine;

public class RotateParticleWithObject : MonoBehaviour
{
    public Transform targetObject; // The object whose rotation you want to match
    private ParticleSystem particleSystem; // Reference to the Particle System
    public Vector3 rotationOffset; // Offset to apply to the rotation

    void Start()
    {
        // Get the Particle System component
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Check if the target object is not null and the Particle System is valid
        if (targetObject != null && particleSystem != null)
        {
            // Match the rotation of the Particle System with the rotation of the target object
            Quaternion targetRotation = targetObject.rotation * Quaternion.Euler(rotationOffset);
            transform.rotation = targetRotation;
        }
    }
}