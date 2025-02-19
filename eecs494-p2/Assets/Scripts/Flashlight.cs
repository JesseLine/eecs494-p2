using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;

    public GameObject ON;
    public GameObject OFF;
    public float battery = 100f;
    public TextMeshProUGUI batteryPercentageText;

    private bool isON;

    private bool gamePause = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);

        ON.SetActive(false);
        OFF.SetActive(true);
        isON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePause) return;

        if (isON)
        {
            battery -= Time.deltaTime;
            batteryPercentageText.text = "Battery: " + ((int)battery).ToString() + "%";
        }
        if(battery <= 0)
        {
            isON = false;
            ON.SetActive(false);
            OFF.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isON)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }
            if (!isON && battery > 0)
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }
            isON = !isON;
        }
    }
    public bool isOn()
    {
        return isON;
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
