using UnityEngine;

public class CoroutinesManager : MonoBehaviour
{
    public static CoroutinesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
