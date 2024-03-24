using UnityEngine;

public class Recharge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShootController shootController = other.gameObject.GetComponent<ShootController>();
            if (shootController != null)
            {
                shootController.ResetCooldown();
            }

            // make the powerup inactive
            gameObject.SetActive(false);
        }
    }
}