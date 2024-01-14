using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalizer_AK : MonoBehaviour
{

    public AudioSource aSource;
    public float[] samples = new float[64];
    public Transform[] cubes;
    private Vector3 gravity = new Vector3(0.0f, 0.25f, 0.0f);
    public Transform[] speakers;

    void FixedUpdate()
    {
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);

        if (cubes.Length > 0)
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                if (cubes[i].localScale.y <= Mathf.Clamp(samples[i] * (50 + i * i) / 10, 0.04f, 0.6f))
                {
                    cubes[i].localScale = new Vector3(0.04f, Mathf.Clamp(samples[i] * (50 + i * i) / 5, 0.04f, 0.6f), 0.04f);
                }
                else
                {
                    cubes[i].localScale = new Vector3(0.04f, cubes[i].localScale.y - Time.deltaTime * 2, 0.04f);
                }
            }
        }
        if (speakers.Length > 0)
        {
            for (int i = 0; i < speakers.Length; i++)
            {
                if (speakers[i].localPosition.z > -Mathf.Clamp(samples[i+28] * (50 + i * i) / 700, -0.03f, 0.03f))
                {
                    speakers[i].localPosition = new Vector3(0f, 0f, -Mathf.Clamp(samples[i] * (50 + i * i) / 400, -0.03f, 0.03f));
                }
                else
                {
                    speakers[i].localPosition = Vector3.Lerp(Vector3.zero, speakers[i].localPosition, Time.deltaTime);
                }
            }
        }
    }
}
