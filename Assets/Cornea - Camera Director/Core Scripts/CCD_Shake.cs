using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCD_Shake : MonoBehaviour
{

    private CorneaCameraDirector Cornea;                    //get the main Cornea script
    private float duration;                                 //get the duration from the main cornea script
    private float magnitude;                                //get the magnitude from the main cornea script

    //trigger on start
    void Start(){
        Cornea = GetComponent<CorneaCameraDirector>();
    }

    //set the defaults
    void Defaults(){
        duration = Cornea.ShakeDuration;
        magnitude = Cornea.ShakeMagnitude;
    }

    //the main trigger method
    public void Shake(){
        //get the defaults first
        Defaults();

        //trigger the coroutine
        StartCoroutine(Shaker(duration, magnitude));
    }

    //coroutine that does the actual shaking
    public IEnumerator Shaker(float duration, float magnitude)
    {
        //save the original camera local position
        Vector3 originalPos = transform.localPosition;

        //to save the elapsed time
        float elapsed = 0.0f;

        while(elapsed < duration){
            //make new position times magnitude
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            //set the camera local position axis to the new magnitudes
            transform.localPosition = new Vector3(x, y, originalPos.z);

            //increment the passed delta time
            elapsed += Time.deltaTime;

            //wait for the next frame
            yield return null;
        }

        //return the camera to it's original position
        transform.localPosition = originalPos; 
    }

    //hide this script from the inspector
    void Reset(){
        this.hideFlags = HideFlags.HideInInspector;
    }
}
