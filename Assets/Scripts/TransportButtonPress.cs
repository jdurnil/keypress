using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class TransportButtonPress : MonoBehaviour
{
    [SerializeField, Oculus.Interaction.Interface(typeof(IInteractableView))]
    private Object interactableView;

    private IInteractableView interactableViewInterface;
    public AudioSource audioSource;
    public AudioClip sound;
    [SerializeField]
    public string buttonName;
    private bool isPressed = false;
    public EventOut eventOut;
    [SerializeField]
    private Color _selectedColor = Color.red;
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {

        interactableViewInterface = interactableView as IInteractableView;
    }

    // Update is called once per frame
    void Update()
    {
        // Use the commented line instead after the equal to use the hover instead of selected
        if (interactableViewInterface.State == InteractableState.Select && !isSelected) //InteractableState.Hover) 
        {
            
              
            eventOut.OnActivateEvent.Invoke(buttonName,0, 1);
            audioSource.PlayOneShot(sound);
            isPressed = true;
          
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
    public void ReceiveDispatch(int ChannelNumber, float value)
    {
        if (0== ChannelNumber)
        {
            if (value == 1)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                isPressed = true;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                isPressed = false;
            }
        }
    }
}
