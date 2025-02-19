using UnityEngine;

public class CameraLook : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    private bool gameOver = false;
    private bool gamePause = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        if (gamePause) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //stops over-rotation of camera

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        playerBody.Rotate(Vector3.up * mouseX);


    }

    void _OnGameOver(GameOverEvent e)
    {
        Cursor.lockState = CursorLockMode.None;
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
