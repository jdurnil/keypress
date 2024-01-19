using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DispatchBaseEvent : UnityEvent<int, float> { }
public class DispatchOutBaseEvent : UnityEvent<string, int, float> { }

public class EventDispatcher : MonoBehaviour
{
    public DispatchBaseEvent OnVolumeEvent;
    public UnityEvent<int, float> OnLevelLEvent;
    public UnityEvent<int, float> OnLevelREvent;
    public UnityEvent<int, string> OnLabelEvent;
    public UnityEvent<int, float> OnPanEvent;
    public UnityEvent<int, float> OnMuteEvent;
    public UnityEvent<int, float> OnSoloEvent;
    public UnityEvent<int, float> OnSelectEvent;
    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
