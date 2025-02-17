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
            }
            else
            {
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
