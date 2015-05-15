using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Point3))]
public class Point3Editor : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        int oldIndentLevel = EditorGUI.indentLevel;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        EditorGUIUtility.labelWidth = 14f;
        contentPosition.width *= 0.333f;

        EditorGUI.indentLevel = 0;

        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("x"));

        contentPosition.x += contentPosition.width;


        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"));

        contentPosition.x += contentPosition.width;

        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("z"));

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = oldIndentLevel;
    }
}