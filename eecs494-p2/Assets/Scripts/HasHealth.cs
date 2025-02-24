using UnityEngine;
using TMPro;

public class HasHealth : MonoBehaviour
{
    Subscription<NewWaveEvent> newWaveEventSubscription;

    public int health = 10;
    public int maxHealth = 10;

    public static int currentWave = 0;

    public TextMeshProUGUI playerHealthText;

    public GameObject batteryPrefab;

    private float healthIncreaseMultiplyer = 1.5f;

    private void Start()
    {
        newWaveEventSubscription = EventBus.Subscribe<NewWaveEvent>(_OnNewWave);

        health = (int)(health * Mathf.Pow(healthIncreaseMultiplyer, currentWave));
        
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + health.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if(transform.tag == "Player")
            {
                //gameOver
                EventBus.Publish<GameOverEvent>(new GameOverEvent());
            }
            else
            {
                if(transform.tag == "Enemy")
                {
                    if(batteryPrefab != null)
                    {
                        float rand = Random.Range(0, 10);
                        if(rand <= 1)
                        {
                            Debug.Log("battery dropped");
                            Instantiate(batteryPrefab, transform.position, batteryPrefab.transform.rotation);
                        }
                    }
                    EventBus.Publish<DeathEvent>(new DeathEvent(transform.tag));
                }
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + health.ToString();
        }
    }

    public void resetHealth()
    {
        health = maxHealth;
    }


    void _OnNewWave(NewWaveEvent e)
    {
        currentWave++;
    }
}

public class DeathEvent
{
    public string tag;
    public DeathEvent(string _tag) { tag = _tag; }

}

public class GameOverEvent
{

}
