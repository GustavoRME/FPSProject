using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LauncherComponent))]
public class LauncherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
               
        var component = (LauncherComponent)target;
        
        if (GUILayout.Button("Throw Grenade"))
            component.Fire();
    }
}

