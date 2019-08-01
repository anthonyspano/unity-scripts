using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MovePattern))]
public class MyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovePattern myTarget = (MovePattern)target;

        SerializedProperty mM = serializedObject.FindProperty("myMoves");
        SerializedProperty mW = serializedObject.FindProperty("myWait");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(mM, true);
        EditorGUILayout.PropertyField(mW, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        
    }
}