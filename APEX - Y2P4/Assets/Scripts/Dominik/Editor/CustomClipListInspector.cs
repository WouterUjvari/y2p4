using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClipList))]
public class CustomClipListInspector : Editor 
{

    public ClipList clipList;

    private void OnEnable()
    {
        clipList = (ClipList)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(40);

        #region Save Asset Button
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Asset"))
        {
            SaveAssetsDirty();
        }

        GUILayout.EndHorizontal();
        #endregion
    }

    private void SaveAssetsDirty()
    {
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
