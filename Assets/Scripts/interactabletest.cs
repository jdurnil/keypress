using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField, Oculus.Interaction.Interface(typeof(IInteractableView))]
    private Object interactableView;

    private IInteractableView interactableViewInterface;
    public Animator testanim;
    public AudioSource audioSource;
    public AudioClip sound;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        interactableViewInterface = interactableView as IInteractableView; 
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(interactableViewInterface.State);
        
        // Use the commented line instead after the equal to use the hover instead of selected
        if(interactableViewInterface.State == InteractableState.Select) //InteractableState.Hover) 
        {
            if(!isPressed)
                audioSource.PlayOneShot(sound);

            isPressed = true;
        } // if you use the Hover I suggest uncomment this line, if you are happy with Select state leave this line commented
        else //if(interactableViewInterface.State == InteractableState.Normal) 
        {
            isPressed = false;
        }
        
        testanim.SetBool("Pressed", isPressed);
    }
}
