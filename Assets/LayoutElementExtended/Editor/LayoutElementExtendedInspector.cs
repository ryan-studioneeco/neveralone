//Copyright (c) 2018 - @QuantumCalzone
using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(LayoutElementExtended), true)]
public class LayoutElementExtendedInspector : LayoutElementEditor
{
    private SerializedProperty m_IgnoreLayout = null;
    private SerializedProperty m_LayoutPriority = null;

    private SerializedProperty minWidthExtended = null;
    private SerializedProperty minHeightExtended = null;
    private SerializedProperty preferredWidthExtended = null;
    private SerializedProperty preferredHeightExtended = null;
    private SerializedProperty flexibleWidthExtended = null;
    private SerializedProperty flexibleHeightExtended = null;
    private SerializedProperty maxWidth = null;
    private SerializedProperty maxHeight = null;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_IgnoreLayout);

        if (!m_IgnoreLayout.boolValue)
        {
            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.LabelField("Extended", EditorStyles.boldLabel);

            DisplayPropertyFieldInBox(minWidthExtended);
            DisplayPropertyFieldInBox(minHeightExtended);
            DisplayPropertyFieldInBox(preferredWidthExtended);
            DisplayPropertyFieldInBox(preferredHeightExtended);
            DisplayPropertyFieldInBox(flexibleWidthExtended);
            DisplayPropertyFieldInBox(flexibleHeightExtended);
            DisplayPropertyFieldInBox(maxWidth);
            DisplayPropertyFieldInBox(maxHeight);

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.PropertyField(m_LayoutPriority);

        serializedObject.ApplyModifiedProperties();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        m_IgnoreLayout = serializedObject.FindProperty("m_IgnoreLayout");
        m_LayoutPriority = serializedObject.FindProperty("m_LayoutPriority");

        minWidthExtended = serializedObject.FindProperty("minWidthExtended");
        minHeightExtended = serializedObject.FindProperty("minHeightExtended");
        preferredWidthExtended = serializedObject.FindProperty("preferredWidthExtended");
        preferredHeightExtended = serializedObject.FindProperty("preferredHeightExtended");
        flexibleWidthExtended = serializedObject.FindProperty("flexibleWidthExtended");
        flexibleHeightExtended = serializedObject.FindProperty("flexibleHeightExtended");
        maxWidth = serializedObject.FindProperty("maxWidth");
        maxHeight = serializedObject.FindProperty("maxHeight");
    }

    private void DisplayPropertyFieldInBox(SerializedProperty target)
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(target);
        EditorGUILayout.EndVertical();
    }
}
