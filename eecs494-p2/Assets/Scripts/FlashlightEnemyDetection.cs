using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightEnemyDetection : MonoBehaviour
{
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;

    public float LightRange = 10f;
    public float LightAngle = 45;

    public float damageRate = 1f;

    private float enemyEyeColor = 0x00680A;
    private Light pointLight;
    private float time = 0f;

    private bool gamePause = false;

    LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground","Enemy");
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePause) return;

        time += Time.deltaTime;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            //Debug.Log(enemy.name + " " + enemyInLightAngle(enemy) + " " + enemyNotHidden(enemy) + " " + enemyInRange(enemy));
            if(enemyInRange(enemy) && enemyNotHidden(enemy) && enemyInLightAngle(enemy) && GetComponent<Flashlight>().isOn())
            {
                enemy.GetComponentInParent<GhostMovement>().ReduceSpeed();

                if((time >= damageRate))
                {
                    //deal damage
                    //Debug.Log("deal damage");
                    enemy.GetComponent<HasHealth>().TakeDamage(GetComponent<DoesDamage>().damage);
                    Debug.Log(enemy.GetComponent<HasHealth>().health);

                    //change enemy eyes color
                    //Debug.Log("EYE CHANGE COLOR");
                    EventBus.Publish<DamageDealtEvent>(new DamageDealtEvent(enemy));
                    //enemy.GetComponentInChildren<Light>().color = Color.red;

                }
                
            }
            else
            {
                //Debug.Log("Enemy NOT in light");
                enemy.GetComponentInParent<GhostMovement>().NormalSpeed();
                //go back to normal eye color
                //enemy.GetComponentInChildren<Light>().color = Color.green;
            }
        }
        if(time >= damageRate)
        {
            time = 0;
        }

    }

    bool enemyInLightAngle(GameObject enemy)
    {
        Vector3 side1 = enemy.transform.position - transform.position;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);
        if(angle < LightAngle && angle > -1 * LightAngle)
        {
            //Debug.Log("Enemy in angle");
            return true;
        }
        else
        {
            //Debug.Log("Enemy not in angle");
            return false;
        }
    }
    
    bool enemyInRange(GameObject enemy)
    {
        if (Vector3.Distance(transform.position, enemy.transform.position) < LightRange)
        {
            //in range of flashlight
            //Debug.Log("in range");
            return true;
            
        }
        //Debug.Log("not in range");
        return false;
    }

    bool enemyNotHidden(GameObject enemy)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (enemy.transform.position - transform.position), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.blue, 30f);
            if(hit.transform == enemy.transform)
            {
                //Debug.Log("enemy not hidden");
                //Debug.Log(hit.collider.name);
                return true;
            }
            else
            {
                //Debug.Log("enemy hidden");
                //Debug.Log(hit.collider.name);
                return false;
            }
        }
        else
        {
            //Debug.Log("enemy !!!! hidden");
            return false;
        }
    }
    void _OnPause(PauseEvent e)
    {
        gamePause = true;
    }

    void _OnUnPause(UnPauseEvent e)
    {
        gamePause = false;
    }
}

public class DamageDealtEvent
{
    public GameObject go;
    public DamageDealtEvent(GameObject _go) { go = _go; }
}
