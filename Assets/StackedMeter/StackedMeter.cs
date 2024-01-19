using UnityEngine;

public class StackedMeter : MonoBehaviour
{
    public Renderer[] bars;
    public float greenLevelPercentage = 0;
    public Color greenColor = Color.green;
    public float yellowLevelPercentage = 70;
    public Color yellowColor = Color.yellow;
    public float redLevelPercentage = 90;
    public Color redColor = Color.red;
    public ChannelNumberReceiver channelNumberReceiver;
    public int Channel;
    public float maxValue = 50f;

    [SerializeField, Range(0f, 1f)]
    private float meterValue = 0f;

    private Color originalColor;

    private bool invertList = true;

    private void Start ()
    {
        Channel = channelNumberReceiver.ChannelNumber;
        originalColor = bars[0].material.color;
        if(invertList)
        {
            System.Array.Reverse(bars);
        }
    }

    private void Update ()
    {
        UpdateMeter();
    }

    public void ReceiveDispatch(int channel, float value)
    {
        if(channel == Channel)
        {
            SetValue(value);
        }
       
    }

    public void ChangeMaxValue (float value)
    {
        maxValue = value;
    }

    public void SetValue (float value)
    {
        meterValue = Mathf.Clamp(value, 0, maxValue);
        meterValue = meterValue / maxValue;
        UpdateMeter();
    }

    private void UpdateMeter ()
    {
        int barsAmount = Mathf.FloorToInt(meterValue * bars.Length);
        for (int i = 0; i < bars.Length; i++)
        {
            if (i < barsAmount)
            {
                bars[i].material.color = GetColorForBar(i);
                bars[i].material.EnableKeyword("_EMISSION");
                bars[i].material.SetColor("_EmissionColor", GetColorForBar(i));

            }
            else
            {
                bars[i].material.color = originalColor;
                //bars[i].material.SetColor("_EmissionColor", originalColor);
                bars[i].material.DisableKeyword("_EMISSION");
            }
        }
    }

    private Color GetColorForBar (int barIndex)
    {
        float barPercentage = (float)barIndex / bars.Length * 100f;
        if (barPercentage > redLevelPercentage)
        {
            return redColor;
        }
        else if (barPercentage > yellowLevelPercentage)
        {
            return yellowColor;
        }
        else if (barPercentage >= greenLevelPercentage)
        {
            return greenColor;
        }
        else
        {
            return originalColor;
        }
    }
}
