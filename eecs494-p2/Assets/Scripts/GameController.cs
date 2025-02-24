using UnityEngine;

public class GameController : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;
    public GameObject gameOverScreen;

    public GameObject pauseMenu;

    private bool gamePaused = false;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(1920, 1080, false);

        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        //will try to fix this after the playtest
        /*if(Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            Debug.Log("PAUSE!");
            gamePaused = true;
            pauseMenu.SetActive(true);
            EventBus.Publish<PauseEvent>(new PauseEvent());
        }*/
    }

    void _OnGameOver(GameOverEvent e)
    {
        if (gameOverScreen == null) return;
        gameOverScreen.SetActive(true);
    }

}

public class PauseEvent
{

}
