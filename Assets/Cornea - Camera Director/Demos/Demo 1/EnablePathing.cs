using UnityEngine;

public class EnablePathing : MonoBehaviour
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
            //this method starts the camera pathing
            Cornea.Lerp.CameraLerpPath();
        }
    }
}
