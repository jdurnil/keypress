using UnityEngine;

public class CCD_Warp : MonoBehaviour
{   
    [HideInInspector]
    public float WarpMax;
    [HideInInspector]                                                                                   //maximum distance for the warp effect
    public bool Go;                                                                                     //flag the warp effect to go forward
    [HideInInspector]
    public bool Back;                                                                                   //flag for warp effect to get back
    [HideInInspector]
    public float WarpSpeed;                                                                             //speed of the warp effect
    [HideInInspector]
    public float FOVDEF;                                                                                //camera Field of View default
    [HideInInspector]
    private bool _WarpStart = false;                                                                    //Start property value
    private CorneaCameraDirector Cornea;                                                                //get the main CORNEA script

    public bool Start {                                                                                 //trigger warp effect property
        get {
            //return the value of the property
            return _WarpStart;
        }

        set {
            //set the speed and max distance that're set from the main script
            WarpSpeed = Cornea._WarpSpeed;
            WarpMax = Cornea._WarpDistance;
            
            //only enable warp when properties are false 
            if(value == true && (!Start && !Back && !Go)){
                //set the value 
                _WarpStart = value;
                //set the go and back flags
                Back = false;
                Go = true;
            }else{
                _WarpStart = false;
            }
        }
    }

    //trigger on awake
    void Awake(){
        //get main script
        Cornea = GetComponent<CorneaCameraDirector>();
        //set the defaults
        Defaults();
    }

    //sets the defaults of the variables
    public void Defaults(){
        //set the defaults from the main Cornea script variables
        FOVDEF = Cornea._WarpFOVDefault;
        WarpSpeed = Cornea._WarpSpeed;
        WarpMax = Cornea._WarpDistance;
        Go = false;
        Back = false;
    }

    //the warping method gets run on the update loop in the main script
    public void Warping(){
        //warp effect going afar
        if(Go){
            //if current FOV is less, continue incrementing - causing warping effect
            if(Camera.main.fieldOfView <= WarpMax){
                Camera.main.fieldOfView += Time.deltaTime + 1f / (WarpSpeed / 2f);
            }else{
                //when the forward warp is done
                //enable the flag for the backward warp
                Go = false;
                Back = true;
            }
        }

        //warp effect coming back
        if(Back){
            //if current FOV is bigger, continue decrementing
            if(Camera.main.fieldOfView > FOVDEF){
                Camera.main.fieldOfView -= Time.deltaTime + 1f / (WarpSpeed / 2f);
            }else{
                //when done reset everything back to normal
                Camera.main.fieldOfView = FOVDEF;
                Back = false;
                Start = false;
            }
        }   
    }

    //hide this script from inspector
    void Reset(){
        this.hideFlags = HideFlags.HideInInspector;
    }
}
