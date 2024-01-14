using UnityEngine;

public class EnablePosition : MonoBehaviour
{
    CorneaCameraDirector Cornea;

    void Start()
    {
        //get the main Cornea script
        Cornea = GetComponent<CorneaCameraDirector>();    
    }

    void Update(){
        //when W is clicked on the keyboard
        if(Input.GetKeyDown(KeyCode.W)){
            //this method starts the warp method
            Cornea.Lerp.CameraLerpPrev();
        }

        //when E is clicked on the keyboard
        if(Input.GetKeyDown(KeyCode.E)){
            //this method makes the camera lerp to the next position
            Cornea.Lerp.CameraLerpNext();
        }
    }
}
