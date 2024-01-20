using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    public static string[] scenes;
    #if UNITY_EDITOR
    private static string[] ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }
    [UnityEditor.MenuItem("Utils/SceneLoadManager/Update Scene Names")]
    public static void UpdateNames()
    {
        //SceneLoadManager context = (SceneLoadManager)command.context;
        scenes = ReadNames();
    }

    private void Reset()
    {
        scenes = ReadNames();
    }
#endif
}





