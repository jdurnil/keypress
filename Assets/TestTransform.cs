using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransform : MonoBehaviour
{
    public Vector3 Position;
    // Start is called before the first frame update
    void Start()
    {
        
        Position = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Position;
    }
}
