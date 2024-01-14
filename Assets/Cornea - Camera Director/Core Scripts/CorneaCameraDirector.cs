using UnityEngine;

[RequireComponent(typeof(CCD_Warp))]
[RequireComponent(typeof(CCD_Lerp))]
[RequireComponent(typeof(CCD_Handheld))]
[RequireComponent(typeof(CCD_Shake))]

public class CorneaCameraDirector : MonoBehaviour
{
    //public variables for the warp effect
    #region Warp
    
    [HideInInspector]
    public CCD_Warp Warp;                              //instance of the warp effect script
    [Header("Warp Variables")]
    
    [Tooltip("The speed of the warp")]
    public float _WarpSpeed = 0.3f;                    //warp speed
    
    [Range(0f, 160f)]
    [Tooltip("The maximum distance of the warp")]
    public float _WarpDistance = 160f;                 //maximum warp distance
    
    [Tooltip("The default camera Field of View to return to after warp")]
    public float _WarpFOVDefault = 60f;                //camera FOV to get back to after warp
    #endregion
    
    //public variables for the lerp effect
    #region Lerp

    [HideInInspector]
    public CCD_Lerp Lerp;                                   //get the lerp script instance
    
    [Header("Lerp Variables")]
    [Space(10)]

    [Tooltip("The array of camera positions")]
    public Transform[] LerpCameraPositions;                 //array of Transforms camera positions
    
    [Tooltip("The time/speed of lerp")]
    public float LerpTime = 10f;                            //lerping time
    
    [Tooltip("Should rotation be lerped as well?")]
    public bool LerpRotation = true;                       //should rotation be lerped as well or not
    
    [Tooltip("The time/speed of Quaternion lerping")]
    public float RotationLerpTime = 10f;                    //time of rotation lerp

    [Tooltip("Should camera pathing be looped")]
    public bool CameraPathingLoop = false;

    #endregion

    //public variables for the handheld effect
    #region Handheld

    [HideInInspector]
    public CCD_Handheld Handheld;                       //instance of Handheld script
    
    [Header("Handheld Variables")]
    [Space(10)]

    [Tooltip("A Transform of an empty gameobject that should act as the handholder")]
    public Transform Handholder;                        //Transform of the shaker object
    
    [Tooltip("The speed of the handheld movement")]
    public float HandheldMovementSpeed = 3000f;         //speed of the handheld mode movement
    
    [HideInInspector]
    public float HandheldMinX = -1f,                    //range of min and max of the axis to randomize 
    HandheldMaxX = 1f,
    HandheldMinY = -1f,
    HandheldMaxY = 1f,
    HandheldMinZ = -1f,
    HandheldMaxZ = 1f;
    
    [Tooltip("The magnitude of the axis during handheld movement. The larger the number, the stronger it becomes")]
    public float HandheldMagnitudeX = 0.12f,            //the magnitude of the axis
    HandheldMagnitudeY = 0.12f, 
    HandheldMagnitudeZ = 0.12f;
    
    #endregion

    //public varaibles for the shake effect
    #region Shake

    [HideInInspector]   
    public CCD_Shake Shake;                                  //get the main shake script
    
    [Header("Shake Variables")]
    [Space(10)]

    [Tooltip("The duration of the shake")]
    public float ShakeDuration = 2f;                        //the duration of the shake
    
    [Tooltip("The magnitude of the shake that will be applied on all axis")]
    public float ShakeMagnitude = 0.5f;                    //power of the shake

    #endregion
    

    //run when awake
    void Awake() {
        //error handling
        if (!GetComponent<Camera>()){
            Debug.LogError ("Cornea Camera Director must be added to a camera");
        }

        //get the effects scripts
        Warp = GetComponent<CCD_Warp>();
        Lerp = GetComponent<CCD_Lerp>();
        Handheld = GetComponent<CCD_Handheld>();
        Shake = GetComponent<CCD_Shake>();
    }

    //detects and runs the effects when triggered
    void Update()
    {
        Warp.Warping();
        Lerp.Lerping(Lerp.GetCurrentIndex);
        Handheld.Lerping(0);
    }
    
    //destroy all attached effect scripts on component delete
    //or disable
    void OnDisable()
    {
        Destroy(GetComponent<CCD_Warp>());
        Destroy(GetComponent<CCD_Lerp>());
        Destroy(GetComponent<CCD_Handheld>());
        Destroy(GetComponent<CCD_Shake>());    

    }

    //add all effect scripts when script is added or enabled
    void OnEnable()
    {
        gameObject.AddComponent<CCD_Warp>();
        gameObject.AddComponent<CCD_Lerp>();
        gameObject.AddComponent<CCD_Handheld>();
        gameObject.AddComponent<CCD_Shake>();     
    }

    //give error on any reset if attached to non-camera object
    void Reset(){
        //error handling
        if (!GetComponent<Camera>()){
            Debug.LogError ("Cornea Camera Director must be added to a camera");
        }
    }
}
