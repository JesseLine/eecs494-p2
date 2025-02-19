using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        Debug.Log("CONTINUE");
        EventBus.Publish<UnPauseEvent>(new UnPauseEvent());
        
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

public class UnPauseEvent
{

}
