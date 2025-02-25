using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDectection : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;

    public float invincibilityTime = 3f;

    private float time = 3f;
    private bool gameOver = false;

    private void Start()
    {
        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
   /* private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Enemy")
        {
            Debug.Log("hit!");
            if (hit.transform.GetComponentInParent<DoesDamage>() != null && time >= invincibilityTime)
            {
                Debug.Log("Player takes damage");
                transform.GetComponent<HasHealth>().TakeDamage(hit.transform.GetComponentInParent<DoesDamage>().damage);
                time = 0;
            }
        }
    } */
    

    private void OnTriggerEnter(Collider other)
    {
        if (gameOver) return;

        if(other.gameObject.tag == "Enemy")
        {
            if(time >= invincibilityTime)
            {
                GetComponent<HasHealth>().TakeDamage(1);
                //Debug.Log("Knockback!");
                time = 0;
            }
            StartCoroutine(Knockback(other.gameObject));

        }

        if(other.tag == "Battery")
        {
            Debug.Log("Battery Collected!");
            EventBus.Publish<BatteryPickUpEvent>(new BatteryPickUpEvent());
            Destroy(other.gameObject);
        }
    }

    IEnumerator Knockback(GameObject enemy)
    {
        Vector3 knockbackDirection = (enemy.gameObject.transform.position - transform.position).normalized;
        float knockbackForce = 5f;
        enemy.transform.parent.transform.gameObject.GetComponentInChildren<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        enemy.transform.parent.transform.gameObject.GetComponentInChildren<Rigidbody>().AddForce(-knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    void _OnGameOver(GameOverEvent e)
    {
        gameOver = true;
    }

}

public class BatteryPickUpEvent
{

}
