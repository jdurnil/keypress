using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    // Static singleton property
    public static MixSingleton Instance { get; private set; }

    // Public member
    public MixObject MixObject { get; set; }

    // Initialize the singleton in the Awake() function.
    private void Awake()
    {
        if (Instance == null)
        {
            MixObject = new MixObject();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
