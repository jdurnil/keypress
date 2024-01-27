using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RainbowArt.CleanFlatUI
{
    public class TransitionMultiHover : MonoBehaviour,IPointerEnterHandler
    {
        [SerializeField]
        Animator[] animators;

        public void OnPointerEnter(PointerEventData eventData)
        {
            for(int i = 0; i < animators.Length; i++)
            {
                Animator animator = animators[i];
                animator.Play("Transition",0,0);
            }    
        }
    }
}