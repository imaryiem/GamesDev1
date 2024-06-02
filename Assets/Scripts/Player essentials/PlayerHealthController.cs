using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    private bool debugMode = false; // Allows pressing the 'H' or 'G' keys to simulate the player healing or taking damage.

    [SerializeField] private TextMeshProUGUI healthLabel;

    [Header("Monitor health (do not edit)")]
    [SerializeField] private int health = 100;

    public int Health()
    {
        return health;
    }

    public void ResetHealth()
    {
        health = 100;
        healthLabel.color = Color.green;
        healthLabel.text = health + "%";
    }

    public void TakeDamage(int dmgAmount)
    {
        //health -= RandomHealthAmountGenerator(7, 22);
        health -= dmgAmount;
        //Debug.Log("current health: " + health + "%");

        RefreshLabelColor();

        if (health <= 0) {
            Debug.Log("Player has died, Game Over...!");
            Debug.Log("Press 'spacebar' to try again!");
        }

        healthLabel.text = health + "%";
    }

    public void Heal()
    {
        switch (health)
        {
            case > 100:
                health += 0;
                break;
            case 100:
                health += 25; //so new max would be 125 health points
                break;
            default:
                health += RandomHealthAmountGenerator(10, 18);
                if (health > 100) health = 100;
                break;
        }

        RefreshLabelColor();
        healthLabel.text = health + "%";
    }

    private void RefreshLabelColor()
    {
        switch (health)
        {
            case < 50:
                healthLabel.color = Color.red;
                break;
            case <= 100:
                healthLabel.color = Color.green;
                break;
            default:
                healthLabel.color = Color.cyan;
                break;
        }
    }

    private int RandomHealthAmountGenerator(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTesting")
    //    {
    //        TakeDamage(RandomHealthAmountGenerator(7, 22));
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyTesting")
        {
            TakeDamage(RandomHealthAmountGenerator(7, 22));
        }
    }

    private void Update()
    {
        // Simulate damage being taken (for debugging)
        if (debugMode && Keyboard.current.hKey.wasPressedThisFrame)
        {
            // 'H' key for simulating for healing
            Heal();
        }
        else if (debugMode && Keyboard.current.gKey.wasPressedThisFrame)
        {
            // 'G' key for simulating damage taken
            TakeDamage(RandomHealthAmountGenerator(7, 22));
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
