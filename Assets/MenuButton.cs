using Oculus.Interaction;
using UnityEngine;

public class MenuButton : MonoBehaviour
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

    //private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        interactableViewInterface = interactableView as IInteractableView;
        target.SetActive(false);
        //board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Use the commented line instead after the equal to use the hover instead of selected
        if (interactableViewInterface.State == InteractableState.Select && !isSelected) //InteractableState.Hover) 
        {
            
                //gameObject.GetComponent<Renderer>().material.color = _selectedColor;
               
            audioSource.PlayOneShot(sound);
            target.SetActive(!target.activeSelf);
            //testlerp.Grow();
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
}
