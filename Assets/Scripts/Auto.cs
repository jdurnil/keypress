using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Auto : MonoBehaviour
{
    public GameObject button;
    [SerializeField]
    private MaterialPropertyBlockEditor _editor;
    [SerializeField]
    private MaterialPropertyBlockEditor _editor2;
    public List<GameObject> buttons;
    [SerializeField]
    public List<GameObject> tests;
    private Color _currentColor;

  
    // Start is called before the first frame update
    void Start()
    {

        //_currentColor = button.GetComponent<RoundedBoxProperties>().Color;
        _editor.MaterialPropertyBlock.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.31f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnButtonSelect()
    {
        //button.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        //button.GetComponent<Renderer>().material.SetColor("_Color",  new Color(1.0f, 0.0f, 0.0f, 0.75f));
        //button.GetComponent<RoundedBoxProperties>().Color = new Color(1.0f, 0.0f, 0.0f, 0.75f);
        foreach(var button2 in buttons)
        {
            //button2.GetComponent<MaterialPropertyBlock>().SetColor("_Color", _currentColor);
            if(button2 != null)
            {
                var tempbutton = button2.GetComponent<MaterialPropertyBlockEditor>();
                tempbutton.MaterialPropertyBlock.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.31f));
            }
           
        }
        _editor.MaterialPropertyBlock.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 0.75f));
        //_editor2.MaterialPropertyBlock.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 0.75f));


    }
}
