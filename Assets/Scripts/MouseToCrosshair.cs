using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script makes the object follow the mouse cursor and rotate towards it.
/// Written with input from ChatGPT using the following prompt:
/// "Write a script that makes an object follow the mouse cursor and rotate towards it. The object should orbit around a center object."
/// </summary>
public class MouseToCrosshair : MonoBehaviour
{
    public Transform CrosshairOrbitObject; // The object to orbit around

    private float orbitRadius; // Radius of the orbit

    void Start()
    {
        // Calculate the radius of the orbit (sum of the radius of the center object and the radius of the orbiting object)
        orbitRadius = (CrosshairOrbitObject.localScale.x + transform.localScale.x) ; // Summing up the radius of both objects
    }

    void Update()
    {
        // Calculate the direction from the center of the screen to the mouse position
        Vector2 direction = Mouse.current.position.ReadValue() - new Vector2(Screen.width / 2f, Screen.height / 2f); // Center of the screen

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Calculate the new position of the object
        float x = CrosshairOrbitObject.position.x + Mathf.Cos(angle) * orbitRadius;
        float y = CrosshairOrbitObject.position.y + Mathf.Sin(angle) * orbitRadius;

        // Update the position of the object
        transform.position = new Vector2(x, y);

        // Apply the rotation
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90, Vector3.forward);
    }
}