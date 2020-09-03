using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetComponent))]
public class TargetEditor : Editor
{
    private SerializedProperty _lifeProperty;
    private SerializedProperty _DieParticleProperty;
    private SerializedProperty _hitClipsProperty;
    private SerializedProperty _useParticlesProperty;
    private SerializedProperty _DieEventProperty;

    private void OnEnable()
    {
        _lifeProperty = serializedObject.FindProperty("life");
        _DieParticleProperty = serializedObject.FindProperty("dieParticle");
        _hitClipsProperty = serializedObject.FindProperty("hitClips");
        _useParticlesProperty = serializedObject.FindProperty("useParticlesOnDeath");
        _DieEventProperty = serializedObject.FindProperty("onDie");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_lifeProperty);
        EditorGUILayout.PropertyField(_hitClipsProperty);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_DieEventProperty);
        EditorGUILayout.PropertyField(_useParticlesProperty, new GUIContent("Particles On Death", "if true, will use particles set when this target die"));

        if(_useParticlesProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_DieParticleProperty);
        }

        //Set to apply modifications in the editor. Without this, it not possible modify the values
        serializedObject.ApplyModifiedProperties();
    }
}
