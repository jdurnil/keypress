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
    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
