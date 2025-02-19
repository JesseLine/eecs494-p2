using UnityEngine;
using TMPro;

public class HasHealth : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public TextMeshProUGUI playerHealthText;

    private void Start()
    {
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
}

public class DeathEvent
{
    public string tag;
    public DeathEvent(string _tag) { tag = _tag; }

}

public class GameOverEvent
{

}
