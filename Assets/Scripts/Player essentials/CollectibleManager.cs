using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private PlayerHealthController healthController;

    private bool ignoreHealing = false;

    private void Awake()
    {
        healthController = FindObjectOfType<PlayerHealthController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "HealthCollectible":
                if (healthController.Health() > 100)
                {
                    ignoreHealing = true;
                }
                else
                {
                    ignoreHealing = false;
                }

                if (!ignoreHealing)
                {
                    healthController.Heal();
                    other.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}