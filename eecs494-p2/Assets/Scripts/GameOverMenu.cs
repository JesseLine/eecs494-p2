using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        EventBus.Publish<RestartGameEvent>(new RestartGameEvent());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

public class RestartGameEvent
{

}
