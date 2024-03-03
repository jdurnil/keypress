using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
