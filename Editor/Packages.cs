using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif
using UnityEngine;

namespace TotojGameDev
{
    public static class Packages
    {
        
#if UNITY_EDITOR
        internal static async Task ReplacePackagedFromGistUrl(string id, string user = "AJulliard")
        {
            var url = GetGistUrl(id, user);
            var contents = await GetContents(url);
            ReplacePackageFile(contents);
        }

        private static string GetGistUrl(string id, string user) => $"https://gist.github.com/{user}/{id}/raw";

        private static async Task<string> GetContents(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        private static void ReplacePackageFile(string contents)
        {
            var existing = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            File.WriteAllText(existing, contents);
            Client.Resolve();
        }

        public static void InstallUnityPackage(string packageName)
        {
            Client.Add($"com.unity.{packageName}");
        }
#endif
    }
}