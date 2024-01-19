using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelNumberReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    public Channel Channel;
    public int ChannelNumber;
    
    void Awake()
    {
        ChannelNumber = Channel.ChannelNumber;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
