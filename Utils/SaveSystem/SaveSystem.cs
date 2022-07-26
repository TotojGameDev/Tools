using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TotojGameDev
{
    public static class SaveSystem
    {
        private static readonly string SaveFilePath = Application.persistentDataPath;
        
        const string DEFAULT_FILE_NAME = "game-data.game";

        public static bool SavePlayer(object data, string fileName = DEFAULT_FILE_NAME)
        {
            if (data != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string fullPath = fileName != "" ? $"{SaveFilePath}/{fileName}" : $"{SaveFilePath}/{DEFAULT_FILE_NAME}";
                Debug.Log($"Saving player data in path : {fullPath}");
                FileStream stream = new FileStream(fullPath, FileMode.Create);

                formatter.Serialize(stream, data);
                stream.Close();
                return true;
            }

            return false;
        }

        public static object LoadPlayer(string fileName = DEFAULT_FILE_NAME)
        {
            string fullPath = fileName != "" ? $"{SaveFilePath}/{fileName}" : $"{SaveFilePath}/{DEFAULT_FILE_NAME}";
            Debug.Log($"Loading player saved data in path : {fullPath}");

            if (File.Exists(fullPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(fullPath, FileMode.Open);

                if (stream == null || stream.Length == 0)
                {
                    Debug.LogWarning($"Empty stream, returning null in {fullPath}");
                    return null;
                }

                object data = formatter.Deserialize(stream);

                stream.Close();
                return data;
            }

            Debug.LogWarning($"Save file not found in {fullPath}");
            return null;
        }
        
        public static void RemovePlayerSavedData(string fileName = DEFAULT_FILE_NAME) {
            string fullPath = fileName != "" ? $"{SaveFilePath}/{fileName}" : $"{SaveFilePath}/{DEFAULT_FILE_NAME}";
            Debug.Log($"Removing player saved data in path : {fullPath}");
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log("Done !");
            }
            else
            {
                Debug.Log("File not found.");
            }
        }
    }
}