using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixButton : MonoBehaviour
{
    [SerializeField, Oculus.Interaction.Interface(typeof(IInteractableView))]
    private Object interactableView;

    private IInteractableView interactableViewInterface;
    public Animator testanim;
    public AudioSource audioSource;
    public AudioClip sound;
    public GameObject target;
    private bool isSelected = false;
    public testlerp testlerp;
    public testlerp testlerp2;
    public string childName = "Fader";
    private List<GameObject> childObjects;
    public GameObject Control;
    public Transform afterStartMixerSpot;

    //private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        interactableViewInterface = interactableView as IInteractableView;
        Control.transform.position += Vector3.up * 100f;
        
        childObjects = GameObject.FindGameObjectsWithTag("Fader").ToList();
        // TurnOffTransform();
        //target.SetActive(false);
        // Control.SetActive(false);
        // Now childObjects contains all child GameObjects with the name "MyChild"

        //board.SetActive(false);

        CoroutinesManager.Instance.StartCoroutine(LateStart());
    }

    private IEnumerator LateStart ()
    {
        Debug.Log("<color=red>Waiting for 2 seconds</color>");
        yield return new WaitForEndOfFrame();
        Debug.Log("<color=green>2 seconds have passed</color>");
        TurnOffTransform();
        Control.SetActive(false);
        Control.transform.position = afterStartMixerSpot.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Use the commented line instead after the equal to use the hover instead of selected
        if (interactableViewInterface.State == InteractableState.Select && !isSelected) //InteractableState.Hover) 
        {

            //gameObject.GetComponent<Renderer>().material.color = _selectedColor;

            audioSource.PlayOneShot(sound);
            //target.SetActive(!target.activeSelf);
           
            if(!Control.activeSelf)
            {
                Control.SetActive(!Control.activeSelf);
                var scale = new Vector3(0.1f, 0.091338f, 0.1f);
                testlerp.Grow(scale);
                testlerp2.Shrink();
                TurnOnTransform();
            }
            else
            {
                TurnOffTransform();
                
                testlerp.Shrink();
                testlerp2.Shrink();
                Control.SetActive(!Control.activeSelf);
            }
            
            
            //isPressed = true;

            isSelected = true;

           


            //gameObject.GetComponent<Renderer>().material.color = _selectedColor;
        }
        else if (interactableViewInterface.State == InteractableState.Normal)
        {
            isSelected = false;
        }
        // if you use the Hover I suggest uncomment this line, if you are happy with Select state leave this line commented


        //testanim.SetBool("Pressed", isPressed);

    }

    public void TurnOffTransform()
    {
        foreach (GameObject child in childObjects)
        {
            child.GetComponent<TransformRangeValue>().enabled = false;
        }
    }

    public void TurnOnTransform()
    {
        foreach (GameObject child in childObjects)
        {
            child.GetComponent<TransformRangeValue>().enabled = true;
        }
    }
}
