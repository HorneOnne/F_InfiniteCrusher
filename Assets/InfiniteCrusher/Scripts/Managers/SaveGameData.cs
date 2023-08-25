using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace InfiniteCrusher
{
    [System.Serializable]
    public class SaveGameData
    {
        private readonly string path = Application.dataPath + "/InfiniteCrusher/Saves/GameData";
        private GameData _gameData;

        public SaveGameData(GameData gameData)
        {
            this._gameData = gameData;
        }

        public void SaveAllData()
        {
            Save(_gameData, "gameData");
        }

        public void LoadAllData()
        {
            _gameData = Load<GameData>("gameData");
        }

        public GameData GetSaveData()
        {
            return _gameData;
        }


        #region Private Save/Load methods
        private void Save<T>(T objectToSave, string key)
        {
            Directory.CreateDirectory(path);
            string jsonString = JsonUtility.ToJson(objectToSave);
            using (StreamWriter sw = new StreamWriter($"{path}{key}.json"))
            {
                sw.Write(jsonString);
            }

            Debug.Log($"Saved at path: {path}{key}.json");
        }

        private T Load<T>(string key)
        {
            T returnValue = default(T);
            if (File.Exists($"{path}{key}.json"))
            {
                string jsonString = "";
                // LOAD DATA
                using (StreamReader sr = new StreamReader($"{path}{key}.json"))
                {
                    jsonString = sr.ReadToEnd();
                    returnValue = JsonUtility.FromJson<T>(jsonString);
                    Debug.Log("Loaded.");
                }
            }
            else
            {
                Debug.Log("NOT FOUND FILE.");
            }
            return returnValue;
        }

        private string LoadJsonString(string key)
        {
            string returnString = "";
            if (File.Exists($"{path}{key}.json"))
            {
                string jsonString = "";
                // LOAD DATA
                using (StreamReader sr = new StreamReader($"{path}{key}.json"))
                {
                    jsonString = sr.ReadToEnd();
                    Debug.Log("Loaded.");
                }
                returnString = jsonString;
            }
            else
            {
                Debug.Log("NOT FOUND FILE.");
            }
            return returnString;
        }
        #endregion
    }

    [System.Serializable]
    public struct GameData
    {
        public SerializableBigInt Balance;
        public ExperienceData ExperienceData;
        public int UpgradeSpeedLevel;
        public int UpgradeTeethLevel;
        public int UpgradeToothSizeLevel;
    }

    [System.Serializable]
    public struct SerializableBigInt
    {
        public string value;

        public SerializableBigInt(BigInteger bigInt)
        {
            value = bigInt.ToString();
        }

        public BigInteger ToBigInteger()
        {
            return BigInteger.Parse(value);
        }
    }
}
