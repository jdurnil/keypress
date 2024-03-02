using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testlerp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.LeanScale(new Vector3(0.1f, 0.091338f, 0.1f), 1f).setEase(LeanTweenType.easeOutQuart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
