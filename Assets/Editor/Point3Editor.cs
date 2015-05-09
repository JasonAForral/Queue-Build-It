using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Point3))]
public class Point3Editor : Editor
{

    public override void OnInspectorGUI ()
    {
        //SerializeField one = (SerializeField)target;
        //base.OnInspectorGUI();
        //EditorGUILayout.IntField("Experience", target);
    }

}
