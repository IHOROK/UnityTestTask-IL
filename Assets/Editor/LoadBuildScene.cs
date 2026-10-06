using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Opens an enabled scene from Build Settings by its position among enabled scenes.
/// </summary>
public static class LoadBuildScene
{
    [MenuItem("Tools/Scenes/Load First Active Scene &1")]
    private static void LoadFirst()
    {
        LoadActiveSceneAtIndex(0);
    }

    [MenuItem("Tools/Scenes/Load Second Active Scene &2")]
    private static void LoadSecond()
    {
        LoadActiveSceneAtIndex(1);
    }

    private static void LoadActiveSceneAtIndex(int activeSceneIndex)
    {
        var activeScenes = System.Array.FindAll(EditorBuildSettings.scenes, scene => scene.enabled);
        if (activeSceneIndex >= activeScenes.Length || string.IsNullOrEmpty(activeScenes[activeSceneIndex].path))
        {
            Debug.LogWarning($"There is no active scene #{activeSceneIndex + 1} configured in Build Settings.");
            return;
        }

        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        EditorSceneManager.OpenScene(activeScenes[activeSceneIndex].path, OpenSceneMode.Single);
    }
}
