using Keplerth;
using System;
using System.IO;
using UnityEngine;

namespace CutiesCoop
{
    [Serializable]
    public class CutiesConfig
    {
        [NonSerialized]
        public string configPath;

        public int dropRate;

        public int[] allowedItemIDs;

        public CutiesConfig()
        {
            configPath = Application.persistentDataPath + "/cuties.json";

            dropRate = 2;

            allowedItemIDs = new[] { 
                //ore
                3004, 3005, 3006, 3007, 3008, 3009, 3012, 3013, 3014, 3015,
                //plants
                9001, 9002, 3221, 3204, 3203, 3201,
                //food
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
                //drops
                9003, 9006, 9007, 9008, 9009, 9010, 9018, 9027
            };

            if (File.Exists(configPath))
            {
                LoadConfig();
            }
            else
            {
                SaveConfig();
            }
        }

        public void SaveConfig()
        {
            string classJson = JsonUtility.ToJson(this);
            try
            {
                File.WriteAllText(configPath, classJson);
            }
            catch (Exception) { }
        }

        public void LoadConfig()
        {
            string readConfig = String.Empty;
            try
            {
                readConfig = File.ReadAllText(configPath);
            }
            catch (Exception) { }

            if (readConfig != string.Empty)
            {
                JsonUtility.FromJsonOverwrite(readConfig, this);
            }
        }
    }
}
