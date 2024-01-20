using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScenesListWindow : EditorWindow
{
    Vector2 scrollPos;
    private GUIStyle customTitleStyle;
    private GUIStyle customLabelStyle;

    private void OnEnable()
    {
        minSize = new Vector2 (300, 200);
        try
        {
            customTitleStyle = new GUIStyle(EditorStyles.label);
            customTitleStyle.fontSize = 20;

            customLabelStyle = new GUIStyle(EditorStyles.label);
            customLabelStyle.padding.left = 10;
        }
        catch { }

    }


    [UnityEditor.MenuItem("Utils/SceneLoadManager/Build Scenes Window")]
    public static void ShowWindow()
    {
        GetWindow<ScenesListWindow>("Build Scenes");
    }
    private void OnGUI()
    {
        GUILayout.Label("Scenes list in build.", customTitleStyle);

        if (SceneLoadManager.scenes != null)
        {
            EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.size.x), GUILayout.Height(100));
            foreach (string scene in SceneLoadManager.scenes)
            {
                GUILayout.Label("• " + scene, customLabelStyle);
            }
            EditorGUILayout.EndScrollView();
        }
        else
        {
            SceneLoadManager.UpdateNames();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Update scenes list"))
        {
            SceneLoadManager.UpdateNames();
        }
    }
}
