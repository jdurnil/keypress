using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHead : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TrackingAnchor;
    private GameObject head;
    private float initRotationX;
    private float initRotationY;
    private float initRotationZ;
    void Start()
    {

        head = TrackingAnchor.transform.GetChild(3).gameObject;
       
        initRotationY = head.transform.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        var rotation = head.transform.eulerAngles;
        var newRotation = rotation.y;
        var change = newRotation - initRotationY;


    }
}
