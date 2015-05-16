using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Grid.TerrainType))]
public class TerrainTypeEditor : PropertyDrawer
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

        EditorGUIUtility.labelWidth = 46f;
        contentPosition.width *= 0.5f;

        EditorGUI.indentLevel = 0;

        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("penalty"));

        contentPosition.x += contentPosition.width;
        EditorGUIUtility.labelWidth = 33f;
        
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("mask"));


        EditorGUI.EndProperty();
        EditorGUI.indentLevel = oldIndentLevel;
    }

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        return Screen.width < 333 ? (16f + 18f) : 16f;
    }
}