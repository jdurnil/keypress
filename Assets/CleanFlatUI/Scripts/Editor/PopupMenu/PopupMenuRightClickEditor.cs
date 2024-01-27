﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditorInternal;

namespace RainbowArt.CleanFlatUI
{
    [CustomEditor(typeof(PopupMenuRightClick))]
    public class PopupMenuRightClickEditor : Editor
    {
        SerializedProperty popupMenu;
        
        protected virtual void OnEnable()
        {
            popupMenu = serializedObject.FindProperty("popupMenu");      
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(popupMenu);              
            serializedObject.ApplyModifiedProperties();         
        }
    }
}
