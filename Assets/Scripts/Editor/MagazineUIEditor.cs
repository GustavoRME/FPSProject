using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(MagazineUIComponent))]
public class MagazineUIEditor : Editor
{
    private SerializedProperty _magazineType;
    private SerializedProperty _textMesh;
    private SerializedProperty _meshRenderer;   
    private SerializedProperty _maxWobble;
    private SerializedProperty _wobbleSpeed;
    private SerializedProperty _recovery;
    

    private void OnEnable()
    {
        _magazineType = serializedObject.FindProperty("magazineType");
        _textMesh = serializedObject.FindProperty("textMesh");
        _meshRenderer = serializedObject.FindProperty("rend");
        _maxWobble = serializedObject.FindProperty("maxWobble");
        _wobbleSpeed = serializedObject.FindProperty("wobbleSpeed");
        _recovery = serializedObject.FindProperty("recovery");        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_magazineType);

        if (_magazineType.intValue == (int)MagazineUIComponent.MagazineType.Text)
        {
            //Text mode to show the bullets at clip
            EditorGUILayout.PropertyField(_textMesh);
        }
        else
        {
            //Renderer mode to show the bullets at clip
            EditorGUILayout.PropertyField(_meshRenderer);
            EditorGUILayout.PropertyField(_maxWobble);
            EditorGUILayout.PropertyField(_wobbleSpeed);
            EditorGUILayout.PropertyField(_recovery);                  
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
