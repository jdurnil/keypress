using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationRangeValue : MonoBehaviour
{
    public enum Axis { X, Y, Z };

    public Axis axis;
    public float initialAngle;
    public float endingAngle;
    [Range(0, 1)] public float rotationValue;
    private float previousRotationValue = float.NaN;
    private Transform localTransform;
    public UnityEvent<float> OnRotationValueChanged;

    private float angle;

    void Start ()
    {
        localTransform = this.transform;
        rotationValue = GetRotationRangeValue(NormalizeAngle(angle), NormalizeAngle(initialAngle), NormalizeAngle(endingAngle));
    }

    void Update ()
    {
        switch (axis)
        {
            case Axis.X:
                angle = localTransform.localEulerAngles.x;
                break;
            case Axis.Y:
                angle = localTransform.localEulerAngles.y;
                break;
            case Axis.Z:
                angle = localTransform.localEulerAngles.z;
                break;
        }
        
        rotationValue = GetRotationRangeValue(NormalizeAngle(angle), NormalizeAngle(initialAngle), NormalizeAngle(endingAngle));

        // Check if the rotation has changed since the last frame
        if (!Mathf.Approximately(rotationValue, previousRotationValue))
        {
            OnRotationValueChanged.Invoke(rotationValue);
            previousRotationValue = rotationValue;
        }
    }

    float GetRotationRangeValue(float currentAngle, float normalizedStart, float normalizedEnd)
    {
        // Map the current angle from -180 to 180 to 0 to 360
        currentAngle = currentAngle > 180f ? currentAngle - 360f : currentAngle;
        
        // Calculate the range
        float range = normalizedEnd - normalizedStart;
        float adjustedCurrentAngle = currentAngle - normalizedStart;
        
        // Adjust for when the range crosses over 360 degrees
        if (range < 0f) 
        {
            range += 360f;
            adjustedCurrentAngle += 360f;
        }
        
        // Normalize the angle from 0 to 1
        return Mathf.Clamp01(adjustedCurrentAngle / range);
    }

    float NormalizeAngle(float angle)
    {
        // Normalize angle to be within 0 - 360 when negative
        angle = (angle + 360f) % 360f;
        return angle;
    }

    public void ShowAngleValue (float value)
    {
        Debug.Log(value);
    }
}
