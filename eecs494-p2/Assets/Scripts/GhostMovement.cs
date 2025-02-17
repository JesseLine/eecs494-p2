using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 2.5f;
    public float reducedSpeed;

    private NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        reducedSpeed = speed / 2;
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

    public void ReduceSpeed()
    {
        navMeshAgent.speed = reducedSpeed;
    }
    public void NormalSpeed()
    {
        navMeshAgent.speed = speed;
    }
}
