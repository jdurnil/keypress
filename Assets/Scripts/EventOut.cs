using extOSC;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class EventOut : MonoBehaviour
{
    public UnityEvent<string, int, float> OnActivateEvent;
    public OSCTransmitter Transmitter;
    // Start is called before the first frame update
    void Start()
    {
        Transmitter.RemoteHost = "192.168.0.235";
        Transmitter.LocalPort = 7000;
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleIncoming(string action, int channel, float value)
    {
        ConstructMessage(action, channel, value);
    }

    public void ConstructMessage(string action, int channel, float value)
    {

       
        if(action == "volume" || action == "pan" || action == "select" || action == "mute" || action == "solo")
        {
            var address = "/mas/tracks/" + channel + "/" + action;

            var message = new OSCMessage(address);
            message.AddValue(OSCValue.Float(value));

            Transmitter.Send(message);
            if(action == "volume")
            {
                if (MixSingleton.Instance.MixObject.channels[channel] != null)
                {
                    MixSingleton.Instance.MixObject.channels[channel].Volume = value;
                }
                
            } else if(action == "pan")
            {
                if (MixSingleton.Instance.MixObject.channels[channel] != null)
                {
                    MixSingleton.Instance.MixObject.channels[channel].Pan = value;
                }
               
            } else if(action == "select")
            {
                if (MixSingleton.Instance.MixObject.channels[channel] != null)
                {
                    MixSingleton.Instance.MixObject.channels[channel].Selected = value == 1 ? true : false;
                }
                
            } else if(action == "mute")
            {
                if (MixSingleton.Instance.MixObject.channels[channel] != null)
                {
                    MixSingleton.Instance.MixObject.channels[channel].Mute = value == 1 ? true : false;
                }
                
            } else if(action == "solo")
            {
                if (MixSingleton.Instance.MixObject.channels[channel] != null)
                {
                    MixSingleton.Instance.MixObject.channels[channel].Solo = value == 1 ? true : false;
                }
                
            }
            
        } else if(action == "EQ")
        {

        } else if(action == "play" || action == "stop" || action == "record")
        {
            var address = "/mas/transport/" + action;

            var message = new OSCMessage(address);
            message.AddValue(OSCValue.Float(value));

            Transmitter.Send(message);
        }
       


    }
}
