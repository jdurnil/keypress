using UnityEngine;

public class EnableLocation : MonoBehaviour
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
            //this method starts the method that lerps the camera
            //to the specified index position
            Cornea.Lerp.CameraLerp(5);
        }
    }
}
