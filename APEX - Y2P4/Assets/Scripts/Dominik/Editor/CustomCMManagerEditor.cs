using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorMixingManager))]
public class CustomCMManagerEditor : Editor
{

    public ColorMixingManager cmManager;

    private void OnEnable()
    {
        cmManager = (ColorMixingManager)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);

        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Individual Colors", EditorStyles.boldLabel);
        if (GUILayout.Button("Add Color", GUILayout.Width(100)))
        {
            AddColor();
            return;
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(10);

        for (int i = 0; i < cmManager.colors.Count; i++)
        {
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();

            ColorMixingManager.Colors colorInfo = cmManager.colors[i];

            EditorGUILayout.LabelField("Color:", EditorStyles.boldLabel);
            colorInfo.color = EditorGUILayout.ColorField(cmManager.colors[i].color);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                RemoveColor(i);
                return;
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Color Name:", EditorStyles.boldLabel);
            colorInfo.name = EditorGUILayout.TextField(colorInfo.name);
            GUILayout.EndHorizontal();

            GUILayout.Space(15);

            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("This color burns into the following color:", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            colorInfo.burnsIntoColor = EditorGUILayout.TextField(colorInfo.burnsIntoColor);
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.Space(10);

            GUILayout.BeginVertical("box");

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("This color can mix with the following colors:", EditorStyles.boldLabel);
            if (GUILayout.Button("Add Color", GUILayout.Width(100)))
            {
                AddMixingColor(i);
                return;
            }
            GUILayout.EndHorizontal();

            if (colorInfo.colorMixingOptions != null)
            {
                for (int ii = 0; ii < colorInfo.colorMixingOptions.Count; ii++)
                {
                    GUILayout.Space(10);

                    ColorMixingManager.Colors.ColorMix colorMixInfo = colorInfo.colorMixingOptions[ii];

                    GUILayout.BeginHorizontal();
                    colorMixInfo.colorToMixWith = EditorGUILayout.TextField("Color gets mixed with: ", colorMixInfo.colorToMixWith);
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        RemoveMixingColor(i, ii);
                        return;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    colorMixInfo.resultColor = EditorGUILayout.TextField("And will result in the color: ", colorMixInfo.resultColor);
                    GUILayout.EndHorizontal();

                    colorInfo.colorMixingOptions[ii] = colorMixInfo;
                }
            }

            GUILayout.EndVertical();

            cmManager.colors[i] = colorInfo;

            GUILayout.EndVertical();

            GUILayout.Space(20);
        }

        //#region Save Asset Button
        //GUILayout.BeginHorizontal();

        //if (GUILayout.Button("Save Asset"))
        //{
        //    SaveAssetsDirty();
        //}

        //GUILayout.EndHorizontal();
        //#endregion
    }

    private void AddColor()
    {
        cmManager.colors.Add(new ColorMixingManager.Colors());
    }

    private void AddMixingColor(int colorsIndex)
    {
        if (cmManager.colors[colorsIndex].colorMixingOptions == null)
        {
            ColorMixingManager.Colors mix = cmManager.colors[colorsIndex];

            mix.colorMixingOptions = new List<ColorMixingManager.Colors.ColorMix>();
            cmManager.colors[colorsIndex] = mix;
        }

        cmManager.colors[colorsIndex].colorMixingOptions.Add(new ColorMixingManager.Colors.ColorMix());
    }

    private void RemoveColor(int index)
    {
        cmManager.colors.RemoveAt(index);
    }

    private void RemoveMixingColor(int colorsIndex, int index)
    {
        cmManager.colors[colorsIndex].colorMixingOptions.RemoveAt(index);
    }

    private void SaveAssetsDirty()
    {
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
    }
}
