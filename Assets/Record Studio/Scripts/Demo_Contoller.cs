using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Contoller : MonoBehaviour {

    public Transform Door;
    public Transform Mic;
    public Transform Eq;
    public Transform Sin;
    public Transform In;
    public Transform Mix;

    public void DoorF()
    {
        Door.GetComponent<Animation>().Play();
    }

    public void MicF()
    {
        Mic.GetComponent<Animation>().Play();
    }

    public void EqF()
    {
        Eq.GetComponent<Animation>().Play();
    }

    public void SinF()
    {
        Sin.GetComponent<Animation>().Play();
    }

    public void InF()
    {
        In.GetComponent<Animation>().Play();
    }

    public void MixF()
    {
        Mix.GetComponent<Animation>().Play();
    }
}
