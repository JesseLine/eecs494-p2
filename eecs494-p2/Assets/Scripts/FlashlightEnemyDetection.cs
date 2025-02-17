using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightEnemyDetection : MonoBehaviour
{
    public float LightRange = 10f;
    public float LightAngle = 45;

    LayerMask layerMask;

    private List<GameObject> ghostList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground","Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            if(enemyInRange(enemy) && enemyNotHidden(enemy) && enemyInLightAngle(enemy))
            {
                //deal damage
                Debug.Log("Enemy in flashlight cone");
            }
            else
            {
                Debug.Log("Enemy NOT in light");
            }
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
}
