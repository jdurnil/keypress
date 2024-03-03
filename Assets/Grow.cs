using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Vector3 scale;
    private void OnEnable()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.LeanScale(scale, 1f).setEase(LeanTweenType.easeInQuart);


    }
}
