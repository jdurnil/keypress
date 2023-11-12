using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField, Oculus.Interaction.Interface(typeof(IInteractableView))]
    private Object interactableView;

    private IInteractableView interactableViewInterface;
    public float speed = 20f;
    public Animator testanim;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        interactableViewInterface = interactableView as IInteractableView; 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(interactableViewInterface.State);
        
        if(interactableViewInterface.State == InteractableState.Select && isPressed==false) {
            //testanim.SetTrigger("Press");
            transform.Rotate(7, 0, 0);
            isPressed = true;
        }
        
        if(interactableViewInterface.State == InteractableState.Normal && isPressed == true)
        {
            transform.Rotate(-7, 0, 0);
            isPressed = false;
        }
    }
}
