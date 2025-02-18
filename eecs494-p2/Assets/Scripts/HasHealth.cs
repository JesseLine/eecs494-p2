using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;


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
                Destroy(transform.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
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
