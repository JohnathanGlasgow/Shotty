using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script makes the crosshair follow the mouse position.
/// Source: https://www.youtube.com/watch?v=-bkmPm_Besk
/// </summary>
public class CrosshairMovement : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}