using Oculus.Platform.Models;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(TransformRangeValue))]
public class TransformRangeValueEditor : Editor
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    public override VisualElement CreateInspectorGUI ()
    {
        // Create a new VisualElement to be the root of our inspector UI
        var listVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/TransformRangeValueEditor.uxml");
        
        var root = listVisualTree.CloneTree();
        var container = root.Q<VisualElement>("container");
        var useRefStart = container.Q<Toggle>("useRefStart");
        var scriptReference = container.Q<ObjectField>("scriptReference");
        var useRefEnd = container.Q<Toggle>("useRefEnd");
        var startPositionTransform = container.Q<ObjectField>("startPositionTransform");
        var endPositionTransform = container.Q<ObjectField>("endPositionTransform");
        var startPosition = container.Q<Vector3Field>("startPosition");
        var endPosition = container.Q<Vector3Field>("endPosition");
        var setStartPosition = container.Q<Button>("setStartPosition");
        var setEndPosition = container.Q<Button>("setEndPosition");

        scriptReference.objectType = typeof(MonoScript);
        scriptReference.value = AssetDatabase.LoadAssetAtPath<MonoScript>("Assets/Scripts/TransformRangeValue.cs");
        scriptReference.SetEnabled(false);

        if(useRefStart.value)
        {
            startPositionTransform.style.display = DisplayStyle.Flex;
            startPosition.style.display = DisplayStyle.None;
            setStartPosition.style.display = DisplayStyle.None;
        }
        else
        {
            startPositionTransform.style.display = DisplayStyle.None;
            startPosition.style.display = DisplayStyle.Flex;
            setStartPosition.style.display = DisplayStyle.Flex;
        }

        if(useRefEnd.value)
        {
            endPositionTransform.style.display = DisplayStyle.Flex;
            endPosition.style.display = DisplayStyle.None;
            setEndPosition.style.display = DisplayStyle.None;
        }
        else
        {
            endPositionTransform.style.display = DisplayStyle.None;
            endPosition.style.display = DisplayStyle.Flex;
            setEndPosition.style.display = DisplayStyle.Flex;
        }

        useRefStart.RegisterValueChangedCallback((evt) =>
        {
            if(evt.newValue)
            {
                startPositionTransform.style.display = DisplayStyle.Flex;
                startPosition.style.display = DisplayStyle.None;
                setStartPosition.style.display = DisplayStyle.None;
            }
            else
            {
                startPositionTransform.style.display = DisplayStyle.None;
                startPosition.style.display = DisplayStyle.Flex;
                setStartPosition.style.display = DisplayStyle.Flex;
            }
        });

        useRefEnd.RegisterValueChangedCallback((evt) =>
        {
            if(evt.newValue)
            {
                endPositionTransform.style.display = DisplayStyle.Flex;
                endPosition.style.display = DisplayStyle.None;
                setEndPosition.style.display = DisplayStyle.None;
            }
            else
            {
                endPositionTransform.style.display = DisplayStyle.None;
                endPosition.style.display = DisplayStyle.Flex;
                setEndPosition.style.display = DisplayStyle.Flex;
            }
        });

        setStartPosition.clicked += () =>
        {
            startPosition.value = ((TransformRangeValue)target).transform.position;
        };

        setEndPosition.clicked += () =>
        {
            endPosition.value = ((TransformRangeValue)target).transform.position;
        };

        return root;
    }
}
