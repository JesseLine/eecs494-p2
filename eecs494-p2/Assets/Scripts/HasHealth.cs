using UnityEngine;
using TMPro;
using System.Collections;

public class HasHealth : MonoBehaviour
{
    Subscription<NewWaveEvent> newWaveEventSubscription;
    Subscription<RestartGameEvent> restartGameEventSubscription;

    public int health = 10;
    public int maxHealth = 10;

    public static int currentWave = 0;

    public TextMeshProUGUI playerHealthText;

    public GameObject batteryPrefab;

    public Light damageIndicatorLight;

    private float healthIncreaseMultiplyer = 1.5f;

    private Vector3 enemyReducedScale;

    private bool gameOver = false;

    private void Start()
    {
        newWaveEventSubscription = EventBus.Subscribe<NewWaveEvent>(_OnNewWave);
        restartGameEventSubscription = EventBus.Subscribe<RestartGameEvent>(_OnRestart);

        health = ((int)(health * Mathf.Pow(healthIncreaseMultiplyer, currentWave)));
        if(health > 5)
        {
            health = 5;
        }
        if(tag == "Enemy")
        {
            enemyReducedScale = new Vector3(transform.parent.localScale.x, transform.parent.localScale.y / 2, transform.parent.localScale.z);
        }
        

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
                gameOver = true;
                EventBus.Publish<GameOverEvent>(new GameOverEvent());
            }
            else
            {
                if(transform.tag == "Enemy")
                {
                    if(batteryPrefab != null)
                    {
                        float rand = Random.Range(0, 100);
                        if(rand <= 15)
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
        if (gameOver) return;

        health -= damage;
        if (playerHealthText != null)
        {
            playerHealthText.text = "Health: " + health.ToString();
            Debug.Log("Player damaged");
            StartCoroutine(DamageLightFlash());
        }
        if(transform.gameObject.tag == "Enemy")
        {
            transform.parent.localScale = enemyReducedScale;
        }
    }

    IEnumerator DamageLightFlash()
    {
        Debug.Log("FLASH OUT");
        damageIndicatorLight.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        damageIndicatorLight.transform.gameObject.SetActive(false);
        
    }

    public void resetHealth()
    {
        health = maxHealth;
    }


    void _OnNewWave(NewWaveEvent e)
    {
        currentWave++;
    }

    void _OnRestart(RestartGameEvent e)
    {
        currentWave = 0;
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
