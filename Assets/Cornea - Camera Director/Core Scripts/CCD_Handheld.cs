using UnityEngine;

public class CCD_Handheld : MonoBehaviour
{
    private Transform[] CameraPositions = new Transform[1];
	private float lerpTime = 3000f;                             //lerp time
	private float currentLerpTime = 0f;                         //for lerp time calculation
	private int CurrentIndex = 0;                               //current index of the camera position
	private bool IsActive = false;                              //whether the lerping method is active
	private float perc;                                         //lerping percentage done
	private bool LerpRotation = false;                          //whether rotation lerping is on or off
    private Transform Shaker;                                   //the shaking position transform

    private CorneaCameraDirector Cornea;                        //get the main cornea script
    private float magnitudeX, magnitudeY, magnitudeZ;           //magnitude of the axis
    private float minX,                                         //range min and max floats to randomize between for each axis
    maxX, 
    minY, 
    maxY, 
    minZ, 
    maxZ;

    private bool _start = false;                                //property modifier
    public bool Start {                                         //property for triggering the method
        get {
            return _start;
        }

        set {
            _start = value;

            if(value){
                GeneratePos();
            }else{
                IsActive = false;
            }
        }
    }
    private CCD_Lerp CorneaLerper;

    //trigger on start
    void Awake(){
        //get the main script
        Cornea = GetComponent<CorneaCameraDirector>();
        CorneaLerper = GetComponent<CCD_Lerp>();
    }

    //set variables to defaults from the main script
    void Defaults(){
        lerpTime = Cornea.HandheldMovementSpeed;

        magnitudeX = Cornea.HandheldMagnitudeX;
        magnitudeY = Cornea.HandheldMagnitudeY;
        magnitudeZ = Cornea.HandheldMagnitudeZ;
        
        minX = Cornea.HandheldMinX;
        maxX = Cornea.HandheldMaxX;

        minY = Cornea.HandheldMinY;
        maxY = Cornea.HandheldMaxY;

        minZ = Cornea.HandheldMinZ;
        maxZ = Cornea.HandheldMaxZ;

        Shaker = Cornea.Handholder;
    }

    //normalize min axes and magnitudes to 0 every n interval
    void AxisNormalizer(){
        float[] times = {1f, 2f, 3f};
        string[] axis = {"x", "y", "z", "all"};
        
        float ChosenTime = times[Random.Range(0, times.Length)];
        string ChosenAxis = axis[Random.Range(0, axis.Length)];

        if(ChosenAxis == "all"){
            minX = 0f;
            minY = 0f;
            minZ = 0f;
            
            magnitudeX = 0f;
            magnitudeY = 0f;
            magnitudeZ = 0f;
        }
        
        else if(ChosenAxis == "x"){
            minX = 0f;
            minY = -1f;
            minZ = -1f;

            magnitudeX = 0f;
            magnitudeY = Cornea.HandheldMagnitudeY;
            magnitudeZ = Cornea.HandheldMagnitudeZ;
        }
        
        else if(ChosenAxis == "y"){
            minY = 0f;
            minX = -1f;
            minZ = -1f;

            magnitudeY = 0f;
            magnitudeX = Cornea.HandheldMagnitudeX;
            magnitudeZ = Cornea.HandheldMagnitudeZ;
        }
        
        else{
            minZ = 0f;
            minX = -1f;
            minY = -1f;

            magnitudeZ = 0f;
            magnitudeY = Cornea.HandheldMagnitudeY;
            magnitudeX = Cornea.HandheldMagnitudeX;
        }

        Invoke("AxisNormalizer", ChosenTime);
    }

    //generate a random position to lerp to
    private void GeneratePos(){
        //get the defaults
        Defaults();

        //normalize an axis for smoothness
        AxisNormalizer();

        //generate a random position on each axis
        float x = Random.Range(minX, maxX) * magnitudeX;
        float y = Random.Range(minY, maxY) * magnitudeY;
        float z = Random.Range(minZ, maxZ) * magnitudeZ;
        
        //set the new positions to the transform
        Shaker.position = new Vector3 (
            transform.position.x + x,
            transform.position.y + y,
            transform.position.z + z
        );

        //set the new shaker position to the first index
        //of the array
        CameraPositions[0] = Shaker;

        //reset the lerp attributes
        //currentLerpTime = 0;
        if(IsActive){
            currentLerpTime = currentLerpTime / 2;
        }else{
            currentLerpTime = 0f;
        }
        
        //when true the update loop will trigger the lerp
        IsActive = true;
    }

    //main lerping method
    public void Lerping(int index = 0){
        //if IsActive not true
        //quit the method
        if(!IsActive){
            return;
        }

        //stop handheld if lerping is active
        if(CorneaLerper.IsActive){
            GeneratePos();
            return;
        }

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		perc = currentLerpTime / lerpTime;
		transform.position = Vector3.Lerp (transform.position, CameraPositions [index].position, perc);

		//if distance between two ends of lerp is smaller than threshold then stop lerp
		if (Vector3.Distance (transform.position, CameraPositions [index].position) <= 0.1f){        
            GeneratePos();
		}
	}

    //hide this script from the inspector
    void Reset(){
        this.hideFlags = HideFlags.HideInInspector;
    }
}
