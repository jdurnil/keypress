using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ButtonPtress : MonoBehaviour
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
    public ChannelNumberReceiver channelNumberReceiver;
    public int channelNumber;
    [SerializeField]
    private Color _selectedColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        channelNumber = channelNumberReceiver.ChannelNumber;
        interactableViewInterface = interactableView as IInteractableView;
    }

    // Update is called once per frame
    void Update()
    {
        // Use the commented line instead after the equal to use the hover instead of selected
        if (interactableViewInterface.State == InteractableState.Select) //InteractableState.Hover) 
        {
            if (!isPressed)
            {
                
                gameObject.GetComponent<Renderer>().material.color = _selectedColor;
                eventOut.OnActivateEvent.Invoke(buttonName, channelNumber, 1);
                isPressed = true;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                eventOut.OnActivateEvent.Invoke(buttonName, channelNumber, 0);
            }

            audioSource.PlayOneShot(sound);

            //gameObject.GetComponent<Renderer>().material.color = _selectedColor;
        } // if you use the Hover I suggest uncomment this line, if you are happy with Select state leave this line commented
         

        //testanim.SetBool("Pressed", isPressed);
    }
}
