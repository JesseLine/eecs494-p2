using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    Subscription<PauseEvent> pauseEventSubscription;
    Subscription<UnPauseEvent> unPauseEventSubscription;
    Subscription<GameOverEvent> gameOverEventSubscription;
    Subscription<NewWaveEvent> newWaveEventSubscription;
    Subscription<WaveEndEvent> waveEndEventSubscription;
    Subscription<BatteryPickUpEvent> batteryPickUpEventSubscription;

    public GameObject ON;
    public GameObject OFF;
    public float battery = 50f;
    public float maxBattery = 50f;
    public TextMeshProUGUI batteryPercentageText;

    private bool isON;

    private bool gamePause = false;
    private bool gameOver = false;
    private bool waveStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseEventSubscription = EventBus.Subscribe<PauseEvent>(_OnPause);
        unPauseEventSubscription = EventBus.Subscribe<UnPauseEvent>(_OnUnPause);
        gameOverEventSubscription = EventBus.Subscribe<GameOverEvent>(_OnGameOver);
        newWaveEventSubscription = EventBus.Subscribe<NewWaveEvent>(_OnNewWave);
        waveEndEventSubscription = EventBus.Subscribe<WaveEndEvent>(_OnWaveEnd);
        batteryPickUpEventSubscription = EventBus.Subscribe<BatteryPickUpEvent>(_OnBatteryPickUp);

        ON.SetActive(false);
        OFF.SetActive(true);
        isON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePause) return;
        if (gameOver) return;

        if (isON)
        {
            if (waveStarted)    //battery doesn't go down when the wave hasn't started
            {
                battery -= Time.deltaTime;
                if (batteryPercentageText != null)
                {
                    batteryPercentageText.text = "Battery: " + ((int)(battery / maxBattery * 100)).ToString() + "%";
                }
            }
            
            
        }
        if (!isON)
        {
            if (battery > maxBattery)
            {
                battery = maxBattery;
            }
            if (batteryPercentageText != null)
            {
                batteryPercentageText.text = "Battery: " + ((int)(battery/maxBattery * 100)).ToString() + "%";
            }
            
        }
        if (battery <= 0)
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

    void _OnGameOver(GameOverEvent e)
    {
        gameOver = true;
    }

    void _OnNewWave(NewWaveEvent e)
    {
        waveStarted = true;
    }

    void _OnWaveEnd(WaveEndEvent e)
    {
        waveStarted = false;
    }

    void _OnBatteryPickUp(BatteryPickUpEvent e)
    {
        battery = maxBattery;
    }
}
