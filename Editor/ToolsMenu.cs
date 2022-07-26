using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
#endif

namespace TotojGameDev
{
    public static class ToolsMenu
    {
#if UNITY_EDITOR
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Debug.Log(dataPath);

            Folders.CreateDirectories("_Project", "Scripts", "Scenes", "Art");
            Refresh();
        }

        [MenuItem("Tools/Setup/Load New Manifest")]
        public static async void LoadNewManifest() => await Packages.ReplacePackagedFromGistUrl("613ef721e2f1f4c510dd0c3c23ae33ae");

        [MenuItem("Tools/Setup/Packages/Add New Input System")]
        public static void AddNewInputSystem() => Packages.InstallUnityPackage("inputsystem");
        
        [MenuItem("Tools/Setup/Packages/Post Processing")]
        public static void AddPostProcessing() => Packages.InstallUnityPackage("postprocessing");
        
        [MenuItem("Tools/Setup/Packages/Cinemachine")]
        public static void AddCinemachine() => Packages.InstallUnityPackage("cinemachine");
#endif
    }
}
