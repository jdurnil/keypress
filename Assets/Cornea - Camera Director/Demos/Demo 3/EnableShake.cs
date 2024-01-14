using UnityEngine;

public class EnableShake : MonoBehaviour
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
            //this method starts the shake method
            Cornea.Shake.Shake();
        }
    }
}
