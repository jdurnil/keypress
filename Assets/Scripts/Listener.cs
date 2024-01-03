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
       
        
        

        if (message.Address.Contains("volume"))
        {
            float value;

            message.ToFloat(out value);
            var channelstring = message.Address.Split('/')[3];
            int channel;
            bool succeed = int.TryParse(channelstring, out channel);
            if(channelstring != "master" && succeed)
            {
                dispatcher.OnVolumeEvent.Invoke(channel, value);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}