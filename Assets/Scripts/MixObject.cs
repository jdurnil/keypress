using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixObject
{
    // Start is called before the first frame update
    public List<MixChannel> channels = new List<MixChannel>();
   
}

public class  MixChannel
{
    public float Pan { get; set; }
    public float Volume { get; set; }
    public bool Mute { get; set; }
    public bool Solo { get; set; }
    public bool Record { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public bool Selected { get; set; }
    public EQ Eq { get; set; }
    public int ChannelNumber { get; set; }
    public Automation Automation { get; set; }
}

public class  EQ
{
    
    public EQBand Low { get; set; }
    public EQBand LowMid { get; set; }
    public EQBand HighMid { get; set; }
    public EQBand High { get; set; }
    public EQBand LowCut { get; set; }
    public EQBand HighCut { get; set; }
    public bool Enabled { get; set; }
    public bool Bypass { get; set; }
    public bool Solo { get; set; }
    public bool Mute { get; set; }
    
}

public class EQBand
{
    public int Frequency { get; set; }
    public float Gain { get; set; }
    public float Q { get; set; }
    public bool Enabled { get; set; }
}

public class Automation
{
    public bool Latch { get; set; }
    public bool Touch { get; set; }
    public bool Read { get; set; }
}

