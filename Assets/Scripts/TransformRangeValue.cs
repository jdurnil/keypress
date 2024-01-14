using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransformRangeValue : MonoBehaviour
{
    public bool useRefStart = false;
    public Transform startPositionTransform;
    public Vector3 startPosition;
    public bool useRefEnd = false;
    public Transform endPositionTransform;
    public Vector3 endPosition;
    public ChannelNumberReceiver channelNumberReceiver;
    public EventOut eventOut;
    int channel;
    [Range(0, 1)] public float faderValue;

    private Vector3 previousPosition;
    private bool isPositionSet = false;

    public UnityEvent<float> OnFaderValueChanged;

    private void Start ()
    {
        channel = channelNumberReceiver.ChannelNumber;

        if(useRefStart && startPositionTransform == null)
        {
            Debug.LogError("Start position transform is null");
        }
        else if(useRefEnd && endPositionTransform == null)
        {
            Debug.LogError("End position transform is null");
        }
        else if(!useRefStart && !useRefEnd)
        {
            if((startPosition - endPosition).sqrMagnitude < 0.01f)
            {
                Debug.LogError("Start and end positions are too close to each other");
            }
        }
        
        var startPos = useRefStart ? startPositionTransform.position : startPosition;
        var endPos = useRefEnd ? endPositionTransform.position : endPosition;

        // faderValue = GetRangeValue(startPosition, endPosition, transform.position);
        transform.position = Vector3.Lerp(startPos, endPos, faderValue);
        previousPosition = transform.position;
        Debug.Log("channel: " + channel);
    }

    private void Update ()
    {
        var startPos = useRefStart ? startPositionTransform.position : startPosition;
        var endPos = useRefEnd ? endPositionTransform.position : endPosition;

        // Perform the check only if the position has changed
        if(transform.position != previousPosition)
        {
            faderValue = GetRangeValue(startPos, endPos, transform.position);
            eventOut.OnActivateEvent.Invoke("volume", channel, faderValue);
            OnFaderValueChanged.Invoke(faderValue);
            previousPosition = transform.position;
        }
    }

    public float GetRangeValue(Vector3 start, Vector3 end, Vector3 current)
    {
        Vector3 startToEnd = end - start;
        Vector3 startToCurrent = current - start;

        // Project startToCurrent onto startToEnd to find the normalized position
        return Mathf.Clamp01(Vector3.Dot(startToCurrent, startToEnd) / startToEnd.sqrMagnitude);
    }

    public void ReceiveDispatch(int Channel, float value)
    {
        if(Channel == channel)
        {
            SetFaderValue(value);
        }
        
    }

    public void SetFaderValue (float value)
    {
        var startPos = useRefStart ? startPositionTransform.position : startPosition;
        var endPos = useRefEnd ? endPositionTransform.position : endPosition;

        faderValue = value;
        transform.position = Vector3.Lerp(startPos, endPos, faderValue);
        previousPosition = transform.position;
    }

    public void ShowFaderValue (float value)
    {
        Debug.Log(value);
    }
}
