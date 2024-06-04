using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AutoButtonPress : MonoBehaviour
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
    public Animator testanim;
    public int channelNumber;
    public GameObject menu;
    [SerializeField]
    private Color _selectedColor = Color.red;
    public bool isSelected = false;
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
        if (interactableViewInterface.State == InteractableState.Select && !isSelected) //InteractableState.Hover) 
        {
            if (!isPressed)
            {

                //gameObject.GetComponent<Renderer>().material.color = _selectedColor;
                gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", _selectedColor);
                
                audioSource.PlayOneShot(sound);
                isPressed = true;
                //eventOut.OnActivateEvent.Invoke(buttonName, channelNumber, 1);
            }
            else
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

                audioSource.PlayOneShot(sound);
                isPressed = false;
                //eventOut.OnActivateEvent.Invoke(buttonName, channelNumber, 0);
            }
            isSelected = true;
            testanim.SetBool("Pressed", isPressed);
            menu.SetActive(isPressed);


            //gameObject.GetComponent<Renderer>().material.color = _selectedColor;
        }
        else if (interactableViewInterface.State == InteractableState.Normal)
        {
            isSelected = false;
        }
        // if you use the Hover I suggest uncomment this line, if you are happy with Select state leave this line commented


        //testanim.SetBool("Pressed", isPressed);

    }
    //public void ReceiveDispatch(int ChannelNumber, float value)
    //{
    //    if (channelNumber == ChannelNumber)
    //    {
    //        if (value == 1)
    //        {
    //            //gameObject.GetComponent<Renderer>().material.color = Color.red;
    //            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    //            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", _selectedColor);
    //            isPressed = true;
    //        }
    //        else
    //        {
    //            //gameObject.GetComponent<Renderer>().material.color = Color.white;
    //            gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

    //            isPressed = false;
    //        }
    //    }
    
}
