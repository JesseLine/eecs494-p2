using UnityEngine;

public class GameController : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;
    public GameObject gameOverScreen;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Screen.SetResolution(1920, 1080, false);

        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        gameOverScreen.SetActive(false);
    }

    void _OnGameOver(GameOverEvent e)
    {
        gameOverScreen.SetActive(true);
    }
}
