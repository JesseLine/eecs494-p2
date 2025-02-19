using UnityEngine;

public class LightController : MonoBehaviour
{
    Subscription<DamageDealtEvent> damageDealtSubscription;

    private string enemyEyeColorHex = "0x00680A";
    private Color enemyEyeColor;
    private Light light;
    private float timer = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageDealtSubscription = EventBus.Subscribe<DamageDealtEvent>(_OnDamageDealt);
        light = GetComponent<Light>();
        enemyEyeColor = hexToColor(enemyEyeColorHex);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            light.color = enemyEyeColor;
        }
    }

    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    void _OnDamageDealt(DamageDealtEvent e)
    {
        if(e.go == transform.parent.gameObject)
        {
            if (timer >= 1)
            {
                light.color = Color.red;
                timer = 0;
            }
        }
        
        

    }
}
