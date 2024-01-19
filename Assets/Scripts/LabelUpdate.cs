using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelUpdate : MonoBehaviour
{
    public ChannelNumberReceiver channelNumberReceiver;
    public int Channel;
    public TMP_Text myTextVariable;
    // Start is called before the first frame update
    void Start()
    {
        Channel = channelNumberReceiver.ChannelNumber;
        myTextVariable.text = "It works";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDispatch(int channel, string value)
    {
        if(channel == Channel)
        {
            //
            //GetComponent<TextMesh>().text = value;
            myTextVariable.text = value;
        }
    }
}
