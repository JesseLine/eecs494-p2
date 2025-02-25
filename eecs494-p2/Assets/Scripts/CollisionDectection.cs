using UnityEngine;

public class CollisionDectection : MonoBehaviour
{
    public float invincibilityTime = 3f;

    private float time = 3f;

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
        if(other.gameObject.tag == "Enemy")
        {
            if(time >= invincibilityTime)
            {
                GetComponent<HasHealth>().TakeDamage(1);
                time = 0;
            }
            
        }

        if(other.tag == "Battery")
        {
            Debug.Log("Battery Collected!");
            EventBus.Publish<BatteryPickUpEvent>(new BatteryPickUpEvent());
            Destroy(other.gameObject);
        }
    }

}

public class BatteryPickUpEvent
{

}
