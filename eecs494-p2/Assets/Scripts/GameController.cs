using UnityEngine;

public class GameController : MonoBehaviour
{
    Subscription<GameOverEvent> gameOverEventSubscription;
    public GameObject gameOverScreen;
    private void Start()
    {
        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        gameOverScreen.SetActive(false);
    }

    void _OnGameOver(GameOverEvent e)
    {
        gameOverScreen.SetActive(true);
    }
}
