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
        if (position.height > 16f)
        {
            position.height = 16f;
            EditorGUI.indentLevel += 1;
            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.y += 18f;
        }

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

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (16f + 18f) : 16f;
    }
}