using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelUpdate : MonoBehaviour
{
    public ChannelNumberReceiver channelNumberReceiver;
    public int Channel;
    // Start is called before the first frame update
    void Start()
    {
        Channel = channelNumberReceiver.ChannelNumber;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDispatch(int channel, string value)
    {
        if(channel == Channel)
        {
            GetComponent<TextMesh>().text = value;
        }
    }
}
