//Copyright (c) 2018 - @QuantumCalzone
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LayoutElementExtendedValue))]
public class LayoutElementExtendedValuePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        GUI.Label(position, label, EditorStyles.boldLabel);

        SerializedProperty enabled = property.FindPropertyRelative("enabled");
        EditorGUILayout.PropertyField(enabled);

        if (enabled.boolValue)
        {
            SerializedProperty reference = property.FindPropertyRelative("reference");
            SerializedProperty referenceType = property.FindPropertyRelative("referenceType");
            SerializedProperty referenceDelta = property.FindPropertyRelative("referenceDelta");
            SerializedProperty targetValue = property.FindPropertyRelative("targetValue");

            EditorGUILayout.PropertyField(reference);
            EditorGUILayout.PropertyField(referenceType);
            EditorGUILayout.PropertyField(referenceDelta);

            if (referenceType.enumValueIndex != 0)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(targetValue);
                GUI.enabled = true;
            }
            else EditorGUILayout.PropertyField(targetValue);
        }

        EditorGUI.EndProperty();
    }
}
