using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;

    public GameObject player;
    public float speed = 2.5f;
    public float reducedSpeed = 1f;

    public float currentSpeed;
    private NavMeshAgent navMeshAgent;

    private bool gamePause = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);

        currentSpeed = speed;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePause) return;

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

    void _OnPause(PauseEvent e)
    {
        gamePause = true;
    }

    void _OnUnPause(UnPauseEvent e)
    {
        gamePause = false;
    }
}
