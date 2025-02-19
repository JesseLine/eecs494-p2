using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;

    public CharacterController controller;
    public Transform groundCheck;

    public float speed = 12f;
    public float gravity = -10f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;
    private bool gameOver = false;
    private bool gamePause = false;

    private void Start()
    {
        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        if (gamePause) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void _OnGameOver(GameOverEvent e)
    {
        gameOver = true;
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
