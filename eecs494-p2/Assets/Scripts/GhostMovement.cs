using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 2.5f;
    public float reducedSpeed = 1f;

    public float currentSpeed;
    private NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    private void LateUpdate()
    {
        navMeshAgent.speed = currentSpeed;
        //Debug.Log(currentSpeed.ToString());
    }

    public void ReduceSpeed()
    {
        //Debug.Log("speed reduced");
        currentSpeed = reducedSpeed;
    }
    public void NormalSpeed()
    {
        if (navMeshAgent != null)
        {
            currentSpeed = speed;
        }

    }
}
