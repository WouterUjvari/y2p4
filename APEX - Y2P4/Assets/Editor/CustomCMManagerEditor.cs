using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomCMManagerEditor))]
public class CustomCMManagerEditor : Editor
{

    public ColorMixingManager cmManager;

    private void OnEnable()
    {
        cmManager = (ColorMixingManager)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        for (int i = 0; i < cmManager.colors.Count; i++)
        {
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Color:", EditorStyles.boldLabel);

        }

        //GUILayout.BeginVertical("box");

        //GUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Card Categories:", EditorStyles.boldLabel);
        //if (GUILayout.Button("+", GUILayout.Width(20)))
        //{
        //    AddCategory();
        //}

        //GUILayout.EndHorizontal();

        //for (int i = 0; i < card.categories.Count; i++)
        //{
        //    GUILayout.BeginHorizontal();
        //    card.categories[i] = (Card.Category)EditorGUILayout.EnumPopup("Category: ", card.categories[i]);
        //    if (GUILayout.Button("X", GUILayout.Width(20)))
        //    {
        //        RemoveCategory(i);
        //        return;
        //    }
        //    GUILayout.EndHorizontal();
        //}

        //GUILayout.EndVertical();

        //GUILayout.Space(40);

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
