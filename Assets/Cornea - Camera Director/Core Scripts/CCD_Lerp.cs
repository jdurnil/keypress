using UnityEngine;

public class CCD_Lerp : MonoBehaviour
{
    private Transform[] CameraPositions;                        //array of camera positions
	private bool _lerp = false;                                 //StartLerp property modifier
	private Vector3 StartPos;                                   //save current camera position
	private Vector3 EndPos;                                     //save the camera position going to
	private float lerpTime = 5f;                                //lerp time
	private float QuatLerpTime = 1f;                            //lerp quaternion time
	private float currentLerpTime = 0f;                         //for lerp time calculation
	private int CurrentIndex;                                   //current index of the camera position
	[HideInInspector]
	public bool IsActive = false;                              	//whether the lerping method is active
	private float perc;                                         //lerping percentage done
	private float QuatPerc;                                     //quaternion lerp percentage done
	private bool LerpRotation = false;                          //whether rotation lerping is on or off
	private bool ShouldStopLerp = false;                        //flag that the lerping should stop
	private bool ShouldStopQuatLerp = false;                    //flag that the quaternion lerp should be stopped
    private CorneaCameraDirector Cornea;                        //get the main Cornea script
	private bool Handheld;										//save the original Handheld bool
    public int GetCurrentIndex {                                //property for returning the current index of the camera position
		get { return CurrentIndex; }
	}
    public bool StartLerp {                                    	//property that triggers the lerping
		//return the property modifier
        get{ return _lerp; }                                    

		set{
			_lerp = value;
            //if lerping should initiate
            //return everything to default
            //and trigger the lerping
			if (_lerp){
                Defaults();
				currentLerpTime = 0;
                ShouldStopLerp = false;
				ShouldStopQuatLerp = false;
				IsActive = true;
				Lerping (CurrentIndex);
            }
		}
	}
	private bool isPathing = false;								//flag whether pathing is enabled or not
	private float lerpingThreshold = 0.01f;
	bool loopPathing = true;

    //trigger on awake
    void Start(){
        //get the main cornea script
        Cornea = GetComponent<CorneaCameraDirector>();
        CurrentIndex = 0;
        //get the defaults from the main script
        Defaults();
    }

    //set the defaults from the main script
    void Defaults(){
        lerpTime = Cornea.LerpTime;
        QuatLerpTime = Cornea.RotationLerpTime;
        LerpRotation = Cornea.LerpRotation;
        CameraPositions = Cornea.LerpCameraPositions;
		loopPathing = Cornea.CameraPathingLoop;
    }

    //lerp camera to stated index position
	public void CameraLerp(int StateIndex){
		if ( (StateIndex + 1) <= CameraPositions.Length) {
			CurrentIndex = StateIndex;
			StartLerp = true;
		} else {
			Debug.LogError ("Camera Lerping can't go to stated index. Out of bounds!");
		}
	}

    //lerp camera to the next index
	public void CameraLerpNext(){
		if ( (CurrentIndex + 1) < CameraPositions.Length) {
			CurrentIndex++;
			StartLerp = true;
		}
	}

	//lerp camera to the previous index
	public void CameraLerpPrev(){
		if (CurrentIndex > 0) {
			CurrentIndex--;
			StartLerp = true;
		}
	}

	//lerp the positions one after another by incrementing
	public void CameraLerpPath(){
		isPathing = true;
		lerpingThreshold = 0.01f;
		CameraLerpNext();
	}

    //the main lerping method
	public void Lerping(int index = 0){
        //if IsActive not true
        //quit the method
        if(!IsActive){
            return;
        }

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		perc = currentLerpTime / lerpTime;
		QuatPerc = currentLerpTime / QuatLerpTime;

		if (!ShouldStopLerp){
			transform.position = Vector3.Lerp (transform.position, CameraPositions [index].position, perc);
        }

		//only if checked, lerp rotation as well
		if (LerpRotation && !ShouldStopQuatLerp){
			transform.rotation = Quaternion.Lerp (transform.rotation, CameraPositions [index].rotation, QuatPerc);
        }

		//if distance between two goals of lerp is smaller than threshold then stop lerp
		if (Vector3.Distance (transform.position, CameraPositions [index].position) <= lerpingThreshold){
            ShouldStopLerp = true;
			//if rotation lerping is enabled
			if (LerpRotation) {
				//if angle between two quaternions is smaller than threshold then stop
				if (Quaternion.Angle(transform.rotation, CameraPositions [index].rotation) <= lerpingThreshold){
					ShouldStopQuatLerp = true;
					IsActive = false;
				}
			//if lerp rotation disabled then stop the method
			}else {
				IsActive = false;
			}
			
			//if pathing is enabled and current index is smaller than the max length
			//increment to next position
			if(isPathing && (CurrentIndex + 1 < CameraPositions.Length)){
				CameraLerpNext();
			}else{
				if(loopPathing){
					CameraLerp(0);
				}else{
					isPathing = false;
				}
			}
		}
	}

    //hide this script from the inspector
    void Reset(){
        this.hideFlags = HideFlags.HideInInspector;
    }
}
