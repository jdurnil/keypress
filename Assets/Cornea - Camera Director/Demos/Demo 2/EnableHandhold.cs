using UnityEngine;

public class EnableHandhold : MonoBehaviour
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
            //this method starts the handheld method
            Cornea.Handheld.Start = true;
        }
    }
}
