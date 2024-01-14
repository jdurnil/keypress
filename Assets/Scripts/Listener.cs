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
                    value = value / 1.0f;
                   // value = -1 * (Mathf.Log(value));
                    dispatcher.OnLevelLEvent.Invoke(channel, value);
                }
            }
            else if (message.Address.Contains("level-r-real"))
            {
                float value = 0;

                message.ToFloat(out value);

                if (channelstring != "master" && succeed)
                {
                    value = value / 1.0f;
                    //var test = Mathf.Log(value,5);
                    //value = -1 * (Mathf.Log(value));


                    //value = .01f * ( 100 - (100 * (Mathf.Pow(2, (20 * (value - 1))))));
                    dispatcher.OnLevelREvent.Invoke(channel, value);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
