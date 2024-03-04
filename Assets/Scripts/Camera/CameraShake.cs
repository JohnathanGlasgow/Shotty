using UnityEngine;
using DG.Tweening;

//Code from:
//https://www.youtube.com/watch?v=NKl27_jEpzA
//https://github.com/agoodboygames/Simple-Camera-Shake-in-Unity/blob/main/CameraShake.cs

//to call camera shake from another script use:
//CameraShake.Shake(duration, strength);

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    private void OnShake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }

    public static void Shake(float duration, float strength) => Instance.OnShake(duration, strength);
}