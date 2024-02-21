using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class Listener : MonoBehaviour
{
    // Start is called before the first frame update
    public EventDispatcher dispatcher;

    public OSCReceiver receiver;
    void Start()
    {
        receiver.LocalPort = 6000;
        receiver.Bind("*", OnReceiveEvent);
        
    }

    void OnReceiveEvent(OSCMessage message)
    {
        

     

        Debug.Log(message);

        var addresschannelArray = message.Address.Split('/');
        if(addresschannelArray.Length > 3)
        {
            var channelstring = addresschannelArray[3];
            int channel;
            bool succeed = int.TryParse(channelstring, out channel);

            if (message.Address.Contains("volume"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnVolumeEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("level-l-real"))
            {
                float value = 0;

                message.ToFloat(out value);


                if (channelstring != "master" && succeed)
                {
                    //value = vvalue / 1.0f;
                    var decibelVal = Mathf.Log10(value) * 20f;
                    var normalizedVal = JMAP(decibelVal, -80f, 0f, 0f, 1f);
                   // value = -1 * (Mathf.Log(value));
                    dispatcher.OnLevelLEvent.Invoke(channel, normalizedVal);
                }
            }
          
            else if (message.Address.Contains("level-r-real"))
            {
                float value = 0;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    var decibelVal = Mathf.Log10(value) * 20f;
                    var normalizedVal = JMAP(decibelVal, -80f, 0f, 0f, 1f);
                    Debug.Log("level-r-real: " + normalizedVal);
                    dispatcher.OnLevelREvent.Invoke(channel, normalizedVal);
                }
            } 
            else if (message.Address.Contains("name") && !message.Address.Contains("current_name"))
            {
                string value = "";

                message.ToString(out value);
                dispatcher.OnLabelEvent.Invoke(channel, value);
            }
            else if (message.Address.Contains("pan"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnPanEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("mute"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnMuteEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("solo"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnSoloEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("select"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnSelectEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("pan"))
            {
                float value;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    dispatcher.OnPanEvent.Invoke(channel, value);
                }
            }
        }
        else
        {
            if (message.Address.Contains("volume"))
            {
                float value;

                message.ToFloat(out value);

                dispatcher.OnVolumeEvent.Invoke(0, value);
            }
            else if (message.Address.Contains("level-l") && !message.Address.Contains("level-l-real"))
            {
              
            }
            else if (message.Address.Contains("level-r") && !message.Address.Contains("level-r-real"))
            {
                
            }
        }
       

        
    }

    float JMAP(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
