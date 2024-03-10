using UnityEngine;

public class testlerp : MonoBehaviour
{
    // Start is called before the first frame update
    public void Grow(Vector3 scale)
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.LeanScale(scale, 1f).setEase(LeanTweenType.easeOutQuart);
    }

    public void Shrink()
    {
        transform.LeanScale(new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutQuart);
    }
    //public void OnEnable()
    //{
    //    transform.localScale = new Vector3(0f, 0f, 0f);
    //    transform.LeanScale(new Vector3(0.1f, 0.091338f, 0.1f), 1f).setEase(LeanTweenType.easeOutQuart);
    //}

    // Update is called once per frame
}
