using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;
    Subscription<NewWaveEvent> newWaveEventSubscription;

    public GameObject player;

    public GameObject waveController;

    public float speed = 4f;
    public float reducedSpeed = 1f;

    //public static int currentWave = 0;
    public float currentSpeed;
    private NavMeshAgent navMeshAgent;

    private float enemySpeedIncreaseMultiplyer = 1.15f;

    private bool gamePause = false;

    private Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);
        newWaveEventSubscription = EventBus.Subscribe<NewWaveEvent>(_OnNewWave);

        waveController = GameObject.FindGameObjectWithTag("WaveController");
        //Debug.Log(currentWave);
        speed = (speed * Mathf.Pow(enemySpeedIncreaseMultiplyer, waveController.GetComponent<WaveController>().GetCurrentWave()-1));
        reducedSpeed = speed / 2;

        originalScale = transform.localScale;

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

        //lerp size to original
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 15);
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

    void _OnNewWave(NewWaveEvent e)
    {
        //currentWave++;
    }
}
