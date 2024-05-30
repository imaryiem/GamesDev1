using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    private bool debugMode = true; // Allows pressing the 'H' key to simulate the player being hit

    [SerializeField] private TextMeshProUGUI healthLabel;

    [Header("Monitor health (do not edit)")]
    [SerializeField] private int health = 100;

    public void ResetHealth()
    {
        health = 100;
        healthLabel.color = Color.green;
        healthLabel.text = health + "%";
    }

    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;
        //Debug.Log("current health: " + health + "%");

        healthLabel.color = Color.green;
        if (health < 50)
        {
            healthLabel.color = Color.red;
        }

        if (health <= 0) {
            Debug.Log("Player has died, Game Over...!");
            Debug.Log("Press 'spacebar' to try again!");
        }

        healthLabel.text = health + "%";
    }

    private void SimulatePlayerHit()
    {
        DealDamage(20);
    }

    private void Update()
    {
        if (debugMode && Keyboard.current.hKey.wasPressedThisFrame)
        {
            SimulatePlayerHit();
        }

        if (health <= 0 && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Retrying...");
            ResetHealth();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // doesnt work well
            // thinking of just moving the character back instead
        }
    }
}
